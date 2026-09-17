import json
from urllib.parse import quote_plus, urljoin

from langchain_core.tools import tool

from models import CandidateProfile, Job
from scraping import clean_text, demo_or_live, extract_skills

DEMO_JOBS = [
    Job(title="Junior Python Developer", company="Demo Software", location="Sofia",
        work_mode="Hybrid", skills=["Python", "SQL", "Git", "REST"], source="demo",
        url="https://dev.bg/job-listings/"),
    Job(title="Junior Data Analyst", company="Demo Analytics", location="Sofia",
        work_mode="Hybrid", skills=["Python", "SQL", "Excel", "Power BI"], source="demo",
        url="https://www.jobs.bg/"),
    Job(title="QA Automation Engineer", company="Demo Tech", location="Sofia",
        work_mode="Remote", skills=["Python", "Selenium", "API", "Git"], source="demo",
        url="https://dev.bg/job-listings/"),
    Job(title="Backend Developer", company="Demo Cloud", location="Sofia",
        work_mode="Remote", skills=["Python", "FastAPI", "PostgreSQL", "Docker"], source="dev.bg",
        url="https://dev.bg/job-listings/"),
    Job(title="Software Engineer", company="Demo Systems", location="Plovdiv",
        work_mode="Hybrid", skills=["Java", "SQL", "Git", "Docker"], source="jobs.bg",
        url="https://www.jobs.bg/"),
]


@tool
def search_devbg_jobs(query: str, location: str = "Sofia", limit: int = 12) -> list[dict]:
    """Search public DEV.BG listings and return normalized job records."""
    url = "https://dev.bg/job-listings/"
    html, result = demo_or_live(url, "dev.bg", limit)
    jobs: list[dict] = []

    if html:
        soup = result
        for card in soup.select("article, .job-listing, .job-listing-item, .card"):
            text = clean_text(card.get_text(" ", strip=True))
            if len(text) < 25:
                continue
            skills = extract_skills(text)
            if query and query.lower() not in text.lower() and not any(q.lower() in text.lower() for q in query.split()):
                continue
            title_el = card.find(["h2", "h3", "h4", "a"])
            title = clean_text(title_el.get_text(" ", strip=True)) if title_el else text[:90]
            link = title_el.get("href") if title_el and title_el.name == "a" else ""
            jobs.append(Job(
                title=title[:160],
                company="DEV.BG listing",
                location=location,
                skills=skills,
                description=text[:700],
                url=urljoin(url, link) if link else url,
                source="DEV.BG",
            ).model_dump())
            if len(jobs) >= limit:
                break

    if not jobs:
        lower_query = query.lower()
        for job in DEMO_JOBS:
            if not lower_query or lower_query in job.title.lower() or any(lower_query in skill.lower() for skill in job.skills):
                jobs.append(job.model_copy(update={"source": "DEV.BG (demo fallback)"}).model_dump())
        if not jobs:
            jobs = [job.model_dump() for job in DEMO_JOBS[:limit]]

    return jobs[:limit]


@tool
def search_jobsbg_jobs(query: str, location: str = "Sofia", limit: int = 12) -> list[dict]:
    """Search the public JOBS.BG search page and return normalized job records."""
    search_url = "https://www.jobs.bg/front_job_search.php" + f"?frompage=0&term={quote_plus(query)}"
    html, result = demo_or_live(search_url, "jobs.bg", limit)
    jobs: list[dict] = []

    if html:
        soup = result
        for anchor in soup.find_all("a", href=True):
            title = clean_text(anchor.get_text(" ", strip=True))
            if len(title) < 8 or len(title) > 180:
                continue
            parent = anchor.parent
            context = clean_text(parent.get_text(" ", strip=True)) if parent else title
            if len(context) < 20:
                continue
            skills = extract_skills(context)
            if query and query.lower() not in (title + " " + context).lower() and not skills:
                continue
            jobs.append(Job(
                title=title,
                company="JOBS.BG listing",
                location=location,
                skills=skills,
                description=context[:700],
                url=urljoin(search_url, anchor["href"]),
                source="JOBS.BG",
            ).model_dump())
            if len(jobs) >= limit:
                break

    if not jobs:
        lower_query = query.lower()
        for job in DEMO_JOBS:
            if not lower_query or lower_query in job.title.lower() or any(lower_query in skill.lower() for skill in job.skills):
                jobs.append(job.model_copy(update={"source": "JOBS.BG (demo fallback)"}).model_dump())
        if not jobs:
            jobs = [job.model_dump() for job in DEMO_JOBS[:limit]]

    return jobs[:limit]


@tool
def score_job_matches(candidate_json: str, jobs_json: str, top_k: int = 8) -> list[dict]:
    """Score jobs against a candidate profile using explainable deterministic rules."""
    candidate = CandidateProfile.model_validate_json(candidate_json)
    jobs = [Job.model_validate(item) for item in json.loads(jobs_json)]

    candidate_skills = {skill.lower() for skill in candidate.skills + candidate.must_have + candidate.nice_to_have}
    target_roles = [role.lower() for role in candidate.target_roles]
    results: list[dict] = []

    for job in jobs:
        job_skills = {skill.lower() for skill in job.skills}
        matched = sorted(candidate_skills & job_skills)
        missing = sorted(job_skills - candidate_skills)

        title_bonus = 0
        title_low = job.title.lower()
        for role in target_roles:
            if any(token in title_low for token in role.split() if len(token) > 2):
                title_bonus = 15
                break

        skill_score = 55 * (len(matched) / max(1, len(job_skills)))
        location_bonus = 10 if (not candidate.location or candidate.location.lower() in job.location.lower()) else 0
        score = min(100, round(skill_score + title_bonus + location_bonus, 1))

        reasons: list[str] = []
        if matched:
            reasons.append("Matched skills: " + ", ".join(matched))
        if title_bonus:
            reasons.append("Job title aligns with a target role")
        if location_bonus:
            reasons.append("Location matches the candidate preference")
        if missing:
            reasons.append("Potential gaps: " + ", ".join(missing[:5]))

        results.append(
            {
                "job": job.model_dump(),
                "score": score,
                "matched_skills": matched,
                "missing_skills": missing[:8],
                "reasons": reasons,
            }
        )

    return sorted(results, key=lambda item: item["score"], reverse=True)[:top_k]
