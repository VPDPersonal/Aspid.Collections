---
title: Synchronized views
---

Use `CreateSync` to convert each source item into something a consumer needs. The view contains the converted initial contents as soon as the call returns. Further source collection changes update the view automatically.

## Project a list

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

using var scores = new ObservableList<int> { 12, 30 };
using var labels = scores.CreateSync(score => $"{score} XP");

scores.Move(0, 1);
scores[1] = 15;
Console.WriteLine(string.Join(", ", labels)); // 30 XP, 15 XP
```

Edit the source list. The read-only projection keeps the same order. Moving an entry moves its existing projection. Replacing an entry runs the converter for the new value. When a mutable source object changes internally, arrange for its view to observe that object or explicitly replace the collection entry.

## Match the projection to the collection

Lists return `IReadOnlyObservableListSync<T>` with indexed access. Dictionaries keep their keys and project the values into an `IReadOnlyObservableDictionarySync<TKey, T>`. Queues, stacks and hash sets expose an `IReadOnlyObservableCollectionSync<T>` for enumeration and subscription. For a hash set, distinct source items must map to distinct results.

## Release converted items

Pass `isDisposable: true` when each converted item is owned by the view and implements `IDisposable`. For a custom release action, pass a second delegate, for example `models.CreateSync(CreateRow, ReturnRowToPool)`. Removing a row from the source releases its projection. Disposing the view detaches it and releases the remaining projections. See [ownership and cleanup](./lifetime.md).
