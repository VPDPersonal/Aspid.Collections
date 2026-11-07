# Synchronized views

## One destination per source item

Import `Aspid.Collections.Observable.Synchronizer` and call `CreateSync`. It converts existing content immediately and follows future edits. The return type is read-only and disposable.

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

using var names = new ObservableList<string> { "Nova", "Mira" };
using var rows = names.CreateSync(name => new MemberRow(name), isDisposable: true);
names.Move(0, 1); // Keep the same MemberRow objects.
names.RemoveAt(0); // Dispose only the removed row.

sealed class MemberRow : IDisposable
{
    public string Name { get; }
    public MemberRow(string name) => Name = name;
    public void Dispose() => Console.WriteLine($"Released {Name}");
}
```

## Choose cleanup ownership

| Overload | Removed destination items |
| --- | --- |
| `CreateSync(converter)` | No automatic item cleanup |
| `CreateSync(converter, isDisposable: true)` | Calls IDisposable on destination items that implement it |
| `CreateSync(converter, removed)` | Calls your explicit removal callback |

Cleanup applies to removal, replacement, reset and wrapper disposal. List.Move keeps destination identity. Replacing a List/Dictionary item with the same destination reference does not dispose that retained object. The wrapper does not dispose its source, and disposing a source never automatically disposes its model elements.

## Five source shapes

List returns `IReadOnlyObservableListSync<TTo>`, Dictionary returns `IReadOnlyObservableDictionarySync<TKey, TTo>`, and Queue/Stack/HashSet return `IReadOnlyObservableCollectionSync<TTo>`. Queue and Stack preserve their FIFO/LIFO order, including pre-existing stack content.

List, Queue and Stack forward supported range notifications. Dictionary/HashSet sync can consume batches from a compatible custom source, but emit their resulting changes one item at a time. They have no public range mutation API themselves.

## Converter and comparer limits

- A HashSet converter must produce unique destination values under `EqualityComparer<TTo>.Default`. A collision throws. The source comparer identifies source items in the internal map; it is not a comparer for the destination type.
- Dictionary sync preserves the comparer when the source is an `ObservableDictionary<TKey, TFrom>` (or subclass). A different implementation of the read-only dictionary interface uses the default key comparer.
- Keep converters and cleanup callbacks predictable and non-throwing. Mutation is not rolled back if they throw; some consumers may already have processed it.

See [Squad Roster](/tutorials/squad-roster), [Crafting Queue](/tutorials/crafting-queue), and [Match Scoreboard](/tutorials/match-scoreboard).
