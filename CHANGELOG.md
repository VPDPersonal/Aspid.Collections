# Changelog

All notable changes to **Aspid.Collections** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Bilingual Docusaurus documentation website in `Website/`, with the Aspid.FastTools theme, local search, interactive collection illustration, generated runtime API reference, sample walkthroughs and previews, and a GitHub Pages workflow.

### Changed
- Simplified the five sample interfaces, moved diagnostics into a collapsed Events panel, and added switchable light/dark palettes with paired website previews.
- Replaced the introductory samples with five self-contained interactive UI Toolkit scenes: Squad Roster, Item Browser, Crafting Queue, Match Scoreboard, and Zone Occupancy. Each includes local fonts, live events and metrics, a guided exercise, and a walkthrough. Together they demonstrate all five collection types, synchronization ownership, chained filtering, batch notifications, and disposable event subscriptions.

## [1.1.1] — 2026-11-06

### Changed
- Added license and search keywords to the Unity package metadata and removed the dependency on the unpublished Unity Test Framework version. ([#34](https://github.com/VPDPersonal/Aspid.Collections/pull/34), [#20](https://github.com/VPDPersonal/Aspid.Collections/pull/20))
- Added a .NET CI job and expanded regression coverage for observable collections, filtered views, and synchronization wrappers. ([#35](https://github.com/VPDPersonal/Aspid.Collections/pull/35))
- Hardened release input handling and excluded `.gitignore` from published UPM trees. Prereleases now publish to `upm-preview`. ([#29](https://github.com/VPDPersonal/Aspid.Collections/pull/29), [#11](https://github.com/VPDPersonal/Aspid.Collections/pull/11))
- Updated the README layout and examples. ([#31](https://github.com/VPDPersonal/Aspid.Collections/pull/31), [#32](https://github.com/VPDPersonal/Aspid.Collections/pull/32), [#33](https://github.com/VPDPersonal/Aspid.Collections/pull/33))

### Fixed
- Corrected subscription and unsubscription through covariant collection and `SplitByEvents` views. ([#25](https://github.com/VPDPersonal/Aspid.Collections/pull/25), [#26](https://github.com/VPDPersonal/Aspid.Collections/pull/26))
- Made `FilteredList.Count` reflect the current view and cleaned up the source subscription if initialization fails. ([#23](https://github.com/VPDPersonal/Aspid.Collections/pull/23), [#35](https://github.com/VPDPersonal/Aspid.Collections/pull/35))
- Preserved source comparers in dictionary and hash set synchronization and preserved the order of an existing stack when creating a synchronized view. ([#17](https://github.com/VPDPersonal/Aspid.Collections/pull/17), [#15](https://github.com/VPDPersonal/Aspid.Collections/pull/15))
- Prevented synchronization wrappers from releasing an item that a replacement keeps in use, or releasing hash set items twice on repeated disposal. ([#28](https://github.com/VPDPersonal/Aspid.Collections/pull/28), [#27](https://github.com/VPDPersonal/Aspid.Collections/pull/27))
- Validated list move indexes and queue and stack output buffer sizes before modifying the collection. ([#16](https://github.com/VPDPersonal/Aspid.Collections/pull/16), [#18](https://github.com/VPDPersonal/Aspid.Collections/pull/18))
- Suppressed notifications for empty batches and no-op moves, and made list range additions and insertions safe when the input is a live view of the same list. ([#22](https://github.com/VPDPersonal/Aspid.Collections/pull/22), [#35](https://github.com/VPDPersonal/Aspid.Collections/pull/35))
- Reported the stored item in removal notifications when the caller supplies an equal item. ([#19](https://github.com/VPDPersonal/Aspid.Collections/pull/19))

## [1.1.0] — 2026-05-23

### Added
- `FilteredList<T>`: incremental dispatch of source `Add`/`Remove` (single and batch), `Replace` and `Move` (single) — no full rebuild on every event for the common cases. Reset and batch `Replace`/`Move` still fall back to a full `Update()`.
- `ObservableList<T>.RemoveRange(int startIndex, int count)` and the protected `OnRemovedRange(in IReadOnlyList<T>)` hook. `ObservableListSync<TFrom, TTo>` forwards source batch removes through it.
- Batch propagation in `ObservableDictionarySync<TKey, TFrom, TTo>` and `ObservableHashSetSync<TFrom, TTo>` for `Add` / `Remove` (and `Replace` on Dictionary) — previously threw `NotImplementedException`.
- `CollectionChangedEvent<T>`: re-entrancy-safe via copy-on-write of the subscriber list during `Invoke`; a lazy `HashSet<Handler>` tracks unsubscribes during the invoke chain for O(1) skip of removed handlers.
- Performance benchmark scaffold under `Tests/Runtime/Observable/Performance/`, gated by the `ASPID_COLLECTIONS_PERFORMANCE_TESTING` define.
- Aspid script icon at `Editor/Resources/Icons/aspid_icon_medium_green_1022x1011.png`; runtime and test `.cs.meta` files point to it so scripts surface the project icon in the Unity Project view.
- `Samples~/` directory with five importable samples (`package.json` `samples` manifest): basic change-notification dispatch, model→view sync via `CreateSync`, chained `FilteredList` for search+category, `ObservableDictionary` as a keyed table, and `SplitByEvents` as an analytics tap. Each sample ships its own `Aspid.Collections.Samples.*` asmdef.
- `package.json` metadata: `unityRelease` field, a `com.unity.test-framework@1.6.0` dependency, and `author.url` / `changelogUrl` / `documentationUrl` / `licensesUrl` entries.

### Changed
- **BREAKING** — package renamed from `com.aspid.collections` to `tech.aspid.collections`. Consumers must update the identifier in their project's `Packages/manifest.json` (and any scoped-registry / Git-URL references) when upgrading from 1.0.x.
- Tests reorganized under `Tests/Runtime/Observable/` to match Unity's standard runtime/editor split. The test assembly is renamed from `Aspid.Collections.Observable.Tests` to `Aspid.Collections.Tests` (now with an empty `rootNamespace`), and the `Editor`-only `includePlatforms` constraint is dropped from both the test and performance asmdefs so suites compile and run on player platforms too.
- **BREAKING** — `ASPID_COLLECTIONS_PERFORMANCE_TESTING` removed from the performance asmdef's `defineConstraints`. Without `com.unity.test-framework.performance` installed the perf asmdef now **fails to compile** instead of being silently excluded; the define is still auto-set via `versionDefines` when the package is present.
- `ObservableQueueSync` / `ObservableStackSync` / `ObservableDictionarySync`: unsupported actions now throw `NotSupportedException` with a descriptive message instead of `NotImplementedException`.
- Release workflow excludes `CLAUDE.md` / `CLAUDE.md.meta` from the published UPM tree (alongside `.github`).

### Fixed
- `ObservableStackSync.OnPoppedRange` and `ObservableQueueSync.OnDequeuedRange` overrides recursed into themselves via C# overload resolution, causing `StackOverflowException` on `Dispose` and any batch dequeue/pop.
- `ObservableDictionarySync.OnReplaced` passed `newItem.Value` to `OnRemoved`, disposing/notifying the freshly inserted value instead of the discarded one.
- `FilteredList.ApplyMoveSingle` did not re-seat the moved entry when a `Comparer` was set; for comparer-equal items the source-index tie-break could drift from a fresh `Update()` rebuild.
- `ObservableHashSetSync.AddOne` ignored the `Add` return value, silently corrupting `_sync` on a non-injective converter — now throws `InvalidOperationException`. `RemoveOne` uses `TryGetValue` instead of the indexer to avoid `KeyNotFoundException` inside the source's `Invoke`.
- `ObservableList<T>.RemoveRange` validates both `startIndex < 0` and `count < 0` → `ArgumentOutOfRangeException` before taking the lock (previously `OverflowException` from `new T[-1]` for `count`, and `IndexOutOfRangeException` from the per-item read for `startIndex`).
- `FilteredList` 3-arg constructor: source subscription now happens after `_filter` / `_comparer` are set, closing a subscribe-before-init window.

## [1.0.2] — 2026-05-18

### Added
- Release workflow that publishes a UPM branch and per-version `upm/*` tags.

### Changed
- README: replaced the Installation section with a UPM-only Integration section.
- Refreshed `.meta` files.

## [1.0.1] — 2026-05-18

Initial tracked release.
