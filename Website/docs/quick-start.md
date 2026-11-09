# Quick start

## One source, two consumers

Create a collection, then attach a synchronized projection and a filtered view. Their initial content includes the source's existing items.

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using Aspid.Collections.Observable.Synchronizer;

using var scores = new ObservableList<int> { 25, 5, 15 };
using var labels = scores.CreateSync(score => $"Score {score}");
using var visible = scores.CreateFiltered(score => score >= 10);

scores.Add(30);
Console.WriteLine(labels[3]); // Score 30
Console.WriteLine(visible.Count); // 3

scores.Move(3, 0);
Console.WriteLine(labels[0]); // Score 30
```

`using var` disposes in reverse declaration order: filtered view, synchronized projection, then source. The source keeps only data; consumers decide how to present it.

## Choose your next step

1. [Choose a collection](collections.md) for ordering, unique membership or stable keys.
2. [Handle events](events.md) if you need exact items and indexes.
3. [Create synchronized views](synchronization.md) if one source item owns one destination object.
4. [Filter and sort](filtering.md) for search results and read-only projections.

## Try it in Unity

Import [Squad Roster](/tutorials/squad-roster). Recruit a pair, rotate the members, then replace one. Its view IDs and counters show which card objects were created, kept and released.

For a model property change, use [Item Browser](/tutorials/item-browser): edit a price and explicitly refresh the filter chain. The collections observe structural edits, not arbitrary properties inside items.
