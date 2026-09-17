import os
from typing import Any

from dotenv import load_dotenv

try:
    from google.colab import userdata  # type: ignore
except ImportError:  # pragma: no cover
    userdata = None

load_dotenv()


def configure_langsmith_tracing(project_name: str = "CareerMatchAI") -> bool:
    """Enable LangSmith tracing for LangChain/LangGraph runs using .env credentials."""
    api_key = os.getenv("LANGSMITH_APIKEY") or os.getenv("LANGSMITH_API_KEY")
    if not api_key:
        return False

    os.environ["LANGSMITH_TRACING"] = "true"
    os.environ["LANGSMITH_API_KEY"] = api_key
    os.environ.setdefault("LANGSMITH_PROJECT", project_name)

    workspace_id = os.getenv("LANGSMITH_WORKSPACE_ID")
    if workspace_id:
        os.environ["LANGSMITH_WORKSPACE_ID"] = workspace_id

    return True


configure_langsmith_tracing()


def get_openai_api_key() -> str:
    key = os.getenv("OPENAI_API_KEY")
    if key:
        return key
    if userdata is not None:
        key = userdata.get("OPENAI_API_KEY")
        if key:
            return key
    raise RuntimeError(
        "Missing OPENAI_API_KEY. Set it in a .env file or in Colab Secrets as OPENAI_API_KEY."
    )


def get_model_name() -> str:
    return os.getenv("MODEL_NAME", "gpt-5.4-nano")


def get_llm_settings() -> dict[str, Any]:
    return {
        "model": get_model_name(),
        "temperature": 0.1,
        "api_key": get_openai_api_key(),
    }
