---
id: zone-occupancy
slug: /zone-occupancy
title: "Zone Occupancy \u2014 unique visitors and alarms"
sidebar_label: "Zone Occupancy"
---

# Zone Occupancy — unique visitors and alarms

<SampleAvailability />

<SamplePreview sample="zone-occupancy" alt="ZoneOccupancy scene in Unity" />

Keep a unique visitor set and independently attach or dispose an alarm subscription. Import the sample in **Package Manager → Aspid.Collections → Samples**, open `Scenes/ZoneOccupancy.unity` and enter Play mode. The built-in UI Toolkit module (`com.unity.modules.uielements`) is required.

Use **Light / Dark** to switch the scene theme. Expand **+ Events** for the log and diagnostic counters.

## Try it

1. Nova and Mira start inside. ALARM SIGNALS is zero because attaching does not replay existing contents.
2. Enter NOVA again returns false without another marker, event or signal.
3. Enter visitor adds the next absent visitor and sends a signal.
4. Detach alarm and change occupancy: markers update, signal count stays unchanged.
5. Attach alarm observes future changes. Evacuate sends one Reset rather than a Remove for each visitor.

## The collection code

```csharp
_markers = _visitors.CreateSync(CreateMarker,
    marker => marker.RemoveFromHierarchy());

_alarm = _visitors.SplitByEvents(
    added: (items, index) => OnEntered(items),
    removed: (items, index) => OnLeft(items),
    reset: OnEvacuated);

_alarm.Dispose(); // The occupancy projection remains connected.
```

Keep the IObservableEvents handle returned by SplitByEvents. It owns the alarm subscription independently of the synchronized markers. Use CreateSync for initial contents and SplitByEvents for future events.

## Ownership

HashSet enumeration order is unspecified; Leave first uses its first enumerated item. Dispose alarm, then markers, then visitors. Re-enabling the component starts a clean session.
