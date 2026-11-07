---
name: asp-collections-release
description: Prepare an Aspid.Collections release PR, publish its tag after approval, or verify the published GitHub and Unity Package Manager release. Use for an explicit release request with a version.
---

# Release Aspid.Collections

Take the requested version, stripping a leading `v`. Ask for the version if it is missing. Use a SemVer version accepted by [the release workflow](../../../.github/workflows/release.yml). Stable versions go to the `upm` branch, and prereleases go to `upm-preview`. Both use the immutable `upm/<version>` tag.

## Choose the phase

Fetch `origin/main` and check the remote for the exact `refs/tags/v<version>` ref. If the tag exists, verify publication. Otherwise, find the release PR for that version by searching PRs targeting `main` and checking the manifest and changelog in the diff. If it is open, report its link and checks and stop. If it is merged, proceed to the tag phase. If `origin/main` already has the version and no matching merged PR can be verified, stop and report the discrepancy.

## Prepare the release PR

1. Start from a clean checkout. Verify that the requested version is greater than the version in `origin/main:Aspid.Collections/Packages/tech.aspid.collections/package.json` using SemVer precedence. Create a release branch from `origin/main` using the repository branch naming guidance.
2. Set the version in `Aspid.Collections/Packages/tech.aspid.collections/package.json`. In `CHANGELOG.md`, keep `## [Unreleased]` and add a nonempty `## [<version>] — YYYY-MM-DD` section. Move applicable Unreleased notes there and fill gaps from the commits since the preceding release tag. Link each entry to its corresponding merged PRs when available.
3. Check that the manifest and changelog versions match, run `git diff --check`, and run `dotnet test DotnetTests/Aspid.Collections.Tests.csproj --configuration Release`. Stop on a failed check and report the cause.
4. Commit the release changes with `asp-commit` and open a draft PR into `main` with `asp-pr`. Report the PR link, selected channel, and checks. Wait for the release PR to merge before proceeding.

## Publish the tag

1. Get the merged PR's `mergeCommit.oid` with `gh pr view <number> --json state,mergeCommit`. Verify that the SHA is in `origin/main`, its manifest and changelog match the requested version, and the relevant checks passed.
2. Present the version, channel, and SHA. Get explicit approval to publish that tag unless the user has already authorized that exact publication. Pushing the tag starts the Release workflow.
3. Recheck the remote release and UPM tag refs. If the release tag already exists, verify publication. If only the UPM tag exists, stop and investigate. Otherwise create and push the annotated tag on the verified merge SHA.

   ```sh
   git tag -a "v<version>" -m "Release v<version>" <merge-sha>
   git push origin "refs/tags/v<version>"
   ```

## Verify publication

1. Find the Release run with `gh run list --workflow release.yml --branch "v<version>" --event push --limit 1`. Allow a short bounded wait for the run to appear. If publication started through manual dispatch, find the matching run by version input and release SHA. Wait for an active run with `gh run watch <id> --exit-status`.
2. On failure, inspect `gh run view <id> --log-failed` and report the failed step and which refs exist. The workflow refuses to overwrite an existing UPM tag, so check the remote state before retrying. Leave published tags intact.
3. Verify the GitHub release URL and its prerelease flag with `gh release view v<version> --json url,isPrerelease`. Check the package version at `upm/<version>` and verify that the selected UPM branch points at that tag for a fresh publication. When checking an older release, the channel branch may already point at a newer release.
4. Report the release URL, UPM tag, and verification result in Russian.
