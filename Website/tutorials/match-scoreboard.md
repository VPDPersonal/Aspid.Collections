---
id: match-scoreboard
slug: /match-scoreboard
title: "Match Scoreboard \u2014 rank stable player IDs"
sidebar_label: "Match Scoreboard"
---

# Match Scoreboard — rank stable player IDs

<SampleAvailability />

<SamplePreview sample="match-scoreboard" alt="MatchScoreboard scene in Unity" />

Update one keyed score while preserving the row objects of unaffected players. Import the sample in **Package Manager → Aspid.Collections → Samples**, open `Scenes/MatchScoreboard.unity` and enter Play mode. The built-in UI Toolkit module (`com.unity.modules.uielements`) is required.

Use **Light / Dark** to switch the scene theme. Expand **+ Events** for the log and diagnostic counters.

## Try it

1. Orin starts at 220, Nova at 180, Mira at 140. Note their VIEW IDs.
2. Award +25 twice puts Nova first at 230; only Nova gets replacement row objects.
3. Reconnect NOVA finds the same case-insensitive key and adds 10 without adding a player.
4. Leave first removes the leader. Reconnect Nova again to add them at 125 if absent.
5. End match clears all rows. Join player or reconnect Nova to start again.

## The collection code

```csharp
_players = new ObservableDictionary<string, PlayerScore>(
    StringComparer.OrdinalIgnoreCase);
_rows = _players.CreateSync(CreateRow, isDisposable: true);

var player = _players["nova"];
_players["NOVA"] = player.Add(25); // Replace the existing value.
```

PlayerScore is immutable: Add returns a replacement value, and dictionary assignment emits Replace. Ranking is descending score, then case-insensitive ID. Dictionary enumeration order is not used as rank.

## Ownership

ScoreRow implements IDisposable. The sync projection preserves this ObservableDictionary comparer. Dispose rows before players. The eight-player cap is for the scene.
