# Aspid.Collections

Observable collections library for Unity and .NET. Consumed as a Unity
package (`tech.aspid.collections`). Provides covariant observable
collections with synchronization, filtering, and sorting.

## Package Info

- **Package name**: `tech.aspid.collections`, embedded under `Aspid.Collections/Packages/`.
- **Unity**: The package manifest declares `2021.3` as the minimum.
  The development project uses Unity `6000.6.5f1`.
- **Engine dependency**: none — `Aspid.Collections.Observable.asmdef` sets
  `noEngineReferences: true`. Runtime is pure C# and must stay that way
  so the package works in non-engine assemblies too.

## Directory Layout

```
.
├── Aspid.Collections/                          # Unity development project
│   ├── Assets/
│   ├── Packages/
│   │   ├── manifest.json
│   │   └── tech.aspid.collections/             # Embedded UPM package
│   │       ├── package.json
│   │       ├── Editor/
│   │       ├── Runtime/Observable/
│   │       │   ├── Events/
│   │       │   ├── Extensions/
│   │       │   ├── Filtered/
│   │       │   └── Synchronizer/
│   │       ├── Tests/Runtime/Observable/
│   │       └── Samples~/
│   └── ProjectSettings/
├── DotnetTests/                                # .NET test harness for the package
└── Documentation~/                            # Repository README assets
```

The package paths below are relative to `Aspid.Collections/Packages/tech.aspid.collections/`.
The release workflow publishes this subtree at the root of the UPM distribution refs.

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
| `Aspid.Collections.Samples.*.asmdef` (5 of them, one per sample folder under `Samples~/`) | Importable samples. Unlike the runtime, samples *do* reference `UnityEngine` (`noEngineReferences: false`) — they use `MonoBehaviour`, `GameObject`, `Debug.Log`. They are not compiled in-place: Unity copies a sample into `Assets/Samples/...` on import (Package Manager UI), and only then the asmdef participates in the consuming project's build. |

## Testing

Open `Aspid.Collections/` in Unity Hub and run the tests and benchmarks in the Test Runner's PlayMode tab.
From the repository root, run `dotnet test DotnetTests/Aspid.Collections.Tests.csproj --configuration Release`
for the engine-free unit tests used by CI.

Tests live in `Tests/Runtime/Observable/`. The asmdefs do not restrict
`includePlatforms`, so Unity discovers the suites as PlayMode tests. Convention: one
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
- **Samples live in `Samples~/` and are listed in `package.json`.** Each
  importable sample is declared in the `samples` array of `package.json`
  with a `displayName`, `description`, and `path`. The `~` suffix tells
  Unity to skip the folder during asset import in the package itself —
  the contents only land in `Assets/Samples/<package>/<version>/<sample>/`
  after the user clicks "Import" in the Package Manager UI. Adding a new
  sample means **both**: a new subfolder under `Samples~/` *and* a new
  entry in the `samples` manifest. Sample asmdefs reference
  `Aspid.Collections.Observable` and freely use `UnityEngine` types —
  this is the one place in the package where engine references are
  allowed.
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
