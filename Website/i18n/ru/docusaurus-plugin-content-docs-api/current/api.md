---
slug: /
title: Справочник API
---

Код библиотеки находится в сборке `Aspid.Collections.Observable`. Выберите коллекцию по структуре данных, затем определите, какие операции понадобятся её потребителям.

## Типы коллекций

| Тип | Когда использовать |
| --- | --- |
| [ObservableList](./list.md) | Строки с индексом, перестановки и работа с диапазонами |
| [ObservableDictionary](./dictionary.md) | Доступ к записям по ключу |
| [ObservableHashSet](./set.md) | Уникальные элементы и правила сравнения |
| [ObservableQueue](./queue.md) | Обработка задач в порядке поступления |
| [ObservableStack](./stack.md) | История отмены и доступ к последнему элементу |

## Интерфейсы

Через `IObservableCollection<T>` доступны перебор, `Count`, `SyncRoot` и событие `CollectionChanged`. Для чтения по индексу используйте `IReadOnlyObservableList<T>`, для поиска по ключу — `IReadOnlyObservableDictionary<TKey, TValue>`. Интерфейсы коллекции и списка ковариантны. В код, который ожидает коллекцию объектов базового класса, можно передать коллекцию объектов производного класса.

```csharp
using Aspid.Collections.Observable;

using var names = new ObservableList<string> { "Ada" };
IReadOnlyObservableList<string> rows = names;
IObservableCollection<object> entries = rows;
```

Через `rows` потребитель может прочитать имя и подписаться на изменения. Владелец `names` может редактировать список и отвечает за вызов `Dispose()`.

## Методы расширения и результаты

| Пространство имён | Операция | Результат |
| --- | --- | --- |
| `Aspid.Collections.Observable` | `SplitByEvents` | Подписка `IObservableEvents<T>` с отдельным обработчиком для каждого действия. Для отписки вызовите `Dispose()`. |
| `Aspid.Collections.Observable.Extensions` | `Swap(index1, index2)` | Меняет два элемента списка местами с помощью операций перемещения. |
| `Aspid.Collections.Observable.Filtered` | `CreateFiltered` | Создаёт `FilteredList<T>` поверх `IReadOnlyList<T>`. Для чтения результата подойдёт `IReadOnlyFilteredList<T>`. Его событие изменения вызывается без аргументов. |
| `Aspid.Collections.Observable.Synchronizer` | `CreateSync` | Создаёт наблюдаемую проекцию с методом `Dispose()`. У списка сохраняется доступ по индексу, у словаря — по ключу. Для остальных трёх коллекций результат имеет тип `IReadOnlyObservableCollectionSync<T>`. |

[Исходный код библиотеки](https://github.com/VPDPersonal/Aspid.Collections/tree/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable)
