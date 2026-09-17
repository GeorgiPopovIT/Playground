import re
from typing import Any

import requests
from bs4 import BeautifulSoup

HEADERS = {
    "User-Agent": "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 "
    "(KHTML, like Gecko) Chrome/131.0 Safari/537.36"
}


def clean_text(value: str) -> str:
    return re.sub(r"\s+", " ", value or "").strip()


def unique_keep_order(items: list[str]) -> list[str]:
    seen: set[str] = set()
    ordered: list[str] = []
    for item in items:
        key = item.lower().strip()
        if key and key not in seen:
            seen.add(key)
            ordered.append(item.strip())
    return ordered


def extract_skills(text: str) -> list[str]:
    catalog = [
        "Python", "Java", "C#", ".NET", "JavaScript", "TypeScript", "React", "Angular", "Vue",
        "SQL", "PostgreSQL", "MySQL", "MongoDB", "Docker", "Kubernetes", "AWS", "Azure", "GCP",
        "Git", "Linux", "FastAPI", "Django", "Flask", "Selenium", "Cypress", "REST", "GraphQL",
        "Power BI", "Excel", "Pandas", "NumPy", "TensorFlow", "PyTorch", "LangChain", "LangGraph",
        "OpenAI", "Machine Learning", "AI", "Data Science", "HTML", "CSS"
    ]
    lowered = text.lower()
    return [skill for skill in catalog if skill.lower() in lowered]


def demo_or_live(url: str, source: str, limit: int = 12) -> tuple[Any | None, Any]:
    try:
        response = requests.get(url, headers=HEADERS, timeout=15)
        response.raise_for_status()
        soup = BeautifulSoup(response.text, "html.parser")
        text = clean_text(soup.get_text(" ", strip=True))
        if len(text) < 300:
            raise ValueError("Page content too small")
        return response.text, soup
    except Exception as exc:  # pragma: no cover - fallback for public pages
        return None, str(exc)
