using System;
using System.Collections.Generic;
using System.Linq;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;
using Aspid.Collections.Samples.MatchScoreboard.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.MatchScoreboard
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for ranking keyed player scores while preserving unaffected row objects.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(UIDocument)), DefaultExecutionOrder(10)]
    public sealed class MatchScoreboardSample : MonoBehaviour
    {
        private ObservableDictionary<string, PlayerScore> _players;
        private IReadOnlyObservableDictionarySync<string, ScoreRow> _rows;
        private SamplePanel _ui;
        private int _created;
        private int _released;
        private int _joined;

        private void OnEnable()
        {
            _ui = new SamplePanel(GetComponent<UIDocument>().rootVisualElement);
            _created = _released = _joined = 0;
            _players = new ObservableDictionary<string, PlayerScore>(StringComparer.OrdinalIgnoreCase);
            _players.Add("nova", new PlayerScore("nova", "Nova", 180));
            _players.Add("mira", new PlayerScore("mira", "Mira", 140));
            _players.Add("orin", new PlayerScore("orin", "Orin", 220));
            _rows = _players.CreateSync(CreateRow, isDisposable: true);
            _rows.CollectionChanged += Render;
            _ui.Bind("score", () => Award("nova", 25));
            _ui.Bind("rival", () => Award("mira", 40));
            _ui.Bind("join", Join);
            _ui.Bind("leave", LeaveLeader);
            _ui.Bind("reconnect", Reconnect);
            _ui.Bind("clear", _players.Clear);
            Render(null);
            _ui.Log("3 players connected.");
        }

        private ScoreRow CreateRow(PlayerScore player) =>
            new ScoreRow(player, ++_created, () => _released++);

        private void Award(string id, int points)
        {
            // Replace the value to emit a collection event; editing a POCO would not notify.
            var player = _players[id];
            _players[id] = player.Add(points);
            _ui.Log($"{player.Name}: +{points} points.");
        }

        private void Join()
        {
            var id = $"guest-{++_joined:00}";
            _players.Add(id, new PlayerScore(id, $"Guest {_joined:00}", 100));
            _ui.Log($"{id} joined.");
        }

        private void LeaveLeader()
        {
            var leader = _players.OrderByDescending(pair => pair.Value.Score)
                .ThenBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase).First();
            _players.Remove(leader.Key);
            _ui.Log($"{leader.Key} left.");
        }

        private void Reconnect()
        {
            var existed = _players.TryGetValue("NOVA", out var player);
            // Both the dictionary and its sync projection retain the case-insensitive comparer.
            _players["NOVA"] = existed ? player.Add(10) : new PlayerScore("nova", "Nova", 125);
            _ui.Log(existed ? "Nova reconnected: +10." : "Nova joined: 125 points.");
        }

        private void Render(INotifyCollectionChangedEventArgs<KeyValuePair<string, ScoreRow>> change)
        {
            var items = _ui.Get<VisualElement>("items");
            items.Clear();
            var rank = 0;
            // Dictionary enumeration order is not a ranking contract.
            foreach (var pair in _rows.OrderByDescending(pair => pair.Value.Player.Score)
                         .ThenBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase))
            {
                pair.Value.Rank(++rank);
                items.Add(pair.Value.Element);
            }
            SamplePanel.Empty(items, "No players connected.");
            _ui.Text("metric0", _players.Count);
            _ui.Text("metric1", _created);
            _ui.Text("metric2", _released);
            _ui.Text("status", $"{_players.Count} players connected.");
            _ui.Enabled("score", _players.ContainsKey("nova"));
            _ui.Enabled("rival", _players.ContainsKey("mira"));
            _ui.Enabled("join", _players.Count < 8);
            _ui.Enabled("reconnect", _players.ContainsKey("nova") || _players.Count < 8);
            _ui.Enabled("leave", _players.Count > 0);
            _ui.Enabled("clear", _players.Count > 0);
        }

        private void OnDisable()
        {
            if (_ui == null) return;
            _ui.Dispose();
            _rows.CollectionChanged -= Render;
            _rows.Dispose();
            _players.Dispose();
            _ui = null;
        }
    }
}
