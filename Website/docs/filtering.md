# Filtering and sorting

## Derive a read-only list

`CreateFiltered` accepts `IReadOnlyList<T>` and returns `FilteredList<T>`. Give it a predicate, a comparer or both. It observes an observable list or another filtered list; a plain read-only list needs explicit refreshes.

```csharp
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var scores = new ObservableList<int> { 25, 5, 15 };
var minimum = 10;
using var matching = scores.CreateFiltered(value => value >= minimum);
using var sorted = matching.CreateFiltered(Comparer<int>.Default);

scores.Add(12); // The chain updates automatically.
minimum = 20;
matching.Update(); // Refresh the captured predicate and every downstream view.
Console.WriteLine(string.Join(", ", sorted)); // 25
```

The source order is unchanged. Setting `Filter` or `Comparer` calls `Update()` automatically. For comparer-equal values, the source index breaks ties.

## Refresh the right level

Changes to item properties and captured variables do not emit collection notifications. Call `Update()` on the affected upstream view so the rest of the chain receives the refresh. This is why [Item Browser](/tutorials/item-browser) refreshes its search view after discounting an item.

`CollectionChanged` on a filtered view is an `Action` with no items or indexes. You cannot pass that view directly to `CreateSync`, whose List overload requires `IReadOnlyObservableList<T>`. Rebuild an appropriate UI projection on the filtered view's coarse notification, or use a separate adapter with explicit ownership.

## Incremental work and full rebuilds

A directly observed list supports incremental Add/Remove and single-item Replace/Move handling. Reset and batch Replace/Move fall back to full `Update()`. A downstream filtered list receives the coarse Action and rebuilds. Do not assume every link in a chain updates incrementally.

## Lifetime and threading

Dispose from the downstream view back to its source. `FilteredList.Dispose()` removes subscriptions and its listeners; it does not clear the source or promise an empty index map. Treat the disposed view as unusable. `IReadOnlyFilteredList<T>` itself is not IDisposable, so keep its concrete owner.

Build, mutate and enumerate filtered views on a single thread. Their index maps have no internal locks.
