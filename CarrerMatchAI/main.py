from __future__ import annotations

import argparse
import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent
SRC = ROOT / "src"
if str(SRC) not in sys.path:
    sys.path.insert(0, str(SRC))

from demo_cases import TEST_CASES, run_all_demo_cases
from workflow import execute_workflow


def main() -> None:
    parser = argparse.ArgumentParser(description="Run the CareerMatchAI workflow.")
    parser.add_argument("--request", type=str, help="A custom job-search request to process.")
    parser.add_argument("--demo", action="store_true", help="Run the built-in demo scenarios.")
    args = parser.parse_args()

    if args.demo:
        run_all_demo_cases()
        return

    if args.request:
        result = execute_workflow(args.request)
        print(result)
        return

    example = TEST_CASES["python_developer"]
    print("No input provided. Running the default demo case...\n")
    result = execute_workflow(example)
    print(result)


if __name__ == "__main__":
    main()
