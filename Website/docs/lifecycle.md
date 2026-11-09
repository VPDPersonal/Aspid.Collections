# Lifecycle and event safety

## The creator owns the wrapper

Creating `CreateSync`, `CreateFiltered` or `SplitByEvents` creates a subscription. The source can keep that object alive after you drop your own reference. Store the returned handle and dispose it when its consumer goes away.

In Unity, pair `OnEnable` with `OnDisable` if a component can be toggled. First remove UI callbacks and your own event handlers, then release downstream consumers, then sources. The following is teardown order for fields created by such a component:

```csharp
// Stop UI callbacks and your own listeners first.
_cards.CollectionChanged -= OnCardsChanged;
_alarm.Dispose();
_visible.Dispose();
_search.Dispose();
_cards.Dispose();
_source.Dispose();
```

The samples demonstrate complete setup and teardown. A fresh `OnEnable` creates a clean session rather than accumulating handlers.

## Source versus item ownership

Disposing a core collection calls Clear (which emits Reset) and removes its event subscribers. It does not call Dispose on the contained model objects. A sync wrapper can explicitly own destination item cleanup through `isDisposable: true` or a removal callback. It never owns the source itself.

FilteredList disposal only detaches subscriptions and listeners; do not describe it as clearing the data. A disposed filtered view may retain stale indexes. Read-only source interfaces do not include IDisposable; expose them to consumers while keeping the concrete owner in the component or service.

## Reentrancy and exceptions

The dispatch list tolerates subscription changes: new handlers do not join the current snapshot, and removed handlers are skipped. This does not make nested mutations transactional. Avoid changing the source from its own notification while multiple projections are listening.

Exceptions from handlers, converters and cleanup callbacks propagate. The source mutation may already have occurred and later handlers may not run; there is no rollback. Perform fallible validation before mutation and keep notification work small.

## Threads and deferred work

Callbacks run on the caller's thread. Marshal UI changes to the main thread before modifying its source. For delayed processing, copy payload values rather than retaining a batch buffer or an active enumerator. FilteredList needs single-thread access throughout its lifecycle.

See [Zone Occupancy](/tutorials/zone-occupancy) for independent subscriptions and [Squad Roster](/tutorials/squad-roster) for destination ownership.
