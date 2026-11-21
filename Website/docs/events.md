---
title: Change notifications
---

Subscribe to `CollectionChanged` to learn what changed in one edit. The handler receives one argument implementing `INotifyCollectionChangedEventArgs<T>`. Its `Action` uses the .NET enum `NotifyCollectionChangedAction`.

## Read the payload

Check `Action` first. For item changes, `IsSingleItem` selects the appropriate fields. On a list the starting index identifies the position of the first affected item.

| Action | Data to read |
| --- | --- |
| `Add` | `NewItem` or `NewItems`, `NewStartingIndex` |
| `Remove` | `OldItem` or `OldItems`, `OldStartingIndex` |
| `Replace` | Old and new values at the affected position |
| `Move` | Moved item, `OldStartingIndex` and `NewStartingIndex` |
| `Reset` | Read the current collection again |

```csharp
using System;
using System.Collections.Specialized;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
items.CollectionChanged += change =>
{
    if (change.Action != NotifyCollectionChangedAction.Add) return;
    if (change.IsSingleItem)
        Console.WriteLine(change.NewItem);
    else
        Console.WriteLine(string.Join(", ", change.NewItems!));
};

items.Add("Shield");
items.AddRange("Key", "Potion");
```

Each line at the end triggers one callback. The second callback receives both names in `NewItems`. For a dictionary the payload contains key/value pairs. Look up dictionary entries by key.

## Give each action a handler

```csharp
using var changes = items.SplitByEvents(
    added: (batch, index) => Console.WriteLine($"+{batch.Count} @ {index}"),
    moved: (batch, from, to) => Console.WriteLine($"{from} -> {to}"),
    reset: () => Console.WriteLine("Reset"));

items.Add("Map");
items.Move(0, 1);
```

Add this block after the preceding example. `SplitByEvents` wraps even one item in a list. The `replaced` callback receives old items, new items and the index, in that order. Keep the returned handle while you need callbacks and dispose it to unsubscribe.
