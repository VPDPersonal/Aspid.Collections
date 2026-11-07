# Choose a collection

## Shape your data

| Type | Order and identity | Main operations |
| --- | --- | --- |
| `ObservableList<T>` | Indexed, ordered; duplicates allowed | Add, Insert, RemoveAt, Move, AddRange, InsertRange, RemoveRange |
| `ObservableDictionary<TKey, TValue>` | Stable keys; enumeration order is not a ranking contract | Add, indexer assignment, Remove, TryGetValue |
| `ObservableHashSet<T>` | Unique items according to its comparer; order unspecified | Add, Remove, Contains |
| `ObservableQueue<T>` | First in, first out | Enqueue, EnqueueRange, Dequeue, TryDequeue, DequeueRange |
| `ObservableStack<T>` | Last in, first out | Push, PushRange, Pop, TryPop, PopRange |

All five support `Clear`, `Dispose`, change notifications, `SyncRoot` and `CreateSync`. Dictionary and HashSet have no public range mutation methods. For Queue/Stack range removal, pass an output array whose length is the number of items to remove; insufficient source items are rejected before mutation.

## Expose a read-only contract

| Source | Consumer interface |
| --- | --- |
| List | `IReadOnlyObservableList<T>` |
| Dictionary | `IReadOnlyObservableDictionary<TKey, TValue>` |
| Queue, Stack, HashSet | `IObservableCollection<T>` |
| Filtered view | `IReadOnlyFilteredList<T>` |

List, collection and filtered interfaces are covariant in their item type. The dictionary interface is invariant. These source/view interfaces do not own disposal: keep the concrete owner separately. `CreateSync` returns disposable read-only sync interfaces.

## Equality and keys

Pass a comparer to Dictionary or HashSet when identity needs case-insensitive strings or another equality rule. A duplicate set addition returns `false` without notifying. Dictionary assignment replaces an existing key or adds a missing one. For a leaderboard, sort a display projection explicitly rather than relying on dictionary enumeration order.

`ObservableListExtensions.Swap` lives in `Aspid.Collections.Observable.Extensions`. It is implemented with moves, so listeners can observe multiple notifications; it is not an atomic bulk change.

## Threading

Core mutation and enumeration are guarded by `SyncRoot`. Notifications run synchronously on the mutating thread, inside the mutation's lock. Core enumerators hold that lock throughout active enumeration. Dictionary `Keys` and `Values` are live views; their getters do not lock later enumeration.

This is not a general cross-thread UI binding guarantee. Manage subscription changes yourself, marshal UI work to its owning thread, and use a single thread for `FilteredList`. Avoid holding enumerators across asynchronous work. See [lifecycle and event safety](lifecycle.md).
