import hashlib
import json
import re
from typing import Any

from langchain_openai import ChatOpenAI
from langchain_core.tools import tool
from langgraph.checkpoint.memory import MemorySaver
from langgraph.graph import END, START, StateGraph
from langgraph.types import Command, interrupt

from config import get_llm_settings
from models import CandidateProfile, WorkflowState
from tools import score_job_matches, search_devbg_jobs, search_jobsbg_jobs


def get_llm() -> ChatOpenAI:
    return ChatOpenAI(**get_llm_settings())

PROFILE_SYSTEM = """
You are Candidate Profiler, the first agent in a job-application workflow.
Turn the user's CV/request into a structured candidate profile.
Never invent experience, education, skills, salary, location, or language level.
If information is missing, leave it empty or use a conservative default.
Return ONLY valid JSON matching the CandidateProfile schema.
"""

RESEARCH_SYSTEM = """
You are Job Researcher. Find relevant jobs for the candidate using the supplied
job-board tools. Prefer real listing information. Do not invent companies,
requirements, salaries, locations, or URLs. Return a compact JSON list of jobs.
"""

MATCH_SYSTEM = """
You are Match Analyst. Explain which jobs fit the candidate and why.
Use the deterministic scoring tool as the numerical baseline, then use your
language reasoning to explain strengths, gaps, and practical next steps.
Never claim a candidate has a skill that is not present in the profile.
"""


def llm_json(prompt: str) -> dict:
    response = get_llm().invoke(prompt)
    raw = response.content if isinstance(response.content, str) else str(response.content)
    raw = re.sub(r"^```json\s*|\s*```$", "", raw.strip(), flags=re.I)
    return json.loads(raw)


def profile_agent(state: WorkflowState):
    prompt = PROFILE_SYSTEM + "\n\nUSER REQUEST:\n" + state["user_request"]
    schema_hint = CandidateProfile.model_json_schema()
    prompt += "\n\nJSON SCHEMA:\n" + json.dumps(schema_hint, ensure_ascii=False)
    data = llm_json(prompt)
    profile = CandidateProfile.model_validate(data)
    trace = state.get("trace", []) + ["Agent 1: Candidate Profiler completed."]
    return {"candidate": profile.model_dump(), "trace": trace}


def researcher_agent(state: WorkflowState):
    candidate = CandidateProfile.model_validate(state["candidate"])
    query = candidate.target_roles[0] if candidate.target_roles else (candidate.skills[0] if candidate.skills else "software developer")
    dev_jobs = search_devbg_jobs.invoke({"query": query, "location": candidate.location or "Sofia", "limit": 10})
    jobs = search_jobsbg_jobs.invoke({"query": query, "location": candidate.location or "Sofia", "limit": 10})

    combined = dev_jobs + jobs
    seen: set[tuple[str, str, str]] = set()
    unique: list[dict[str, Any]] = []
    for job in combined:
        key = (str(job.get("title", "")).lower(), str(job.get("company", "")).lower(), str(job.get("url", "")))
        if key not in seen:
            seen.add(key)
            unique.append(job)

    trace = state.get("trace", []) + [
        f"Agent 2: Job Researcher used DEV.BG ({len(dev_jobs)} results) and JOBS.BG ({len(jobs)} results)."
    ]
    return {"jobs": unique[:20], "trace": trace}


def matcher_agent(state: WorkflowState):
    candidate = CandidateProfile.model_validate(state["candidate"])
    matches = score_job_matches.invoke({
        "candidate_json": candidate.model_dump_json(),
        "jobs_json": json.dumps(state["jobs"], ensure_ascii=False),
        "top_k": 8,
    })
    trace = state.get("trace", []) + ["Agent 3: Match Analyst scored and ranked the jobs."]
    return {"matches": matches, "trace": trace}


def human_review_node(state: WorkflowState):
    top = state.get("matches", [])[:5]
    review_text = "I've prepared a shortlist of the most relevant jobs for you.\n\n"

    for index, match in enumerate(top, start=1):
        job = match["job"]
        review_text += (
            f"{index}. {job['title']} — {job['company']}\n"
            f"   Match: {match['score']}%\n"
            f"   Location: {job.get('location') or 'Not specified'}\n"
            f"   Work mode: {job.get('work_mode') or 'Not specified'}\n"
            f"   Matched skills: {', '.join(match.get('matched_skills', [])) or 'None identified'}\n\n"
        )

    review_text += (
        "Please review the shortlist before I prepare the final recommendations.\n\n"
        "You can reply with:\n"
        "• approve — to continue with this shortlist\n"
        "• or tell me what you would like to change, for example \"prioritize remote jobs\" or \"keep only junior positions\"."
    )

    feedback = interrupt(review_text)
    return {
        "human_feedback": str(feedback),
        "trace": state.get("trace", []) + ["HITL: human feedback received."]
    }


def final_agent(state: WorkflowState):
    candidate = CandidateProfile.model_validate(state["candidate"])
    matches = state.get("matches", [])
    feedback = state.get("human_feedback", "")

    prompt = MATCH_SYSTEM + f"""

CANDIDATE:
{candidate.model_dump_json(indent=2)}

RANKED MATCHES:
{json.dumps(matches, ensure_ascii=False, indent=2)}

HUMAN FEEDBACK:
{feedback}

Create the final response in Bulgarian.
For each recommended job include: rank, title, company, fit score,
matched skills, important gaps, source and URL.
Then add:
- Why these jobs are a fit
- What to improve in the CV
- 3 concrete application tips
Do not invent missing job facts. If a field is unavailable, say "не е посочено".
"""

    response = get_llm().invoke(prompt)
    answer = response.content if isinstance(response.content, str) else str(response.content)
    trace = state.get("trace", []) + ["Final Agent: final recommendations generated."]
    return {"final_answer": answer, "trace": trace}


def build_graph():
    builder = StateGraph(WorkflowState)
    builder.add_node("candidate_profiler", profile_agent)
    builder.add_node("job_researcher", researcher_agent)
    builder.add_node("match_analyst", matcher_agent)
    builder.add_node("human_review", human_review_node)
    builder.add_node("final_agent", final_agent)

    builder.add_edge(START, "candidate_profiler")
    builder.add_edge("candidate_profiler", "job_researcher")
    builder.add_edge("job_researcher", "match_analyst")
    builder.add_edge("match_analyst", "human_review")
    builder.add_edge("human_review", "final_agent")
    builder.add_edge("final_agent", END)

    return builder.compile(checkpointer=MemorySaver())


def execute_workflow(user_request: str):
    """Required core function: accepts a single user request string."""
    graph = build_graph()
    thread_id = "cv-job-" + hashlib.sha256(user_request.encode()).hexdigest()[:12]
    config = {"configurable": {"thread_id": thread_id}}

    first = graph.invoke({"user_request": user_request, "trace": []}, config=config)
    if "__interrupt__" not in first:
        return first

    interrupt_message = first["__interrupt__"][0].value

    print("\n" + "=" * 70)
    print("REVIEW REQUIRED")
    print("=" * 70)
    print(interrupt_message)

    feedback = input("\nYour response: ").strip()
    resumed = graph.invoke(Command(resume=feedback), config=config)
    print("\n=== WORKFLOW TRACE ===")
    for item in resumed.get("trace", []):
        print("-", item)

    print("\n=== FINAL RESULT ===\n")
    print(resumed.get("final_answer", "No final answer produced."))
    return resumed
