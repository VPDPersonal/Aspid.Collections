---
title: Your first observable list
---

Build a tiny inventory that prints the name of each item as it arrives. You need only the runtime namespace and an installed copy of the package.

## Listen for new items

Call `Run` from your application. Pass `Console.WriteLine` in .NET or `UnityEngine.Debug.Log` in Unity. The same inventory code works in both.

```csharp
using System;
using System.Collections.Specialized;
using Aspid.Collections.Observable;

public static class InventoryExample
{
    public static void Run(Action<string> log)
    {
        using var inventory = new ObservableList<string>();
        inventory.CollectionChanged += change =>
        {
            if (change.Action == NotifyCollectionChangedAction.Add && change.IsSingleItem)
                log(change.NewItem!);
        };

        inventory.Add("Potion");
        inventory.Add("Key");
    }
}
```

You should see `Potion` and then `Key`. Each call to `Add` changes the list and invokes the handler before returning. The action check lets the handler ignore the final `Reset` when the list is disposed.

## Let another object read the inventory

```csharp
using Aspid.Collections.Observable;

public sealed class Inventory : System.IDisposable
{
    private readonly ObservableList<string> _items = new();

    public IReadOnlyObservableList<string> Items => _items;
    public void Collect(string item) => _items.Add(item);
    public void Dispose() => _items.Dispose();
}
```

The UI can subscribe to `Items.CollectionChanged`, read `Count` and access items by index. The inventory owns the mutable list. To add a whole loot drop in one operation, use `AddRange` and handle [batch events](./events.md).
