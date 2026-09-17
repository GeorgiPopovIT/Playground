from __future__ import annotations

from typing import Any, TypedDict

from pydantic import BaseModel, Field


class CandidateProfile(BaseModel):
    name: str = "Candidate"
    target_roles: list[str] = Field(default_factory=list)
    skills: list[str] = Field(default_factory=list)
    years_experience: float = 0.0
    education: list[str] = Field(default_factory=list)
    languages: list[str] = Field(default_factory=list)
    location: str = ""
    work_preference: str = ""
    salary_expectation: str = ""
    must_have: list[str] = Field(default_factory=list)
    nice_to_have: list[str] = Field(default_factory=list)


class Job(BaseModel):
    title: str
    company: str = "Unknown"
    location: str = ""
    work_mode: str = ""
    skills: list[str] = Field(default_factory=list)
    description: str = ""
    url: str = ""
    source: str = ""


class Match(BaseModel):
    job: Job
    score: float
    matched_skills: list[str] = Field(default_factory=list)
    missing_skills: list[str] = Field(default_factory=list)
    reasons: list[str] = Field(default_factory=list)


class WorkflowState(TypedDict, total=False):
    user_request: str
    candidate: dict[str, Any]
    jobs: list[dict[str, Any]]
    matches: list[dict[str, Any]]
    human_feedback: str
    final_answer: str
    trace: list[str]
