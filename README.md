# Aspid.Collections

<picture>
  <source srcset="Documentation~/Images/aspid_collections_readme_banner_dark.webp" />
  <img src="Documentation~/Images/aspid_collections_readme_banner.png" alt="Aspid.Collections" />
</picture>

[![Unity 2021.3.0f1+](Documentation~/Images/status-badge-unity.svg)](#installation)
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

The URL installs the latest release. **Update** in the Package Manager installs the next one. To pin a version or switch channels, see [Installation options](#installation-options).

Requires Unity 2021.3.0f1 or newer.

<details>
<summary>Troubleshooting</summary>

**Compile errors right after the install.** Check your Unity version in **Help → About Unity** (**Unity → About Unity** on macOS).

**The Console shows `Assembly with name 'Aspid.Collections.Observable' already exists`.** The project has a second copy of the package, for example its folder under `Assets`. Delete the copy.

**Package Manager cannot resolve `com.unity.test-framework@1.6.0`.** The published `#upm` release (1.1.0) declares this dependency. [PR #20](https://github.com/VPDPersonal/Aspid.Collections/pull/20) removed it from `main`.

</details>

## Features

### Change notifications

<table>
<tr>
<td width="60%">

```csharp
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

<table width="100%">
<thead>
<tr><th>Type</th><th width="100%">Use it for</th></tr>
</thead>
<tbody>
<tr><td><a href="Runtime/Observable/ObservableList.cs"><code>ObservableList&lt;T&gt;</code></a></td><td>Ordered items with indexes, moves and range operations</td></tr>
<tr><td><a href="Runtime/Observable/ObservableDictionary.cs"><code>ObservableDictionary&lt;TKey, TValue&gt;</code></a></td><td>Items addressed by key, as in the <a href="Samples~/04_DictionaryKeyedTable">keyed leaderboard sample</a></td></tr>
<tr><td><a href="Runtime/Observable/ObservableHashSet.cs"><code>ObservableHashSet&lt;T&gt;</code></a></td><td>Unique items and membership checks</td></tr>
<tr><td><a href="Runtime/Observable/ObservableQueue.cs"><code>ObservableQueue&lt;T&gt;</code></a></td><td>First-in, first-out processing with range operations</td></tr>
<tr><td><a href="Runtime/Observable/ObservableStack.cs"><code>ObservableStack&lt;T&gt;</code></a></td><td>Last-in, first-out processing with range operations</td></tr>
</tbody>
</table>

Expose `IReadOnlyObservableList<T>` or `IObservableCollection<T>` when consumers only need to read and subscribe. These interfaces are covariant for reference types. The dictionary interfaces keep both type parameters invariant.

Dispose synchronized views, filtered views and `SplitByEvents` handles to detach their subscriptions. Dispose dependent views before their sources and use a filtered view and its source on the same thread.

## Resources

| [Unity samples](Samples~) | [Runtime API](Runtime/Observable) | [Changelog](CHANGELOG.md) |
| --- | --- | --- |
| In Package Manager, select **Aspid.Collections → Samples → Import**. Open the imported scene and enter Play mode. | Browse collection types, event arguments and extension methods. | See changes and fixes by release. |

### Installation options

To pin a version, use its `upm/<version>` tag, for example `#upm/1.1.0`. See the [available releases](https://github.com/VPDPersonal/Aspid.Collections/releases).

Preview releases use the `upm-preview` branch when available.

## Help and support

<p>Found a bug or have a question? Include your Unity version, package version and steps to reproduce.</p>
<p><a href="https://github.com/VPDPersonal/Aspid.Collections/issues"><b>Open an issue</b></a> · <a href="https://github.com/VPDPersonal/Aspid.Collections">Star on GitHub</a></p>
