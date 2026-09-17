---
name: commit-message-format
description: 'Use when writing or generating a git commit message for this repo. Enforces the 50/72 rule: subject line max 50 chars, full body kept to around 72 chars total.'
---

# Commit Message Format (50/72 Rule)

## Rules

1. **Subject line**: max 50 characters, imperative mood ("Add", not "Added"/"Adds"), no trailing period.
2. **Blank line**: exactly one blank line between subject and body.
3. **Body**: keep the full body to around 72 characters total. Use it to explain *what* and *why*, not *how*.
4. Only include a body when it adds context beyond the subject; otherwise a single subject line is fine.

## Template

```
Short imperative summary (<=50 chars)

Explain the motivation and context for the change, keeping the
full body to around 72 characters total. Focus on why the
change was made rather than restating the diff.
```

## Procedure

1. Draft the subject line first; trim until it is 50 characters or fewer.
2. If more explanation is needed, add a blank line then the body.
3. Keep the full body to around 72 characters total, trimming or tightening wording as needed.
4. Re-check both limits before finalizing the message.
