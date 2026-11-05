# Rules for Claude in this repository

## Always

- Follow the root `CLAUDE.md`.
- Write in English. Keep code identifiers as they are.

## When you review a pull request

Check, in this order:

1. Correctness: bugs and missed edge cases, with the input that breaks.
2. Data and code in user projects:
   - a renamed or moved file keeps its `.meta` GUID;
   - a renamed serialized field gets `[FormerlySerializedAs]`, a renamed or moved type stored through
     `[SerializeReference]` gets `[MovedFrom]`: without them user projects lose saved data;
   - no C# feature or .NET API that Unity 2021.3 lacks (`package.json` minimum);
   - no `UnityEngine` or `UnityEditor` reference in `Runtime/`: it sets `noEngineReferences: true`.
3. Public API: XML docs on each new or changed member, a CHANGELOG entry that describes the change as a user sees it,
   and `README.md` and `README_RU.md` updated when they describe the API.
4. Workflows: actions pinned to a full commit SHA, only the permissions a job uses, and no `pull_request_target` job
   that checks out or runs the code of the PR.

Do not report:

- Style and naming that `CLAUDE.md` does not cover.

On a review of the commits after your last review, check only those commits and whether your earlier findings are
fixed. Do not repeat a finding that is fixed or that the author answered with a reason.

## Format

- At most 8 findings, most severe first, each in an inline comment on its line that starts with a severity tag:
  - `[blocking]`: the PR must not merge until it is fixed;
  - `[minor]`: an improvement that can wait.
- Post the main comment last, as a new comment with `gh pr comment`, for a requested review too: an agent waits for
  it, and an edit of an earlier comment does not notify it.
- Start the main comment with exactly one line `Verdict: <N> blocking, <M> minor` (`Verdict: 0 blocking, 0 minor`
  without findings): the agent reads it. Put at most 2 short sentences after it.
- Do not praise.
- Do not write the literal trigger phrase (at-sign + "claude"): it starts the workflow again.
