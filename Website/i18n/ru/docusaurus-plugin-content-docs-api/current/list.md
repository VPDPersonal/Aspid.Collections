---
title: ObservableList
---

Используйте `ObservableList<T>` для упорядоченных элементов с доступом по индексу. События добавления, удаления, замены и перемещения содержат индексы, поэтому интерфейс может обновлять отдельные строки.

## Изменение порядка в инвентаре

```csharp
using Aspid.Collections.Observable;

using var slots = new ObservableList<string>();
slots.AddRange("Potion", "Key", "Map");
slots.Move(2, 0);
slots[1] = "Elixir";
slots.RemoveRange(1, 2);
```

В списке останется только `"Map"`. Подписчики последовательно получат групповое `Add`, одиночные `Move` и `Replace`, затем групповое `Remove`.

## Операции

| Метод | Поведение |
| --- | --- |
| `Add / Insert` | Добавляют один элемент в конец или по индексу. |
| `AddRange / InsertRange` | Принимают массив, отдельные аргументы или `IReadOnlyList<T>`. Непустой диапазон вызывает одно событие. |
| `Move(oldIndex, newIndex)` | Перемещает элемент в конечную позицию. Совпадающие допустимые индексы сохраняют порядок. |
| `Remove / RemoveAt / RemoveRange` | Удаляют первое совпадение, элемент по индексу или непрерывный диапазон. |
| `IndexOf / Contains` | Ищут индекс или проверяют наличие элемента. Отсутствующему элементу соответствует индекс −1. |
| `Clear` | Очищает список и отправляет `Reset`. |

[Исходный код](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableList.cs)
