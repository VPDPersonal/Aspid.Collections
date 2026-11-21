---
title: ObservableHashSet
---

Use `ObservableHashSet<T>` for unique members such as unlocked ability IDs. Its comparer defines when two values represent the same member.

## Track unlocked abilities

```csharp
using System;
using Aspid.Collections.Observable;

using var abilities = new ObservableHashSet<string>(StringComparer.OrdinalIgnoreCase);
bool unlocked = abilities.Add("Dash");
bool duplicate = abilities.Add("dash");
bool removed = abilities.Remove("DASH");
```

`unlocked` and `removed` are true. The second add returns false. Subscribers receive one addition and one removal, both containing the stored spelling `"Dash"`.

## Operations

| Method | Behavior |
| --- | --- |
| `Add / Remove` | Return true and raise an event when membership changes. Event indices are −1. |
| `Contains / Overlaps` | Test for one member or any shared member. |
| `IsSubsetOf / IsSupersetOf` | Compare membership. The proper variants additionally require the sets to differ. |
| `SetEquals` | Compare members using the set comparer, independently of order. |
| `Clear` | Remove all members and emit `Reset`. |

[Implementation](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableHashSet.cs)
