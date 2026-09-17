import os
from typing import Any

from dotenv import load_dotenv

try:
    from google.colab import userdata  # type: ignore
except ImportError:  # pragma: no cover
    userdata = None

load_dotenv()


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
