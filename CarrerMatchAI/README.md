# CareerMatchAI

This project converts the notebook-based CV and job matching workflow into a proper Python project with separated files and package structure.

## Project structure

- [main.py](main.py) - CLI entry point
- [requirements.txt](requirements.txt) - runtime dependencies
- [pyproject.toml](pyproject.toml) - package metadata
- [src/career_match_ai/__init__.py](src/career_match_ai/__init__.py) - package export
- [src/career_match_ai/config.py](src/career_match_ai/config.py) - API key and model configuration
- [src/career_match_ai/models.py](src/career_match_ai/models.py) - Pydantic data models
- [src/career_match_ai/scraping.py](src/career_match_ai/scraping.py) - public page scraping helpers
- [src/career_match_ai/tools.py](src/career_match_ai/tools.py) - job search and scoring tools
- [src/career_match_ai/workflow.py](src/career_match_ai/workflow.py) - LangGraph workflow and agent logic
- [src/career_match_ai/demo_cases.py](src/career_match_ai/demo_cases.py) - demo scenarios

## Setup

1. Create and activate a virtual environment:
   ```bash
   python -m venv .venv
   .venv\Scripts\activate
   ```
2. Install dependencies:
   ```bash
   pip install -r requirements.txt
   ```
3. Add your secret key to a local `.env` file:
   ```env
   OPENAI_API_KEY=your_key_here
   MODEL_NAME=gpt-5.4-nano
   ```

## Run the app

```bash
python main.py --demo
```

Or run a custom request:

```bash
python main.py --request "I am a Python Developer with 2 years of experience in Sofia."
```

## Notes

- This version keeps the original notebook logic but separates it into modules.
- The workflow still includes a human review step before the final answer is generated.
- If you want, the next step can be to add a real database, a web UI, or a FastAPI backend.

Expected HITL feedback:

Keep only Junior positions and exclude jobs requiring more than 2 years
of professional experience.

Test Case 3 — Java Developer

A Java Developer with Spring Boot, PostgreSQL, Docker and Git experience searches for Backend Java positions.

Expected HITL feedback:

Prioritize Spring Boot positions and remove jobs that are mainly
frontend or full-stack.

Test Case 4 — Data Analyst

A Data Analyst with Python, SQL, Excel and Power BI experience searches for relevant positions.

Expected HITL action:

approve

Test Case 5 — React / Frontend Developer

A Frontend Developer with JavaScript, TypeScript and React experience searches for React/Frontend positions.

Expected HITL feedback:

Only keep positions where React is explicitly mentioned as a required
or preferred technology. Prioritize remote jobs.

Running the Project

Open the .ipynb file in Google Colab.

Install the required Python packages using the installation cell at the top of the notebook.

Open Colab Secrets.

Add a secret named:

OPENAI_API_KEY

Run the notebook cells from top to bottom.

Execute the test cases.

When the workflow pauses for HITL review, provide either:

approve, or

specific feedback for revision.

Observe the final job-matching result.

Project Structure

The submission consists of:

CV_JOB_Applicant_LangGraph_Colab.ipynb
README.md

The main notebook contains the complete implementation, agents, tools, LangGraph workflow, memory, HITL mechanism and test cases.

Security

API keys must never be included directly in the source code.

Use Google Colab Secrets:

from google.colab import userdata
userdata.get("OPENAI_API_KEY")

Do not commit API keys to GitHub or include them in the submitted ZIP archive.

Expected Workflow

User Request
     |
     v
Candidate Profiler
     |
     v
Job Researcher
     |
     v
Match Analyst
     |
     v
Human Review (HITL)
     |
     +---- approve ----+
     |                 |
     +---- feedback ---+
             |
             v
       Final Agent
             |
             v
      Final Job Report

Assignment Requirements Covered

The implementation addresses the main requirements of the assignment:

Stateful graph using LangGraph

Multiple distinct AI agents

Specific role and responsibility for each agent

Shared state between agents

At least two tools

Conversational/workflow memory

Human-in-the-Loop interruption

execute_workflow(user_request) core function

Human approval and revision scenarios

At least five test cases

API key stored through Colab Secrets

Google Colab compatible notebook

Notes

Job-board websites can change their HTML structure or restrict automated requests. The notebook therefore separates job-search functionality from the agent workflow and can use fallback/demo job data when live job-board access is unavailable.

The project is intended as an educational demonstration of a multi-agent workflow rather than an automated application-submission system.