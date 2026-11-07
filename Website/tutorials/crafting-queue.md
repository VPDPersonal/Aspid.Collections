---
id: crafting-queue
slug: /crafting-queue
title: "Crafting Queue \u2014 production and undo"
sidebar_label: "Crafting Queue"
---

# Crafting Queue — production and undo

<SampleAvailability />

<SamplePreview sample="crafting-queue" alt="CraftingQueue scene in Unity" />

Complete pending FIFO work orders and undo the most recent completion with a LIFO stack. Import the sample in **Package Manager → Aspid.Collections → Samples**, open `Scenes/CraftingQueue.unity` and enter Play mode. The built-in UI Toolkit module (`com.unity.modules.uielements`) is required.

Use **Light / Dark** to switch the scene theme. Expand **+ Events** for the log and diagnostic counters.

## Try it

1. The queue starts with orders #01, #02 and #03. Complete twice.
2. The queue now holds #03; history shows #02 above #01.
3. Undo last pops #02 and enqueues it after #03.
4. Queue batch adds three orders through one EnqueueRange.
5. Clear workbench clears both views. Queue item restarts production.

## The collection code

```csharp
_pendingTickets = _pending.CreateSync(
    CreateTicket, ticket => ticket.RemoveFromHierarchy());
_historyTickets = _history.CreateSync(
    CreateTicket, ticket => ticket.RemoveFromHierarchy());

if (_pending.TryDequeue(out var order))
    _history.Push(order);

if (_history.TryPop(out var completed))
    _pending.Enqueue(completed);
```

Domain WorkOrder objects move between collections. Queue tickets are released on completion and history creates its own tickets. The explicit removal callback detaches VisualElement instances, which are not IDisposable.

## Ownership

Dispose both projections before their sources. Undo means rework at the queue tail, not restoring the original index or refunding resources. The demonstration caps total orders at twelve.
