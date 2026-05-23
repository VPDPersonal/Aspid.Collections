# Aspid.Collections

Observable collections library for Unity and .NET. Consumed as a Unity
package (`tech.aspid.collections`). Provides covariant observable
collections with synchronization, filtering, and sorting.

## Package Info

- **Package name**: `tech.aspid.collections` (`package.json`, v1.0.2)
- **Unity**: `package.json` declares `2021.3` as the manifest minimum; the
  active development target is 2022.3+ (matches the parent MVVM project).
- **Engine dependency**: none — `Aspid.Collections.Observable.asmdef` sets
  `noEngineReferences: true`. Runtime is pure C# and must stay that way
  so the package works in non-engine assemblies too.

## Directory Layout

```
Collections/
├── Runtime/Observable/                         # Pure C# runtime
│   ├── ObservableList.cs                       # Core collections
│   ├── ObservableDictionary.cs
│   ├── ObservableHashSet.cs
│   ├── ObservableQueue.cs
│   ├── ObservableStack.cs
│   ├── IObservableCollection.cs                # Core interfaces
│   ├── IReadOnlyObservableList.cs
│   ├── IReadOnlyObservableDictionary.cs
│   ├── NotifyCollectionChangedEventArgs.cs     # Custom struct, not BCL
│   ├── INotifyCollectionChangedEventArgs.cs
│   ├── NotifyCollectionChangedEventHandler.cs
│   ├── CollectionChangedEvent.cs
│   ├── Events/                                 # IObservableEvents, ObservableCollectionEvents
│   │   └── Extensions/                         # SplitEventsExtensions
│   ├── Extensions/                             # ObservableListExtensions
│   ├── Filtered/                               # FilteredList, IReadOnlyFilteredList
│   │   └── Extensions/                         # CreateFilteredExtensions
│   └── Synchronizer/                           # Observable*Sync, IReadOnly*Sync
│       └── Extensions/                         # CreateSyncExtensions
└── Tests/Runtime/Observable/                   # Unity Test Framework tests
    ├── Helpers/
    └── Performance/                            # Optional perf benchmarks (gated)
```

## Namespaces

- `Aspid.Collections.Observable` — core collections, interfaces, events.
- `Aspid.Collections.Observable.Filtered` — `FilteredList`, `CreateFiltered`.
- `Aspid.Collections.Observable.Synchronizer` — `Observable*Sync`,
  `CreateSync`.

## Assemblies

| asmdef | Purpose |
|--------|---------|
| `Aspid.Collections.Observable.asmdef` | Runtime (`noEngineReferences: true`) |
| `Aspid.Collections.Tests.asmdef` | Unity Test Framework tests (`noEngineReferences: true`, compiled when `UNITY_INCLUDE_TESTS` is defined) |
| `Aspid.Collections.Observable.PerformanceTests.asmdef` | Perf benchmarks; compiled when `UNITY_INCLUDE_TESTS` is defined and references `Unity.PerformanceTesting` directly, so the asmdef also requires `com.unity.test-framework.performance` to be installed. `ASPID_COLLECTIONS_PERFORMANCE_TESTING` is auto-set via `versionDefines` for code-level `#if`-gating. |

## Testing

Tests live in `Tests/Runtime/Observable/` and run via Unity Test Runner
(the asmdef does not pin `includePlatforms`, so tests compile for every
build target and can run as EditMode or PlayMode). Convention: one
`FooTests.cs` per collection/extension (e.g. `ObservableListTests.cs`,
`CreateSyncDictionaryTests.cs`, `FilteredListTests.cs`). Shared helpers
in `Tests/Runtime/Observable/Helpers/`.

## Conventions (delta over parent CLAUDE.md)

- **Disposable ownership.** All observable collections and their
  wrappers (`*Sync`, `FilteredList`, `SplitByEvents` handle) implement
  `IDisposable`. `Dispose()` clears content **and** removes event
  subscriptions. The creator of a wrapper owns its lifecycle.
- **Custom event args.** Change notifications use the project's own
  `NotifyCollectionChangedEventArgs<T>` **struct**, not
  `System.Collections.Specialized.NotifyCollectionChangedEventArgs`.
  Distinguish single-item vs. batch operations via `IsSingleItem` /
  `NewItem`-`OldItem` vs. `NewItems`-`OldItems`.
- **Read-only projections.** Sync and filtered wrappers return
  `IReadOnlyObservable*Sync<T>` / `IReadOnlyFilteredList<T>`. Callers
  should expose these, not the underlying mutable collections.

## Gotchas

- **Keep the runtime engine-free.** Do not add `using UnityEngine;` to
  files in `Runtime/Observable/` — `noEngineReferences: true` will
  break the build, and consumers depend on this package working outside
  Unity.
- **FilteredList chaining.** A `FilteredList` can feed another
  `CreateFiltered`. Each wrapper holds a subscription on its source; if
  you drop the reference without calling `Dispose()`, the source keeps
  the chain alive.
- **FilteredList is single-thread.** Source collections are guarded by
  `SyncRoot`, but `FilteredList` itself is not — its internal index map
  is mutated in place from `OnCollectionChanged`. Build / read /
  enumerate it from one thread (the same one that mutates the source).
- **Performance tests need the perf package.**
  `Tests/Runtime/Observable/Performance/` references
  `Unity.PerformanceTesting` directly, so the asmdef only resolves when
  `com.unity.test-framework.performance` is installed in the consuming
  project. The `versionDefines` block sets
  `ASPID_COLLECTIONS_PERFORMANCE_TESTING` when that package is present —
  use it for `#if`-gating inside benchmark code. The compile gate
  itself is just `UNITY_INCLUDE_TESTS` in `defineConstraints`.

## Pointers

- `README.md` / `README_RU.md` — full public API reference with
  examples (collections, events, sync, filter, sample patterns).
- `CHANGELOG.md` — release notes; the **Unreleased** block lists in-flight
  work (incremental FilteredList dispatch, batch propagation in Sync
  wrappers, re-entrancy safety in `CollectionChangedEvent`).
- Parent framework CLAUDE.md: `../../../../CLAUDE.md` (relative to this
  file, resolves to `Projects/Aspid.MVVM/CLAUDE.md`) — Aspid.MVVM-wide
  context and conventions.
