---
title: ObservableQueue
---

Use `ObservableQueue<T>` to process items in arrival order, such as pending game commands.

## Read a batch

```csharp
using Aspid.Collections.Observable;

using var commands = new ObservableQueue<string>();
commands.EnqueueRange(new[] { "Walk", "Jump", "Turn" });
var next = new string[2];
commands.DequeueRange(next);
bool hasMore = commands.TryPeek(out var remaining);
```

The buffer contains `"Walk"`, then `"Jump"`. The remaining item is `"Turn"`. Enumeration starts with the oldest pending item.

## Operations

| Method | Behavior |
| --- | --- |
| `Enqueue / EnqueueRange` | Insert one item or an array / read-only list. A nonempty range produces one addition event. |
| `Dequeue / TryDequeue` | Remove the next item. The Try variant returns false when empty. |
| `DequeueRange` | Fill the caller’s array in removal order. An oversized buffer raises `InvalidOperationException` before any removal. |
| `Peek / TryPeek` | Read the next item while leaving it in place. |
| `ToArray / TrimExcess` | Copy items in removal order or trim unused storage. |
| `Clear` | Empty the collection and emit `Reset`. |

[Implementation](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableQueue.cs)
