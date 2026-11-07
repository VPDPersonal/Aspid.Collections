# Change notifications

## The payload

Handlers receive `INotifyCollectionChangedEventArgs<T>` without a sender. The concrete payload is the package's own readonly struct `NotifyCollectionChangedEventArgs<T>`; only the action enum comes from `System.Collections.Specialized`.

```csharp
using System;
using System.Collections.Specialized;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
items.CollectionChanged += change =>
{
    if (change.Action == NotifyCollectionChangedAction.Reset)
    {
        Console.WriteLine("Reset");
        return;
    }

    var count = change.IsSingleItem
        ? 1
        : (change.NewItems ?? change.OldItems)!.Count;
    Console.WriteLine($"{change.Action}: {count}");
};
items.AddRange("Potion", "Elixir"); // Add: 2
```

Check `Action == Reset` before reading item fields. `Reset` has no item payload even though `IsSingleItem` is true. Then use `IsSingleItem` to choose `NewItem` / `OldItem` or `NewItems` / `OldItems`.

## Actions and indexes

| Action | Payload | Index meaning |
| --- | --- | --- |
| Add | New item(s) | Insertion index; the old index is -1 |
| Remove | Removed item(s) | Previous index; the new index is -1 |
| Replace | Old and new item(s) | Same index in both fields |
| Move | The same item in old and new fields | Position before and after the move |
| Reset | No items | Both indexes are -1 |

Dictionary and HashSet always use -1 indexes. Their payloads identify keys/items instead. Queue enqueue indexes start at the old Count; dequeue starts at zero. Stack operations use index zero. `PushRange` reports input order, while the last input item becomes the new stack top.

Empty range operations and no-op list moves do not notify. `Clear()` sends Reset, including when already empty. Retained batch payloads are not guaranteed immutable snapshots: copy items before deferring work.

## Separate callbacks

`SplitByEvents` returns a disposable `IObservableEvents<T>`. Each item callback receives a list, even for a single-item operation. Its replacement callback takes old items first, then new items. Creation subscribes to future changes; it does not replay existing content. [Zone Occupancy](/tutorials/zone-occupancy) demonstrates detaching one tap while another consumer stays connected.

Notifications are synchronous; see [exception and reentrancy limits](lifecycle.md).
