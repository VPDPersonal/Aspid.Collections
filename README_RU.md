# Aspid.Collections

![Aspid.Collections](Documentation/Images/aspid_collections_readme_banner.gif)

[![Unity 2021.3+](https://img.shields.io/badge/2021.3%2B-000000?style=flat&logo=unity&logoColor=white&color=4fa35d)](https://unity.com/)
[![Releases](https://img.shields.io/github/v/release/VPDPersonal/Aspid.Collections?label=Release&labelColor=254d2c&color=4fa35d)](https://github.com/VPDPersonal/Aspid.Collections/releases)
[![License](https://img.shields.io/github/license/VPDPersonal/Aspid.Collections?label=License&labelColor=254d2c&color=4fa35d)](LICENSE)

Библиотека наблюдаемых коллекций с поддержкой ковариантности, синхронизации между коллекциями, фильтрации и сортировки.

## Содержание

- [Integration](#integration)
- [Основные возможности](#основные-возможности)
- [Коллекции](#коллекции)
  - [ObservableList](#observablelist)
  - [ObservableDictionary](#observabledictionary)
  - [ObservableHashSet](#observablehashset)
  - [ObservableQueue](#observablequeue)
  - [ObservableStack](#observablestack)
- [Интерфейсы](#интерфейсы)
- [События](#события)
- [Синхронизация коллекций](#синхронизация-коллекций)
- [Фильтрация и сортировка](#фильтрация-и-сортировка)
- [Примеры использования](#примеры-использования)

## Integration

Установите Aspid.Collections через UPM (Unity Package Manager) — добавьте пакет по его Git URL. Релизный workflow публикует ветку `upm`, в корне которой лежит само содержимое пакета, поэтому параметр `?path=` указывать не нужно:

```
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

Чтобы установить конкретную версию, укажите неизменяемый per-release тег `upm/<version>` (список доступных версий — на странице [Releases](https://github.com/VPDPersonal/Aspid.Collections/releases)):

```
https://github.com/VPDPersonal/Aspid.Collections.git#upm/1.1.0
```

Теги версий начинаются с `upm/1.0.2`. Пре-релизы публикуются в отдельную ветку `upm-preview`:

```
https://github.com/VPDPersonal/Aspid.Collections.git#upm-preview
```

## Основные возможности

- 🔔 **Наблюдаемые коллекции** — автоматические уведомления об изменениях
- 🔄 **Синхронизация** — автоматическая синхронизация между коллекциями с преобразованием типов
- 🔍 **Фильтрация** — динамическая фильтрация с автоматическим обновлением
- 📊 **Сортировка** — динамическая сортировка без изменения исходной коллекции
- ✨ **Ковариантность** — поддержка ковариантных интерфейсов

## Коллекции

### ObservableList
```csharp
using Aspid.Collections.Observable;

// Создание
var list = new ObservableList<string>();
var listWithCapacity = new ObservableList<string>(10);
var listFromCollection = new ObservableList<string>(new[] { "a", "b", "c" });

// Подписка на изменения
list.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Базовые операции
list.Add("item");
list.Insert(0, "first");
list[0] = "updated";
bool removed = list.Remove("item");
list.RemoveAt(0);
list.Clear();

// Очистка списка и события
list.Dispose();

// Пакетные операции
list.AddRange(new[] { "a", "b", "c" });
list.InsertRange(0, new[] { "x", "y" });

// Перемещение
list.Move(0, 2); // Переместить элемент с индекса 0 на индекс 2

// Запросы
int index = list.IndexOf("item");
bool contains = list.Contains("item");
list.ForEach(item => Console.WriteLine(item));

// Копирование в массив
var buffer = new string[list.Count];
list.CopyTo(buffer, 0);
```

Расширения (`Aspid.Collections.Observable.Extensions`):

```csharp
using Aspid.Collections.Observable.Extensions;

list.Swap(0, 3); // Поменять местами два элемента по индексу
```

### ObservableDictionary
```csharp
using Aspid.Collections.Observable;

// Создание
var dict = new ObservableDictionary<string, int>();
var dictWithCapacity = new ObservableDictionary<string, int>(capacity: 16);
var dictWithComparer = new ObservableDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
var dictWithBoth = new ObservableDictionary<string, int>(capacity: 16, StringComparer.OrdinalIgnoreCase);
var dictFromCollection = new ObservableDictionary<string, int>(
    new[] { KeyValuePair.Create("a", 1), KeyValuePair.Create("b", 2) }
);

// Подписка на изменения
dict.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Операции
dict.Add("key", 42);
dict["key"] = 100;        // Replace, если ключ существует
dict["newKey"] = 200;     // Add, если ключ не существует
bool removed = dict.Remove("key");
dict.Clear();

// Очистка словаря и события
dict.Dispose();

// Доступ к данным
bool exists = dict.TryGetValue("key", out var value);
bool contains = dict.ContainsKey("key");
bool containsPair = dict.Contains(KeyValuePair.Create("key", 100));

// Перечисление / интроспекция
IEnumerable<string> keys = dict.Keys;
IEnumerable<int> values = dict.Values;
IEqualityComparer<string> comparer = dict.Comparer;
```

### ObservableHashSet
```csharp
using Aspid.Collections.Observable;

// Создание
var set = new ObservableHashSet<string>();
var setWithComparer = new ObservableHashSet<string>(StringComparer.OrdinalIgnoreCase);
var setFromCollection = new ObservableHashSet<string>(new[] { "a", "b", "c" });

// Подписка на изменения
set.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Операции
bool added = set.Add("item");
bool removed = set.Remove("item");
set.Clear();

// Очистка множества и события
set.Dispose();

// Запросы
bool contains = set.Contains("item");
IEqualityComparer<string> comparer = set.Comparer;

// Операции над множествами (только чтение, источник не меняется)
var other = new[] { "a", "b", "c" };
bool isSub       = set.IsSubsetOf(other);
bool isSuper     = set.IsSupersetOf(other);
bool isProperSub = set.IsProperSubsetOf(other);
bool isProperSup = set.IsProperSupersetOf(other);
bool overlaps    = set.Overlaps(other);
bool equals      = set.SetEquals(other);
```

> `ObservableHashSet<T>` намеренно **не** реализует `ISet<T>`:
> изменяющие операции (`UnionWith`, `IntersectWith`, …) не могут
> отправить точные уведомления о добавленных и удалённых элементах,
> поэтому доступны только предикаты множеств выше.

### ObservableQueue
```csharp
using Aspid.Collections.Observable;

// Создание
var queue = new ObservableQueue<string>();
var queueWithCapacity = new ObservableQueue<string>(10);
var queueFromCollection = new ObservableQueue<string>(new[] { "a", "b", "c" });
var queueWrappingExisting = new ObservableQueue<string>(new Queue<string>()); // оборачивает, не копирует

// Подписка на изменения
queue.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Операции
queue.Enqueue("item");
queue.EnqueueRange(new[] { "a", "b", "c" });

string peek = queue.Peek();                       // "item"; на пустой очереди бросает исключение
bool hasPeek = queue.TryPeek(out var peekResult);

string item = queue.Dequeue();
bool success = queue.TryDequeue(out var result);

// Пакетное извлечение: длина буфера не больше Count
var buffer = new string[2];
queue.DequeueRange(buffer);

// Снимок / ёмкость
string[] snapshot = queue.ToArray();
queue.TrimExcess();

queue.Clear();

// Очистка очереди и события
queue.Dispose();
```

### ObservableStack
```csharp
using Aspid.Collections.Observable;

// Создание
var stack = new ObservableStack<string>();
var stackWithCapacity = new ObservableStack<string>(10);
var stackFromCollection = new ObservableStack<string>(new[] { "a", "b", "c" });

// Подписка на изменения
stack.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Операции
stack.Push("item");
stack.PushRange(new[] { "a", "b", "c" });

string peek = stack.Peek();
bool hasPeek = stack.TryPeek(out var peekResult);

string item = stack.Pop();
bool success = stack.TryPop(out var result);

// Пакетное извлечение: длина буфера не больше Count
var buffer = new string[2];
stack.PopRange(buffer);

// Снимок / ёмкость
string[] snapshot = stack.ToArray();
stack.TrimExcess();

stack.Clear();

// Очистка стэка и события
stack.Dispose();
```

## Интерфейсы

Интерфейсы списков, коллекций, синков, фильтров и событий объявляют тип
элемента **ковариантным** (`out T`), поэтому, например,
`IReadOnlyObservableList<Cat>` можно присвоить
`IReadOnlyObservableList<Animal>`. Словарные интерфейсы
(`IReadOnlyObservableDictionary<TKey, TValue>`,
`IReadOnlyObservableDictionarySync<TKey, T>`) инвариантны, как и
`IReadOnlyDictionary<TKey, TValue>`.

### Основные интерфейсы

| Интерфейс | Описание |
|-----------|----------|
| `IObservableCollection<out T>` | Базовый интерфейс для всех наблюдаемых коллекций |
| `IReadOnlyObservableList<out T>` | Только для чтения список с уведомлениями |
| `IReadOnlyObservableDictionary<TKey, TValue>` | Только для чтения словарь с уведомлениями |

### Интерфейсы синхронизации (`Aspid.Collections.Observable.Synchronizer`)

Возвращаются расширениями `CreateSync(...)`. Каждая обёртка-синк
реализует `IDisposable` и владеет своей подпиской на исходную коллекцию.

| Интерфейс | Описание |
|-----------|----------|
| `IReadOnlyObservableCollectionSync<out T>` | База для синхронизированных коллекций кроме списка (Queue/Stack/HashSet) |
| `IReadOnlyObservableListSync<out T>` | Синхронизированная проекция списка (расширяет `IReadOnlyObservableList<T>`) |
| `IReadOnlyObservableDictionarySync<TKey, T>` | Синхронизированная проекция словаря |

### Интерфейсы фильтрации (`Aspid.Collections.Observable.Filtered`)

| Интерфейс | Описание |
|-----------|----------|
| `IReadOnlyFilteredList<out T>` | Только для чтения отфильтрованное/отсортированное представление `IReadOnlyList<T>` (событие `CollectionChanged` без параметров) |

### Интерфейсы событий

| Интерфейс | Описание |
|-----------|----------|
| `IObservableEvents<out T>` | Освобождаемая обёртка, которую возвращает `SplitByEvents(...)`, с отдельными событиями `Added` / `Removed` / `Moved` / `Replaced` / `Reset` |

### Иерархия интерфейсов
```
IObservableCollection<out T>
├── IReadOnlyCollection<T>
├── CollectionChanged event
└── SyncRoot property

IReadOnlyObservableList<out T>
├── IObservableCollection<T>
└── IReadOnlyList<T>

IReadOnlyObservableDictionary<TKey, TValue>
├── IObservableCollection<KeyValuePair<TKey, TValue>>
└── IReadOnlyDictionary<TKey, TValue>

IReadOnlyObservableCollectionSync<out T>
├── IObservableCollection<T>
└── IDisposable

IReadOnlyObservableListSync<out T>
├── IReadOnlyObservableList<T>
└── IReadOnlyObservableCollectionSync<T>

IReadOnlyFilteredList<out T>
├── IReadOnlyList<T>
└── CollectionChanged event (Action)

IObservableEvents<out T>
├── IDisposable
└── Added / Removed / Moved / Replaced / Reset events
```

## События
### NotifyCollectionChangedEventArgs

Структура аргументов события изменения коллекции:

```csharp
public readonly struct NotifyCollectionChangedEventArgs<T>
{
    // Add, Remove, Replace, Move, Reset
    public NotifyCollectionChangedAction Action { get; }
    
    // true для одиночных операций
    public bool IsSingleItem { get; }                    
    
    // Для одиночных операций
    public T? NewItem { get; }
    public T? OldItem { get; }
    
    // Для пакетных операций
    public IReadOnlyList<T>? NewItems { get; }
    public IReadOnlyList<T>? OldItems { get; }
    
    // Индексы
    public int NewStartingIndex { get; }
    public int OldStartingIndex { get; }
}
```

### Типы действий
| Действие | Описание |
|----------|----------|
| `Add` | Добавлены новые элементы |
| `Remove` | Удалены элементы |
| `Replace` | Элемент заменён другим |
| `Move` | Элемент перемещён на новую позицию |
| `Reset` | Коллекция очищена |

### Разделение событий (SplitByEvents)
Для удобной обработки разных типов изменений используйте расширение `SplitByEvents`:

```csharp
using Aspid.Collections.Observable;

var list = new ObservableList<string>();

// Подписка на отдельные события
var events = list.SplitByEvents(
    added: (items, index) => Console.WriteLine($"Added {items.Count} items at {index}"),
    removed: (items, index) => Console.WriteLine($"Removed {items.Count} items from {index}"),
    moved: (items, oldIndex, newIndex) => Console.WriteLine($"Moved from {oldIndex} to {newIndex}"),
    replaced: (oldItems, newItems, index) => Console.WriteLine($"Replaced at {index}"),
    reset: () => Console.WriteLine("Collection cleared")
);

// Не забудьте освободить ресурсы
events.Dispose();
```

## Синхронизация коллекций
Автоматическая синхронизация позволяет создать "зеркало" коллекции с преобразованием типов.

### Создание синхронизированной коллекции
```csharp
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

// Исходная коллекция моделей
var models = new ObservableList<UserModel>();

// Создание синхронизированной коллекции View-моделей
var viewModels = models.CreateSync(
    model => new UserViewModel(model),  // Конвертер
    isDisposable: true                  // Автоматически вызывать Dispose при удалении
);

// Или с кастомным обработчиком удаления
var viewModels2 = models.CreateSync(
    model => new UserViewModel(model),
    removed: vm => vm.Cleanup()
);
// Примечание: для ObservableQueue / ObservableStack / ObservableHashSet /
// IReadOnlyObservableDictionary параметр колбэка удаления называется
// `remove:` (а не `removed:`). Позиционный аргумент снимает это различие.

// Все изменения в models автоматически отражаются в viewModels
models.Add(new UserModel { Name = "John" });
// viewModels теперь содержит UserViewModel для John

// Не забудьте освободить ресурсы
viewModels.Dispose();
```

### Поддерживаемые коллекции для синхронизации
| Исходная коллекция | Метод расширения | Результат |
|--------------------|------------------|-----------|
| `IReadOnlyObservableList<T>` | `CreateSync()` | `IReadOnlyObservableListSync<T>` |
| `ObservableQueue<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `ObservableStack<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `ObservableHashSet<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `IReadOnlyObservableDictionary<K,V>` | `CreateSync()` | `IReadOnlyObservableDictionarySync<K,T>` |

## Фильтрация и сортировка
`FilteredList<T>` предоставляет динамическую фильтрацию и сортировку без изменения исходной коллекции.

### Создание отфильтрованного списка

```csharp
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

var list = new ObservableList<int> { 5, 2, 8, 1, 9, 3 };

// Только фильтрация
var filtered = list.CreateFiltered(x => x > 3);
// filtered содержит: 5, 8, 9

// Только сортировка
var sorted = list.CreateFiltered(Comparer<int>.Default);
// sorted содержит: 1, 2, 3, 5, 8, 9

// Фильтрация и сортировка
var filteredAndSorted = list.CreateFiltered(
    filter: x => x > 2,
    comparer: Comparer<int>.Default
);
// filteredAndSorted содержит: 3, 5, 8, 9
```

### Динамическое изменение фильтров

```csharp
var filtered = list.CreateFiltered();

// Подписка на изменения
filtered.CollectionChanged += () =>
{
    Console.WriteLine("Filtered collection updated");
};

// Динамическое изменение фильтра
filtered.Filter = x => x > 5;

// Динамическое изменение сортировки
filtered.Comparer = Comparer<int>.Create((a, b) => b.CompareTo(a)); // Обратный порядок

// Принудительное обновление
filtered.Update();
```

### Цепочка фильтров

`FilteredList` можно использовать как источник для другого `FilteredList`:

```csharp
var list = new ObservableList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNumbers = list.CreateFiltered(x => x % 2 == 0);
// evenNumbers: 2, 4, 6, 8, 10

var largeEvenNumbers = evenNumbers.CreateFiltered(x => x > 5);
// largeEvenNumbers: 6, 8, 10
```

## Примеры использования
### MVVM паттерн с синхронизацией

```csharp
public class Todo
{
    // Some code.
}

public class TodoService
{
    private readonly ObservableList<Todo> _todos = new();

    public IReadOnlyObservableList<Todo> Todos => _todos;

    public void Add(Todo todo) =>
        _todos.Add(todo);

    public void Remove(Todo todo) =>
        _todos.Remove(todo);
}

public class TodoListViewModel : IDisposable
{
    private readonly TodoService _service;
    private readonly IReadOnlyObservableListSync<TodoItemViewModel> _items;
    
    public IReadOnlyObservableList<TodoItemViewModel> Items => _items;
    
    public TodoListViewModel(TodoService service)
    {
        _service = service;
        
        // Automatic Model -> ViewModel synchronization
        _items = _service.Todos.CreateSync(
            model => new TodoItemViewModel(model),
            isDisposable: true
        );
    }
    
    public void Dispose() => _items.Dispose();
}

// isDisposable: true вызывает Dispose у элементов, реализующих IDisposable
public class TodoItemViewModel : IDisposable
{
    private Todo _model;

    public TodoItemViewModel(Todo model)
    {
        _model = model;
    }

    // Some code.

    public void Dispose()
    {
        // Отписка от модели, освобождение ресурсов.
    }
}
```

### Наблюдаемый список с фильтрацией
```csharp
public class SearchableListView : IDisposable
{
    private readonly ObservableList<ItemModel> _allItems;
    private readonly FilteredList<ItemModel> _visibleItems;
    
    public IReadOnlyFilteredList<ItemModel> VisibleItems => _visibleItems;
    
    public SearchableListView()
    {
        _allItems = new ObservableList<ItemModel>();
        _visibleItems = _allItems.CreateFiltered();
        
        _visibleItems.CollectionChanged += RefreshView;
    }
    
    public void SetSearchQuery(string query)
    {
        _visibleItems.Filter = string.IsNullOrEmpty(query) 
            ? null 
            : item => item.Name.Contains(query, StringComparison.OrdinalIgnoreCase);
    }
    
    public void SetSortOrder(bool ascending)
    {
        _visibleItems.Comparer = ascending
            ? Comparer<ItemModel>.Create((a, b) => string.Compare(a.Name, b.Name))
            : Comparer<ItemModel>.Create((a, b) => string.Compare(b.Name, a.Name));
    }
    
    private void RefreshView() { /* Обновление UI */ }
    
    public void Dispose() => _visibleItems.Dispose();
}
```

### Реактивная обработка событий

```csharp
public class InventoryManager : IDisposable
{
    private readonly ObservableList<Item> _inventory = new();
    private readonly IObservableEvents<Item> _events;
    
    public InventoryManager()
    {
        _events = _inventory.SplitByEvents(
            added: (items, _) =>
            {
                foreach (var item in items)
                    Debug.Log($"Item added: {item.Name}");
            },
            removed: (items, _) =>
            {
                foreach (var item in items)
                    Debug.Log($"Item removed: {item.Name}");
            }
        );
    }
    
    public void AddItem(Item item) => _inventory.Add(item);
    public void RemoveItem(Item item) => _inventory.Remove(item);
    
    public void Dispose() => _events.Dispose();
}
```
## License
MIT License - смотрите файл [LICENSE](LICENSE).
