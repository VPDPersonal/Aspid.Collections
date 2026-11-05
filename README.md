# Aspid.Collections

<picture>
  <source srcset="Documentation~/Images/aspid_collections_readme_banner_dark.webp" />
  <img src="Documentation~/Images/aspid_collections_readme_banner.png" alt="Aspid.Collections" />
</picture>

[![Unity Package](Documentation~/Images/status-badge-unity.svg)](#installation)
[![Release 1.1.0](Documentation~/Images/status-badge-release.svg)](https://github.com/VPDPersonal/Aspid.Collections/releases/tag/v1.1.0)
[![MIT License](Documentation~/Images/status-badge-license.svg)](LICENSE)

**English** · [Русский](README_RU.md)

Aspid.Collections provides observable collections for Unity and .NET. React to changes in an inventory or leaderboard, project models into view models, and keep filtered and sorted views up to date. The runtime is pure C# and has no Unity engine dependency.

## Installation

<table>
<tr>
<td width="56%"><img src="Documentation~/Images/install.webp" alt="Package Manager installs Aspid.Collections from its Git URL" width="100%"></td>
<td width="44%">
<ol>
<li>Open <b>Window → Package Manager</b></li>
<li>Choose <b>+ → Install package from git URL…</b></li>
<li>Paste the URL and press <b>Install</b></li>
</ol>
</td>
</tr>
</table>

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

The `upm` branch tracks the latest stable release. To pin a version, use its `upm/<version>` tag, for example `#upm/1.1.0`. See the [available releases](https://github.com/VPDPersonal/Aspid.Collections/releases).

Preview releases use the `upm-preview` branch.

<details>
<summary>Unity compatibility and troubleshooting</summary>

The package manifest declares Unity 2021.3 as the minimum. Version 1.1.0 also declares a dependency on `com.unity.test-framework@1.6.0`. If Package Manager cannot resolve that dependency in your Unity version, see [the dependency issue](https://github.com/VPDPersonal/Aspid.Collections/pull/20).

</details>

## Features

Each example is a standalone C# snippet with top-level statements.

### Change notifications

<table>
<tr>
<td width="60%">

```csharp
using System;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
items.CollectionChanged += change =>
    Console.WriteLine(change.Action);

items.Add("Potion");       // Add
items[0] = "Elixir";       // Replace
items.RemoveAt(0);         // Remove
```

</td>
<td width="40%">

Subscribe to `CollectionChanged` to react to edits. Read `IsSingleItem` before choosing between `NewItem` / `OldItem` and `NewItems` / `OldItems`. Disposing the list clears it and emits `Reset`.

[Basic Change Notifications →](Samples~/01_BasicChangeNotifications)

</td>
</tr>
</table>

### Synchronized views

<table>
<tr>
<td width="60%">

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

using var scores = new ObservableList<int>();
using var labels = scores.CreateSync(n => $"Score {n}");

scores.Add(10);
scores[0] = 20;
Console.WriteLine(labels[0]); // Score 20
```

</td>
<td width="40%">

`CreateSync` projects each source item into a read-only collection that follows source changes. All five collection types support this one-way synchronization. [Overloads](Runtime/Observable/Synchronizer/Extensions/CreateSyncExtensions.cs)

[Inventory Sync →](Samples~/02_InventorySync)

</td>
</tr>
</table>

### Filtering and sorting

<table>
<tr>
<td width="60%">

```csharp
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var scores = new ObservableList<int> { 25, 5, 15 };
using var visible = scores.CreateFiltered(
    n => n >= 10, Comparer<int>.Default);

scores.Add(12);
// 12, 15, 25
Console.WriteLine(string.Join(", ", visible));
```

</td>
<td width="40%">

`CreateFiltered` presents matching items in the requested order while leaving the source intact. Change `Filter` or `Comparer` at runtime, or chain views for multiple filters. For a source without change notifications, call `Update()` after editing it.

[Filtered Inventory →](Samples~/03_FilteredInventory)

</td>
</tr>
</table>

### Separate callbacks for each change

<table>
<tr>
<td width="60%">

```csharp
using System;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
using var events = items.SplitByEvents(
    added: (batch, _) =>
        Console.WriteLine($"Added {batch.Count}"));

items.AddRange("Potion", "Elixir"); // Added 2
```

</td>
<td width="40%">

`SplitByEvents` routes adds, removes, moves, replacements and resets to separate handlers. Each item callback receives a list, including for single-item changes.

[Analytics Tap →](Samples~/05_SplitByEvents)

</td>
</tr>
</table>

## Collections

| Type | Use it for |
| --- | --- |
| [`ObservableList<T>`](Runtime/Observable/ObservableList.cs) | Ordered items with indexes, moves and range operations |
| [`ObservableDictionary<TKey, TValue>`](Runtime/Observable/ObservableDictionary.cs) | Items addressed by key, as in the [keyed leaderboard sample](Samples~/04_DictionaryKeyedTable) |
| [`ObservableHashSet<T>`](Runtime/Observable/ObservableHashSet.cs) | Unique items and membership checks |
| [`ObservableQueue<T>`](Runtime/Observable/ObservableQueue.cs) | First-in, first-out processing with range operations |
| [`ObservableStack<T>`](Runtime/Observable/ObservableStack.cs) | Last-in, first-out processing with range operations |

Expose `IReadOnlyObservableList<T>` or `IObservableCollection<T>` when consumers only need to read and subscribe. These interfaces are covariant for reference types. The dictionary interfaces keep both type parameters invariant.

Dispose synchronized views, filtered views and `SplitByEvents` handles to detach their subscriptions. Dispose dependent views before their sources and use a filtered view and its source on the same thread.

## Resources

| [Unity samples](Samples~) | [Runtime API](Runtime/Observable) | [Changelog](CHANGELOG.md) |
| --- | --- | --- |
| In Package Manager, select **Aspid.Collections → Samples → Import**. Open the imported scene and enter Play mode. | Browse collection types, event arguments and extension methods. | See changes and fixes by release. |

## Help and support

Include your Unity version, package version and steps to reproduce when reporting a problem. [Open an issue](https://github.com/VPDPersonal/Aspid.Collections/issues)

Aspid.Collections is distributed under the [MIT License](LICENSE).
