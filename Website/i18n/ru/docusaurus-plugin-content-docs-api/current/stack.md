---
title: ObservableStack
---

Используйте `ObservableStack<T>` для обработки от последнего элемента к первому, например в истории отмены.

## Извлечение группы элементов

```csharp
using Aspid.Collections.Observable;

using var commands = new ObservableStack<string>();
commands.PushRange(new[] { "Walk", "Jump", "Turn" });
var next = new string[2];
commands.PopRange(next);
bool hasMore = commands.TryPeek(out var remaining);
```

В буфере окажутся `"Turn"` и `"Jump"`, останется `"Walk"`. Событие группового добавления сохраняет порядок записи, а перебор стека начинается с вершины.

## Операции

| Метод | Поведение |
| --- | --- |
| `Push / PushRange` | Добавляют элемент, массив или список только для чтения. Непустая группа вызывает одно событие добавления. |
| `Pop / TryPop` | Извлекают следующий элемент. Вариант Try возвращает false для пустой коллекции. |
| `PopRange` | Заполняет массив в порядке извлечения. Если буфер больше коллекции, вызывает `InvalidOperationException` до первого удаления. |
| `Peek / TryPeek` | Возвращают следующий элемент и сохраняют его в коллекции. |
| `ToArray / TrimExcess` | Копируют элементы в порядке извлечения или освобождают избыточную ёмкость. |
| `Clear` | Очищает коллекцию и отправляет `Reset`. |

[Исходный код](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableStack.cs)
