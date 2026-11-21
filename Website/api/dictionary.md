---
title: ObservableDictionary
---

Use `ObservableDictionary<TKey, TValue>` to identify each entry by key. An event item is a `KeyValuePair<TKey, TValue>`.

## Update a leaderboard

```csharp
using System;
using Aspid.Collections.Observable;

using var scores = new ObservableDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
scores["Ada"] = 10;
scores["Ada"] = 18;
bool found = scores.TryGetValue("ada", out int score);
```

The two writes raise `Add` and `Replace`. The lookup succeeds and returns 18. For keyed rows, read the key from the event item. Change indices are −1.

## Operations

| Method | Behavior |
| --- | --- |
| `Add(key, value)` | Insert a new key. A duplicate raises `ArgumentException`. |
| `this[key]` | Reading a missing key throws. Assignment inserts or replaces. |
| `ContainsKey / TryGetValue` | Check membership or retrieve a value in one lookup. |
| `Remove(key)` | Return false for a missing key. A successful removal emits the old pair. |
| `Remove(pair)` | Require both the key and value to match. |
| `Keys / Values / Comparer` | Access the key and value sequences and the key equality comparer. |

[Implementation](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableDictionary.cs)
