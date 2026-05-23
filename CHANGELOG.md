# Changelog

All notable changes to **Aspid.Collections** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- `FilteredList<T>`: incremental dispatch of source `Add`/`Remove` (single and batch), `Replace` and `Move` (single) — no full rebuild on every event for the common cases. Reset and batch `Replace`/`Move` still fall back to a full `Update()`.
- `ObservableList<T>.RemoveRange(int startIndex, int count)` and the protected `OnRemovedRange(in IReadOnlyList<T>)` hook. `ObservableListSync<TFrom, TTo>` forwards source batch removes through it.
- Batch propagation in `ObservableDictionarySync<TKey, TFrom, TTo>` and `ObservableHashSetSync<TFrom, TTo>` for `Add` / `Remove` (and `Replace` on Dictionary) — previously threw `NotImplementedException`.
- `CollectionChangedEvent<T>`: re-entrancy-safe via copy-on-write of the subscriber list during `Invoke`; a lazy `HashSet<Handler>` tracks unsubscribes during the invoke chain for O(1) skip of removed handlers.
- Performance benchmark scaffold under `Tests/Observable/Performance/`, gated by the `ASPID_COLLECTIONS_PERFORMANCE_TESTING` define.
- Aspid script icon at `Editor/Resources/Icons/aspid_icon_medium_green_1022x1011.png`; runtime and test `.cs.meta` files point to it so scripts surface the project icon in the Unity Project view.
- `Samples~/` directory with five importable samples (`package.json` `samples` manifest): basic change-notification dispatch, model→view sync via `CreateSync`, chained `FilteredList` for search+category, `ObservableDictionary` as a keyed table, and `SplitByEvents` as an analytics tap. Each sample ships its own `Aspid.Collections.Samples.*` asmdef.

### Changed
- `ObservableQueueSync` / `ObservableStackSync` / `ObservableDictionarySync`: unsupported actions now throw `NotSupportedException` with a descriptive message instead of `NotImplementedException`.
- Release workflow excludes `CLAUDE.md` / `CLAUDE.md.meta` from the published UPM tree (alongside `.github`).

### Fixed
- `ObservableStackSync.OnPoppedRange` and `ObservableQueueSync.OnDequeuedRange` overrides recursed into themselves via C# overload resolution, causing `StackOverflowException` on `Dispose` and any batch dequeue/pop.
- `ObservableDictionarySync.OnReplaced` passed `newItem.Value` to `OnRemoved`, disposing/notifying the freshly inserted value instead of the discarded one.
- `FilteredList.ApplyMoveSingle` did not re-seat the moved entry when a `Comparer` was set; for comparer-equal items the source-index tie-break could drift from a fresh `Update()` rebuild.
- `ObservableHashSetSync.AddOne` ignored the `Add` return value, silently corrupting `_sync` on a non-injective converter — now throws `InvalidOperationException`. `RemoveOne` uses `TryGetValue` instead of the indexer to avoid `KeyNotFoundException` inside the source's `Invoke`.
- `ObservableList<T>.RemoveRange` validates `count < 0` → `ArgumentOutOfRangeException` (previously `OverflowException` from `new T[-1]`).
- `FilteredList` 3-arg constructor: source subscription now happens after `_filter` / `_comparer` are set, closing a subscribe-before-init window.

## [1.0.2] — 2026-05-18

### Added
- Release workflow that publishes a UPM branch and per-version `upm/*` tags.

### Changed
- README: replaced the Installation section with a UPM-only Integration section.
- Refreshed `.meta` files.

## [1.0.1] — 2026-05-18

Initial tracked release.
