---
title: ObservableStack
---

Use `ObservableStack<T>` for last-in, first-out processing, such as undo history.

## Read a batch

```csharp
using Aspid.Collections.Observable;

using var commands = new ObservableStack<string>();
commands.PushRange(new[] { "Walk", "Jump", "Turn" });
var next = new string[2];
commands.PopRange(next);
bool hasMore = commands.TryPeek(out var remaining);
```

The buffer contains `"Turn"`, then `"Jump"`. The remaining item is `"Walk"`. A batch addition reports items in push order, while enumeration visits the top first.

## Operations

| Method | Behavior |
| --- | --- |
| `Push / PushRange` | Insert one item or an array / read-only list. A nonempty range produces one addition event. |
| `Pop / TryPop` | Remove the next item. The Try variant returns false when empty. |
| `PopRange` | Fill the caller’s array in removal order. An oversized buffer raises `InvalidOperationException` before any removal. |
| `Peek / TryPeek` | Read the next item while leaving it in place. |
| `ToArray / TrimExcess` | Copy items in removal order or trim unused storage. |
| `Clear` | Empty the collection and emit `Reset`. |

[Implementation](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableStack.cs)
