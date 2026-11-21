---
title: Ownership and cleanup
---

A source holds a reference to each subscribed wrapper or handler. Give each subscription a clear owner, such as the screen that created it. Release it when that owner stops using it.

## Dispose in reverse order

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var roster = new ObservableList<string> { "Ada", "Alan", "Mia" };
using var matches = roster.CreateFiltered(name => name.StartsWith("A"));
using var shortNames = matches.CreateFiltered(name => name.Length <= 3);
Console.WriteLine(string.Join(", ", shortNames)); // Ada
```

At the end of the scope C# disposes `shortNames`, then `matches`, then `roster`. This works because each wrapper stops listening before its source is released. When the roster belongs to another system, only dispose the two views.

## Pair setup and teardown in Unity

For a subscription that lives only while a component is enabled, save the handle in `OnEnable` and release it in `OnDisable`. A direct `CollectionChanged += Handler` subscription can be removed with `-= Handler` in the same place.

```csharp
using System;
using Aspid.Collections.Observable;
using UnityEngine;

public sealed class InventoryLog : MonoBehaviour
{
    private readonly ObservableList<string> _items = new();
    private IDisposable _subscription;

    private void OnEnable()
    {
        _subscription = _items.SplitByEvents(
            added: (items, _) => Debug.Log(string.Join(", ", items)));
    }

    public void Collect(string item) => _items.Add(item);
    private void OnDisable() => _subscription?.Dispose();
    private void OnDestroy() => _items.Dispose();
}
```

Disposing an event handle detaches its callbacks. Disposing a filtered view detaches it from its source. Disposing a sync view also cleans up projected items according to the ownership option you supplied. Disposing an observable source clears it and removes its subscribers.
