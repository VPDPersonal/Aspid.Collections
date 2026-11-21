---
title: ObservableList
---

Use `ObservableList<T>` for ordered items with stable positions between edits. Add, remove, replace and move events carry the affected indices, so a UI can update individual rows.

## Reorder an inventory

```csharp
using Aspid.Collections.Observable;

using var slots = new ObservableList<string>();
slots.AddRange("Potion", "Key", "Map");
slots.Move(2, 0);
slots[1] = "Elixir";
slots.RemoveRange(1, 2);
```

Only `"Map"` remains. Subscribers see a batch `Add`, a single `Move`, a single `Replace` and a batch `Remove`, in that order.

## Operations

| Method | Behavior |
| --- | --- |
| `Add / Insert` | Append or insert one item. |
| `AddRange / InsertRange` | Accept an array, individual arguments or an `IReadOnlyList<T>`. A nonempty range raises one event. |
| `Move(oldIndex, newIndex)` | Move one item to its final index. Equal valid indices leave the list unchanged. |
| `Remove / RemoveAt / RemoveRange` | Remove the first matching item, an indexed item or a contiguous range. |
| `IndexOf / Contains` | Find a position or test membership. An absent item has index −1. |
| `Clear` | Empty the list and emit `Reset`. |

[Implementation](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableList.cs)
