---
title: ObservableQueue
---

Используйте `ObservableQueue<T>` для обработки в порядке поступления, например в очереди игровых команд.

## Извлечение группы элементов

```csharp
using Aspid.Collections.Observable;

using var commands = new ObservableQueue<string>();
commands.EnqueueRange(new[] { "Walk", "Jump", "Turn" });
var next = new string[2];
commands.DequeueRange(next);
bool hasMore = commands.TryPeek(out var remaining);
```

В буфере окажутся `"Walk"` и `"Jump"`, останется `"Turn"`. Перебор начинается с самого раннего элемента очереди.

## Операции

| Метод | Поведение |
| --- | --- |
| `Enqueue / EnqueueRange` | Добавляют элемент, массив или список только для чтения. Непустая группа вызывает одно событие добавления. |
| `Dequeue / TryDequeue` | Извлекают следующий элемент. Вариант Try возвращает false для пустой коллекции. |
| `DequeueRange` | Заполняет массив в порядке извлечения. Если буфер больше коллекции, вызывает `InvalidOperationException` до первого удаления. |
| `Peek / TryPeek` | Возвращают следующий элемент и сохраняют его в коллекции. |
| `ToArray / TrimExcess` | Копируют элементы в порядке извлечения или освобождают избыточную ёмкость. |
| `Clear` | Очищает коллекцию и отправляет `Reset`. |

[Исходный код](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableQueue.cs)
