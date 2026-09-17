"""CareerMatchAI package."""

__all__ = ["execute_workflow"]


def execute_workflow(user_request: str):
    from .workflow import execute_workflow as _execute_workflow

    return _execute_workflow(user_request)
