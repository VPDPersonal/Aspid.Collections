# Aspid.Collections

[![Unity 2021.3+](https://img.shields.io/badge/2021.3%2B-000000?style=flat&logo=unity&logoColor=white&color=4fa35d)](https://unity.com/)
[![Releases](https://img.shields.io/github/v/release/VPDPersonal/Aspid.Collections?label=Release&labelColor=254d2c&color=4fa35d)](https://github.com/VPDPersonal/Aspid.Collections/releases)
[![License](https://img.shields.io/github/license/VPDPersonal/Aspid.Collections?label=License&labelColor=254d2c&color=4fa35d)](LICENSE)

Observable collections library with support for covariance, collection synchronization, filtering, and sorting.

## Table of Contents

- [Installation](#installation)
- [Key Features](#key-features)
- [Collections](#collections)
  - [ObservableList](#observablelist)
  - [ObservableDictionary](#observabledictionary)
  - [ObservableHashSet](#observablehashset)
  - [ObservableQueue](#observablequeue)
  - [ObservableStack](#observablestack)
- [Interfaces](#interfaces)
- [Events](#events)
- [Collection Synchronization](#collection-synchronization)
- [Filtering and Sorting](#filtering-and-sorting)
- [Usage Examples](#usage-examples)

## Installation
1. Add the package to your Unity project via Package Manager:
   - Aspid.Collections: `https://github.com/VPDPersonal/Aspid.Collections.git`
2. _(Optional)_ Add Aspid.Internal.Unity for custom script icons — runtime works without it:
   - Aspid.Internal.Unity: `https://github.com/VPDPersonal/Aspid.Internal.Unity.git`
3. Or download .unitypackage from the [release page on GitHub](https://github.com/VPDPersonal/Aspid.Collections/releases) and import it into the project.

## Key Features

- 🔔 **Observable Collections** — automatic change notifications
- 🔄 **Synchronization** — automatic synchronization between collections with type conversion
- 🔍 **Filtering** — dynamic filtering with automatic updates
- 📊 **Sorting** — dynamic sorting without modifying the source collection
- ✨ **Covariance** — support for covariant interfaces

## Collections

### ObservableList
```csharp
using Aspid.Collections.Observable;

// Creation
var list = new ObservableList<string>();
var listWithCapacity = new ObservableList<string>(10);
var listFromCollection = new ObservableList<string>(new[] { "a", "b", "c" });

// Subscribe to changes
list.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Basic operations
list.Add("item");
list.Insert(0, "first");
list[0] = "updated";
bool removed = list.Remove("item");
list.RemoveAt(0);
list.Clear();

// Clear list and events
list.Dispose();

// Batch operations
list.AddRange(new[] { "a", "b", "c" });
list.InsertRange(0, new[] { "x", "y" });

// Move
list.Move(0, 2); // Move element from index 0 to index 2

// Queries / inspection
int index = list.IndexOf("item");
bool contains = list.Contains("item");
list.ForEach(item => Console.WriteLine(item));

// Copy to array
var buffer = new string[list.Count];
list.CopyTo(buffer, 0);
```

Extensions (`Aspid.Collections.Observable.Extensions`):

```csharp
using Aspid.Collections.Observable.Extensions;

list.Swap(0, 3); // Swap two elements by index
```

### ObservableDictionary
```csharp
using Aspid.Collections.Observable;

// Creation
var dict = new ObservableDictionary<string, int>();
var dictWithCapacity = new ObservableDictionary<string, int>(capacity: 16);
var dictWithComparer = new ObservableDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
var dictWithBoth = new ObservableDictionary<string, int>(capacity: 16, StringComparer.OrdinalIgnoreCase);
var dictFromCollection = new ObservableDictionary<string, int>(
    new[] { KeyValuePair.Create("a", 1), KeyValuePair.Create("b", 2) }
);

// Subscribe to changes
dict.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
dict.Add("key", 42);
dict["key"] = 100;        // Replace if key exists
dict["newKey"] = 200;     // Add if key doesn't exist
bool removed = dict.Remove("key");
dict.Clear();

// Clear dictionary and events
dict.Dispose();

// Data access
bool exists = dict.TryGetValue("key", out var value);
bool contains = dict.ContainsKey("key");
bool containsPair = dict.Contains(KeyValuePair.Create("key", 100));

// Enumerations / introspection
IEnumerable<string> keys = dict.Keys;
IEnumerable<int> values = dict.Values;
IEqualityComparer<string> comparer = dict.Comparer;
```

### ObservableHashSet
```csharp
using Aspid.Collections.Observable;

// Creation
var set = new ObservableHashSet<string>();
var setWithComparer = new ObservableHashSet<string>(StringComparer.OrdinalIgnoreCase);
var setFromCollection = new ObservableHashSet<string>(new[] { "a", "b", "c" });

// Subscribe to changes
set.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
bool added = set.Add("item");
bool removed = set.Remove("item");
set.Clear();

// Clear set and events
set.Dispose();

// Queries
bool contains = set.Contains("item");
IEqualityComparer<string> comparer = set.Comparer;

// Set operations (read-only; do not mutate the source)
var other = new[] { "a", "b", "c" };
bool isSub       = set.IsSubsetOf(other);
bool isSuper     = set.IsSupersetOf(other);
bool isProperSub = set.IsProperSubsetOf(other);
bool isProperSup = set.IsProperSupersetOf(other);
bool overlaps    = set.Overlaps(other);
bool equals      = set.SetEquals(other);
```

> `ObservableHashSet<T>` intentionally does **not** implement `ISet<T>`:
> mutating set operations (`UnionWith`, `IntersectWith`, …) can't emit
> accurate added/removed notifications, so only the read-only set
> predicates above are exposed.

### ObservableQueue
```csharp
using Aspid.Collections.Observable;

// Creation
var queue = new ObservableQueue<string>();
var queueWithCapacity = new ObservableQueue<string>(10);
var queueFromCollection = new ObservableQueue<string>(new[] { "a", "b", "c" });
var queueWrappingExisting = new ObservableQueue<string>(new Queue<string>()); // wraps, does not copy

// Subscribe to changes
queue.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
string peek = queue.Peek();
bool hasPeek = queue.TryPeek(out var peekResult);

queue.Enqueue("item");
queue.EnqueueRange(new[] { "a", "b", "c" });

string item = queue.Dequeue();
bool success = queue.TryDequeue(out var result);

queue.Clear();

// Clear queue and events
queue.Dispose();

// Batch dequeue
var buffer = new string[3];
queue.DequeueRange(buffer);

// Snapshot / capacity
string[] snapshot = queue.ToArray();
queue.TrimExcess();
```

### ObservableStack
```csharp
using Aspid.Collections.Observable;

// Creation
var stack = new ObservableStack<string>();
var stackWithCapacity = new ObservableStack<string>(10);
var stackFromCollection = new ObservableStack<string>(new[] { "a", "b", "c" });

// Subscribe to changes
stack.CollectionChanged += args =>
{
    Console.WriteLine($"Action: {args.Action}");
};

// Operations
stack.Push("item");
stack.PushRange(new[] { "a", "b", "c" });

string peek = stack.Peek();
bool hasPeek = stack.TryPeek(out var peekResult);

string item = stack.Pop();
bool success = stack.TryPop(out var result);

stack.Clear();

// Clear stack and events
stack.Dispose();

// Batch pop
var buffer = new string[3];
stack.PopRange(buffer);

// Snapshot / capacity
string[] snapshot = stack.ToArray();
stack.TrimExcess();
```

## Interfaces

All interfaces expose their element type as **covariant** (`out T`), so
you can assign e.g. `IReadOnlyObservableList<Cat>` to
`IReadOnlyObservableList<Animal>`.

### Core Interfaces

| Interface | Description |
|-----------|-------------|
| `IObservableCollection<out T>` | Base interface for all observable collections |
| `IReadOnlyObservableList<out T>` | Read-only list with notifications |
| `IReadOnlyObservableDictionary<TKey, TValue>` | Read-only dictionary with notifications |

### Synchronization Interfaces (`Aspid.Collections.Observable.Synchronizer`)

Returned by `CreateSync(...)` extensions. Every sync wrapper implements
`IDisposable` and owns its subscription to the source collection.

| Interface | Description |
|-----------|-------------|
| `IReadOnlyObservableCollectionSync<out T>` | Base for synced non-list collections (Queue/Stack/HashSet) |
| `IReadOnlyObservableListSync<out T>` | Synced list projection (extends `IReadOnlyObservableList<T>`) |
| `IReadOnlyObservableDictionarySync<TKey, T>` | Synced dictionary projection |

### Filtering Interfaces (`Aspid.Collections.Observable.Filtered`)

| Interface | Description |
|-----------|-------------|
| `IReadOnlyFilteredList<out T>` | Read-only filtered/sorted view over an `IReadOnlyList<T>` (raises a parameterless `CollectionChanged`) |

### Event Handling Interfaces

| Interface | Description |
|-----------|-------------|
| `IObservableEvents<out T>` | Disposable wrapper returned by `SplitByEvents(...)` exposing individual `Added` / `Removed` / `Moved` / `Replaced` / `Reset` events |

### Interface Hierarchy
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

## Events
### NotifyCollectionChangedEventArgs

Collection change event arguments structure:

```csharp
public readonly struct NotifyCollectionChangedEventArgs<T>
{
    // Add, Remove, Replace, Move, Reset
    public NotifyCollectionChangedAction Action { get; }
    
    // true for single-item operations
    public bool IsSingleItem { get; }                    
    
    // For single-item operations
    public T? NewItem { get; }
    public T? OldItem { get; }
    
    // For batch operations
    public IReadOnlyList<T>? NewItems { get; }
    public IReadOnlyList<T>? OldItems { get; }
    
    // Indices
    public int NewStartingIndex { get; }
    public int OldStartingIndex { get; }
}
```

### Action Types
| Action | Description |
|--------|-------------|
| `Add` | New items added |
| `Remove` | Items removed |
| `Replace` | Item replaced with another |
| `Move` | Item moved to new position |
| `Reset` | Collection cleared |

### Split Events (SplitByEvents)
For convenient handling of different change types, use the `SplitByEvents` extension:

```csharp
using Aspid.Collections.Observable;

var list = new ObservableList<string>();

// Subscribe to individual events
var events = list.SplitByEvents(
    added: (items, index) => Console.WriteLine($"Added {items.Count} items at {index}"),
    removed: (items, index) => Console.WriteLine($"Removed {items.Count} items from {index}"),
    moved: (items, oldIndex, newIndex) => Console.WriteLine($"Moved from {oldIndex} to {newIndex}"),
    replaced: (oldItems, newItems, index) => Console.WriteLine($"Replaced at {index}"),
    reset: () => Console.WriteLine("Collection cleared")
);

// Don't forget to dispose
events.Dispose();
```

## Collection Synchronization
Automatic synchronization allows creating a "mirror" collection with type conversion.

### Creating a Synchronized Collection
```csharp
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

// Source collection of models
var models = new ObservableList<UserModel>();

// Create synchronized collection of view models
var viewModels = models.CreateSync(
    model => new UserViewModel(model),  // Converter
    isDisposable: true                  // Auto-call Dispose on removal
);

// Or with custom removal handler
var viewModels2 = models.CreateSync(
    model => new UserViewModel(model),
    removed: vm => vm.Cleanup()
);
// Note: for ObservableQueue / ObservableStack / ObservableHashSet /
// IReadOnlyObservableDictionary the removal callback parameter is named
// `remove:` (not `removed:`). Using a positional argument avoids the
// mismatch.

// All changes in models are automatically reflected in viewModels
models.Add(new UserModel { Name = "John" });
// viewModels now contains UserViewModel for John

// Don't forget to dispose
viewModels.Dispose();
```

### Supported Collections for Synchronization
| Source Collection | Extension Method | Result |
|-------------------|------------------|--------|
| `IReadOnlyObservableList<T>` | `CreateSync()` | `IReadOnlyObservableListSync<T>` |
| `ObservableQueue<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `ObservableStack<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `ObservableHashSet<T>` | `CreateSync()` | `IReadOnlyObservableCollectionSync<T>` |
| `IReadOnlyObservableDictionary<K,V>` | `CreateSync()` | `IReadOnlyObservableDictionarySync<K,T>` |

## Filtering and Sorting
`FilteredList<T>` provides dynamic filtering and sorting without modifying the source collection.

### Creating a Filtered List

```csharp
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

var list = new ObservableList<int> { 5, 2, 8, 1, 9, 3 };

// Filter only
var filtered = list.CreateFiltered(x => x > 3);
// filtered contains: 5, 8, 9

// Sort only
var sorted = list.CreateFiltered(Comparer<int>.Default);
// sorted contains: 1, 2, 3, 5, 8, 9

// Filter and sort
var filteredAndSorted = list.CreateFiltered(
    filter: x => x > 2,
    comparer: Comparer<int>.Default
);
// filteredAndSorted contains: 3, 5, 8, 9
```

### Dynamic Filter Changes

```csharp
var filtered = list.CreateFiltered();

// Subscribe to changes
filtered.CollectionChanged += () =>
{
    Console.WriteLine("Filtered collection updated");
};

// Dynamic filter change
filtered.Filter = x => x > 5;

// Dynamic sort change
filtered.Comparer = Comparer<int>.Create((a, b) => b.CompareTo(a)); // Reverse order

// Force update
filtered.Update();
```

### Filter Chaining

`FilteredList` can be used as a source for another `FilteredList`:

```csharp
var list = new ObservableList<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNumbers = list.CreateFiltered(x => x % 2 == 0);
// evenNumbers: 2, 4, 6, 8, 10

var largeEvenNumbers = evenNumbers.CreateFiltered(x => x > 5);
// largeEvenNumbers: 6, 8, 10
```

## Usage Examples
### MVVM Pattern with Synchronization

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

### Observable List with Filtering
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
    
    private void RefreshView() { /* Update UI */ }
    
    public void Dispose() => _visibleItems.Dispose();
}
```

### Reactive Event Handling

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
MIT License - see [LICENSE](LICENSE) file for details.

