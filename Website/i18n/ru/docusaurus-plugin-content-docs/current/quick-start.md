---
title: Первый наблюдаемый список
---

Соберём инвентарь, который выводит название каждого добавленного предмета. Для примера достаточно установить пакет и подключить пространство имён его коллекций.

## Подпишитесь на добавление предметов

Вызовите `Run` из приложения. В .NET передайте `Console.WriteLine`, а в Unity — `UnityEngine.Debug.Log`. Код инвентаря в обоих случаях одинаков.

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

В журнале появятся сначала `Potion`, затем `Key`. Каждый вызов `Add` изменяет список и сразу вызывает обработчик. Проверка типа события пропускает завершающий `Reset`, который список отправляет при освобождении.

## Передайте инвентарь другому объекту для чтения

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

Интерфейс игры может подписаться на `Items.CollectionChanged`, читать `Count` и получать предмет по индексу. Изменяемым списком владеет инвентарь. Чтобы добавить сразу всю добычу, используйте `AddRange` и обработайте [пакетное событие](./events.md).
