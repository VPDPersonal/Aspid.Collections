---
id: item-browser
slug: /item-browser
title: "Item Browser \u2014 search a live catalog"
sidebar_label: "Item Browser"
---

# Item Browser — search a live catalog

<SampleAvailability />

<SamplePreview sample="item-browser" alt="ItemBrowser scene in Unity" />

Search, categorize and sort one stock list, then refresh after changing a property. Import the sample in **Package Manager → Aspid.Collections → Samples**, open `Scenes/ItemBrowser.unity` and enter Play mode. The built-in UI Toolkit module (`com.unity.modules.uielements`) is required.

Use **Light / Dark** to switch the scene theme. Expand **+ Events** for the log and diagnostic counters.

## Try it

1. The initial 8 items are sorted by increasing price.
2. Search for iron: Iron shield (85) and Iron sword (120) remain.
3. Supplies gives no matches while IN STOCK stays at 8; switch back to Gear.
4. Discount first twice makes the source-first Iron sword cost 70 and move ahead of the shield.
5. Reverse sorting, add a delivery, remove the first source item, then restock. Filters remain active.

## The collection code

```csharp
_search = _stock.CreateFiltered(MatchesSearch);
_visible = _search.CreateFiltered(MatchesCategory, PriceComparer());

_stock.Add(item); // Both views follow the collection event.

item.Price -= 25; // No collection event is emitted by a property edit.
_search.Update(); // Refresh upstream so the entire chain updates.
```

Predicates capture the query and category. Changing a captured variable or CatalogItem.Price requires Update on the appropriate upstream view. A new Comparer refreshes automatically.

## Ownership

FilteredList only emits a coarse Action, so this UI rebuilds cards on refresh. Dispose visible, then search, then stock. Access the entire chain on one thread.
