# Уведомления об изменениях

## Содержимое события

Обработчик получает `INotifyCollectionChangedEventArgs<T>` без sender. Конкретные аргументы — собственная readonly-структура пакета `NotifyCollectionChangedEventArgs<T>`; из `System.Collections.Specialized` используется только enum действия.

```csharp
using System;
using System.Collections.Specialized;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
items.CollectionChanged += change =>
{
    if (change.Action == NotifyCollectionChangedAction.Reset)
    {
        Console.WriteLine("Reset");
        return;
    }

    var count = change.IsSingleItem
        ? 1
        : (change.NewItems ?? change.OldItems)!.Count;
    Console.WriteLine($"{change.Action}: {count}");
};
items.AddRange("Potion", "Elixir"); // Add: 2
```

Проверяйте `Action == Reset` до чтения элементов. У `Reset` нет данных об элементах, хотя `IsSingleItem` равен true. Затем по `IsSingleItem` выбирайте `NewItem` / `OldItem` либо `NewItems` / `OldItems`.

## Действия и индексы

| Действие | Элементы | Значение индекса |
| --- | --- | --- |
| Add | Новые элементы | Индекс вставки; старый индекс равен -1 |
| Remove | Удалённые элементы | Прежний индекс; новый равен -1 |
| Replace | Старые и новые элементы | Одинаковый индекс в обоих полях |
| Move | Один элемент в старом и новом полях | Позиция до и после перемещения |
| Reset | Нет элементов | Оба индекса равны -1 |

Dictionary и HashSet всегда передают индексы -1; ключи и элементы определяются содержимым события. При добавлении в очередь индекс начинается со старого Count, при удалении равен нулю. Операции стека используют ноль. `PushRange` передаёт входной порядок, хотя вершиной становится последний входной элемент.

Пустые пакетные операции и перемещение списка в тот же индекс не отправляют уведомлений. `Clear()` отправляет Reset, в том числе для пустой коллекции. Пакетные данные не гарантируют неизменяемого снимка: скопируйте элементы, прежде чем отложить обработку.

## Отдельные обработчики

`SplitByEvents` возвращает освобождаемый `IObservableEvents<T>`. Каждый обработчик элементов получает список, даже при одиночной операции. Обработчик замены получает сначала старые, затем новые элементы. Создание подписывает на будущие изменения и не воспроизводит текущее содержимое. [Zone Occupancy](/tutorials/zone-occupancy) показывает отключение одной подписки при сохранении другого потребителя.

Уведомления синхронные; см. [ограничения исключений и вложенных изменений](lifecycle.md).
