# Aspid.Collections

Observable collections library for Unity and .NET. Consumed as a Unity
package (`com.aspid.collections`). Provides covariant observable
collections with synchronization, filtering, and sorting.

## Package Info

- **Package name**: `com.aspid.collections` (`package.json`, v1.0.1)
- **Unity**: 2022.3+
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
│   ├── Events/                                 # IObservableEvents + SplitByEvents
│   ├── Extensions/                             # ObservableListExtensions
│   ├── Filtered/                               # FilteredList + CreateFiltered
│   └── Synchronizer/                           # Observable*Sync + CreateSync
└── Tests/Observable/                           # EditMode tests (UTF)
    └── Helpers/
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
| `Aspid.Collections.Observable.Tests.asmdef` | Unity Test Framework tests |

## Testing

Tests live in `Tests/Observable/` and run via Unity Test Runner
(EditMode). Convention: one `FooTests.cs` per collection/extension (e.g.
`ObservableListTests.cs`, `CreateSyncDictionaryTests.cs`,
`FilteredListTests.cs`). Shared helpers in `Tests/Observable/Helpers/`.

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

## Pointers

- `README.md` / `README_RU.md` — full public API reference with
  examples (collections, events, sync, filter, sample patterns).
- Parent framework CLAUDE.md: `../../../../CLAUDE.md` (relative to this
  file, resolves to `Projects/Aspid.MVVM/CLAUDE.md`) — Aspid.MVVM-wide
  context and conventions.
