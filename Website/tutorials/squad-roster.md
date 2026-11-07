---
id: squad-roster
slug: /squad-roster
title: "Squad Roster \u2014 a squad with stable views"
sidebar_label: "Squad Roster"
---

# Squad Roster — a squad with stable views

<SampleAvailability />

<SamplePreview sample="squad-roster" alt="SquadRoster scene in Unity" />

Recruit a pair, rotate the squad and replace a member. Observe batch notifications, preserved card identities and cleanup. Import the sample in **Package Manager → Aspid.Collections → Samples**, open `Scenes/SquadRoster.unity` and enter Play mode. The built-in UI Toolkit module (`com.unity.modules.uielements`) is required.

Use **Light / Dark** to switch the scene theme. Expand **+ Events** for the log and diagnostic counters.

## Try it

1. Start with 3 members, 3 created cards and no releases.
2. Recruit pair adds two members through one batch notification.
3. Rotate moves the first member to the end; VIEW IDs and creation/release counters stay unchanged.
4. Replace first keeps the squad count and increments both creation and release counters.
5. Dismiss all releases every card. Recruit starts the next squad.

## The collection code

```csharp
// The wrapper converts existing members and subscribes to later source changes.
_cards = _members.CreateSync(CreateCard, isDisposable: true);

_members.AddRange(first, second); // One batch notification.
_members.Move(0, _members.Count - 1); // Existing card objects survive.
_members[0] = replacement; // The previous projected card is disposed.
```

Move reparents existing elements; it does not create new CrewCard objects. Reset has no removed-item payload. DescribeChange handles Reset before choosing single or batch fields.

## Ownership

The component disposes the sync projection before the source. CrewCard implements IDisposable. The eight-member limit belongs to the demonstration.
