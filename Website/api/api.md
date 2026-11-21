---
slug: /
title: API reference
---

The runtime assembly is `Aspid.Collections.Observable`. Start with the shape of your data, then choose which operations to expose to consumers.

## Collection types

| Type | Choose it for |
| --- | --- |
| [ObservableList](./list.md) | Indexed rows, reordering and range edits |
| [ObservableDictionary](./dictionary.md) | Records identified by key |
| [ObservableHashSet](./set.md) | Unique members and equality rules |
| [ObservableQueue](./queue.md) | Work processed in arrival order |
| [ObservableStack](./stack.md) | Undo history and most-recent-first access |

## Interfaces

Expose `IObservableCollection<T>` for enumeration, `Count`, `SyncRoot` and `CollectionChanged`. Use `IReadOnlyObservableList<T>` for indexed access or `IReadOnlyObservableDictionary<TKey, TValue>` for keyed lookups. Both the collection interface and the list interface are covariant. A consumer expecting a collection of base-class instances can receive a collection of derived-class instances.

```csharp
using Aspid.Collections.Observable;

using var names = new ObservableList<string> { "Ada" };
IReadOnlyObservableList<string> rows = names;
IObservableCollection<object> entries = rows;
```

A consumer holding `rows` can read the current name and subscribe to edits. The code that owns `names` can still modify the list and is responsible for disposal.

## Extensions and return types

| Import | Operation | Result |
| --- | --- | --- |
| `Aspid.Collections.Observable` | `SplitByEvents` | An `IObservableEvents<T>` handle with callbacks for each action. Dispose the handle to unsubscribe. |
| `Aspid.Collections.Observable.Extensions` | `Swap(index1, index2)` | Exchanges two list positions using move operations. |
| `Aspid.Collections.Observable.Filtered` | `CreateFiltered` | A `FilteredList<T>` over an `IReadOnlyList<T>`. Expose `IReadOnlyFilteredList<T>` to consumers that only read the result. Its change event has no arguments. |
| `Aspid.Collections.Observable.Synchronizer` | `CreateSync` | A disposable observable projection. Lists retain indexed access, dictionaries retain keyed access, and the other three collections return `IReadOnlyObservableCollectionSync<T>`. |

[Browse runtime source](https://github.com/VPDPersonal/Aspid.Collections/tree/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable)
