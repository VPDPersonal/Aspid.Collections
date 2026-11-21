---
title: Filtering and sorting
---

Use `CreateFiltered` when several screens need different slices of the same list. The view keeps source items and lets you address the visible results by their own indexes.

## Keep the highest scores

```csharp
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var scores = new ObservableList<int> { 12, 30, 5 };
var descending = Comparer<int>.Create((left, right) => right.CompareTo(left));
using var leaders = scores.CreateFiltered(score => score >= 10, descending);

scores.Add(18);
Console.WriteLine(string.Join(", ", leaders)); // 30, 18, 12

leaders.Filter = score => score >= 20;
Console.WriteLine(string.Join(", ", leaders)); // 30
```

Assigning `Filter` or `Comparer` rebuilds the view immediately. Set `Filter = null` to include all items, or `Comparer = null` to display them in source order. The view exposes a parameterless `CollectionChanged` event. Read the view again inside its handler.

## Refresh after changing a rule or an item

A captured value can change without assigning a new predicate. Call `Update()` when that happens, after editing a field used by a rule, or after editing a plain `List<T>` used as the source.

```csharp
int threshold = 10;
using var qualified = scores.CreateFiltered(score => score >= threshold);
threshold = 25;
qualified.Update();
```

## Combine views

Call `CreateFiltered` on a filtered view to add a second independent rule, for example a search query on top of a category. Expose `IReadOnlyFilteredList<T>` to consumers that only display the result. Build, read and modify the chain on the same thread as the source edits. [Release the outermost view first](./lifetime.md).
