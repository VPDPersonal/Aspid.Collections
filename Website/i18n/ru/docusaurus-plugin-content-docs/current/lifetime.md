---
title: Время жизни и освобождение
---

Источник хранит ссылки на подписанные представления и обработчики. Определите владельца каждой подписки, например экран, который её создал. Освобождайте подписку, когда владелец заканчивает с ней работать.

## Освобождайте в обратном порядке

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var roster = new ObservableList<string> { "Ada", "Alan", "Mia" };
using var matches = roster.CreateFiltered(name => name.StartsWith("A"));
using var shortNames = matches.CreateFiltered(name => name.Length <= 3);
Console.WriteLine(string.Join(", ", shortNames)); // Ada
```

При выходе из области видимости C# вызовет `Dispose()` сначала у `shortNames`, затем у `matches` и в конце у `roster`. Так каждое представление успеет отписаться до освобождения своего источника. Если отряд принадлежит другой системе, освободите только два созданных вами представления.

## Свяжите подписку с жизненным циклом Unity

Если подписка нужна только активному компоненту, сохраните её в `OnEnable` и освободите в `OnDisable`. Прямую подписку `CollectionChanged += Handler` в том же месте можно снять через `-= Handler`.

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

Освобождение объекта подписки отключает его обработчики. Освобождение фильтрованного представления отписывает его от источника. Синхронизированное представление также освобождает созданные элементы согласно выбранному при создании правилу. Освобождение исходной наблюдаемой коллекции очищает её и снимает её подписки.
