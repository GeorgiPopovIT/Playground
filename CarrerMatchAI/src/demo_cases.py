from __future__ import annotations

from workflow import execute_workflow

TEST_CASES = {
    "python_developer": (
        "I am looking for a Python Developer position in Sofia. "
        "I have 2 years of experience with Python, FastAPI, SQL and Git. "
        "I prefer hybrid or remote work. Find suitable job opportunities and rank them based on my profile."
    ),
    "junior_backend": (
        "I am a Junior Backend Developer looking for my first professional opportunity in Bulgaria. "
        "My main skills are Python, REST APIs, SQL, Git and basic Docker. "
        "I am interested in junior backend or Python developer positions. "
        "Search for suitable jobs and explain why each position matches my profile."
    ),
    "java_developer": (
        "I am a Java Developer with 3 years of experience. "
        "My main technologies are Java, Spring Boot, REST APIs, PostgreSQL, Docker and Git. "
        "I am looking for Backend Java Developer positions in Sofia. "
        "Search DEV.BG and JOBS.BG and select the most relevant opportunities."
    ),
    "data_analyst": (
        "I am looking for a Data Analyst position. "
        "I have experience with Python, SQL, Excel, Power BI and basic statistics. "
        "I prefer positions in Sofia, but remote opportunities are also acceptable. "
        "Find relevant job advertisements and rank them according to my skills and experience."
    ),
    "react_developer": (
        "I am a Frontend Developer with 2 years of experience. "
        "My main skills are JavaScript, TypeScript, React, HTML, CSS and Git. "
        "I am looking for React Developer or Frontend Developer positions in Sofia or remote. "
        "Search for suitable jobs and explain the match between my profile and each job."
    ),
}


def run_all_demo_cases() -> dict[str, object]:
    results: dict[str, object] = {}
    for name, request in TEST_CASES.items():
        print(f"\n==== Running demo case: {name} ====")
        results[name] = execute_workflow(request)
    return results
