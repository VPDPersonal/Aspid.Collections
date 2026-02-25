# Aspid.Collections

[![Unity 2021.3+](https://img.shields.io/badge/2021.3%2B-000000?style=flat&logo=unity&logoColor=white&color=4fa35d)](https://unity.com/)
[![Releases](https://img.shields.io/github/v/release/VPDPersonal/Aspid.Collections?label=Release&labelColor=254d2c&color=4fa35d)](https://github.com/VPDPersonal/Aspid.Collections/releases)
[![License](https://img.shields.io/github/license/VPDPersonal/Aspid.Collections?label=License&labelColor=254d2c&color=4fa35d)](LICENSE)

Библиотека наблюдаемых коллекций с поддержкой ковариантности, синхронизации между коллекциями, фильтрации и сортировки.

## Содержание

- [Установка](#установка)
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

## Установка
1. Добавьте следующие пакеты в ваш Unity проект через Package Manager:
   - Aspid.Internal.Unity: `https://github.com/VPDPersonal/Aspid.Internal.Unity.git`
   - Aspid.Collections: `https://github.com/VPDPersonal/Aspid.Collections.git`
2. Или скачайте .unitypackage: с [странице релиза на GitHub](https://github.com/VPDPersonal/Aspid.Collections/releases) и импортируйте его в проект.

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
```

### ObservableDictionary
```csharp
using Aspid.Collections.Observable;

// Создание
var dict = new ObservableDictionary<string, int>();
var dictWithComparer = new ObservableDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
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
```

### ObservableQueue
```csharp
using Aspid.Collections.Observable;

// Создание
var queue = new ObservableQueue<string>();
var queueWithCapacity = new ObservableQueue<string>(10);
var queueFromCollection = new ObservableQueue<string>(new[] { "a", "b", "c" });

// Подписка на изменения
queue.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Операции
string peek = queue.Peek();
bool hasPeek = queue.TryPeek(out var peekResult);

queue.Enqueue("item");
queue.EnqueueRange(new[] { "a", "b", "c" });

string item = queue.Dequeue();
bool success = queue.TryDequeue(out var result);

queue.Clear();

// Очистка очереди и события
queue.Dispose();

// Пакетное извлечение
var buffer = new string[3];
queue.DequeueRange(buffer);
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

stack.Clear();

// Очистка стэка и события
stack.Dispose();

// Пакетное извлечение
var buffer = new string[3];
stack.PopRange(buffer);
```

## Интерфейсы
### Основные интерфейсы

| Интерфейс | Описание |
|-----------|----------|
| `IObservableCollection<T>` | Базовый интерфейс для всех наблюдаемых коллекций |
| `IReadOnlyObservableList<T>` | Только для чтения список с уведомлениями |
| `IReadOnlyObservableDictionary<TKey, TValue>` | Только для чтения словарь с уведомлениями |

### Иерархия интерфейсов
```
IObservableCollection<T>
├── IReadOnlyCollection<T>
├── CollectionChanged event
└── SyncRoot property

IReadOnlyObservableList<T>
├── IObservableCollection<T>
└── IReadOnlyList<T>

IReadOnlyObservableDictionary<TKey, TValue>
├── IObservableCollection<KeyValuePair<TKey, TValue>>
└── IReadOnlyDictionary<TKey, TValue>
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

public class TodoItemViewModel
{
    private Todo _model;

    public TodoItemViewModel(Todo model)
    {
        _model = model;
    }

    // Some code.
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
