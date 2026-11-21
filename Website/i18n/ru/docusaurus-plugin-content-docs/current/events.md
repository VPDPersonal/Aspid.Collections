---
title: События коллекций
---

Подпишитесь на `CollectionChanged`, чтобы узнать, что изменилось за одну операцию. Обработчик получает один аргумент типа `INotifyCollectionChangedEventArgs<T>`. Его свойство `Action` использует перечисление .NET `NotifyCollectionChangedAction`.

## Прочитайте данные события

Сначала проверьте `Action`. Для операций с элементами свойство `IsSingleItem` подсказывает, какие поля читать. В списке начальный индекс указывает на первый затронутый элемент.

| Операция | Что читать |
| --- | --- |
| `Add` | `NewItem` или `NewItems`, `NewStartingIndex` |
| `Remove` | `OldItem` или `OldItems`, `OldStartingIndex` |
| `Replace` | Прежнее и новое значения в изменённой позиции |
| `Move` | Перемещённый элемент, `OldStartingIndex` и `NewStartingIndex` |
| `Reset` | Заново прочитайте текущее содержимое коллекции |

```csharp
using System;
using System.Collections.Specialized;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
items.CollectionChanged += change =>
{
    if (change.Action != NotifyCollectionChangedAction.Add) return;
    if (change.IsSingleItem)
        Console.WriteLine(change.NewItem);
    else
        Console.WriteLine(string.Join(", ", change.NewItems!));
};

items.Add("Shield");
items.AddRange("Key", "Potion");
```

Каждая из двух последних строк вызывает обработчик один раз. При втором вызове оба названия приходят в `NewItems`. У словаря в событии передаются пары ключ–значение. Для поиска записи используйте её ключ.

## Назначьте обработчики отдельным операциям

```csharp
using var changes = items.SplitByEvents(
    added: (batch, index) => Console.WriteLine($"+{batch.Count} @ {index}"),
    moved: (batch, from, to) => Console.WriteLine($"{from} -> {to}"),
    reset: () => Console.WriteLine("Reset"));

items.Add("Map");
items.Move(0, 1);
```

Добавьте этот блок после предыдущего примера. `SplitByEvents` передаёт списком даже один элемент. Обработчик `replaced` получает старые элементы, новые элементы, затем индекс. Сохраните возвращённый объект подписки и вызовите у него `Dispose()`, когда обработчики больше не нужны.
