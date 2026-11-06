// Sample 04 — Dictionary as Keyed Table
//
// ObservableDictionary<TKey, TValue> drives a keyed UI table — think leaderboard
// indexed by playerId. Unlike the list, every event carries a KeyValuePair<TKey,
// TValue>, and the relevant index is the key, not a positional one:
//
//   - Add -> NewItem is the (key, value) pair just inserted
//   - Replace -> OldItem and NewItem share the same key; value changed
//   - Remove -> OldItem is the (key, value) pair just deleted
//   - Reset -> from Clear(); no payload
//
// NewStartingIndex / OldStartingIndex are -1 for the dictionary because position
// is meaningless. Subscribers should dispatch on `args.NewItem.Key`, not on index.

using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

namespace Aspid.Collections.Samples.DictionaryKeyedTable
{
    public sealed class LeaderboardSample : MonoBehaviour
    {
        private ObservableDictionary<string, PlayerScore> _scores;

        // Expose only the read-only projection. Callers can subscribe and read by key,
        // but mutation stays in the owner (this MonoBehaviour).
        public IReadOnlyObservableDictionary<string, PlayerScore> Scores => _scores;

        private void Start()
        {
            _scores = new ObservableDictionary<string, PlayerScore>();
            _scores.CollectionChanged += OnLeaderboardChanged;

            // Add new entries by key.
            _scores.Add("p-001", new PlayerScore("alice", 100));
            _scores.Add("p-002", new PlayerScore("bob",   80));

            // Setting an existing key emits Replace. Setting a missing key emits Add
            // (ObservableDictionary's indexer setter routes to Add() when key is absent).
            _scores["p-001"] = new PlayerScore("alice", 150);

            // Remove by key.
            _scores.Remove("p-002");

            Debug.Log($"Leaderboard size = {_scores.Count}");
        }

        private void OnDestroy() =>
            _scores?.Dispose();

        private static void OnLeaderboardChanged(INotifyCollectionChangedEventArgs<KeyValuePair<string, PlayerScore>> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Debug.Log($"[+] {args.NewItem.Key}: \"{args.NewItem.Value.DisplayName}\" with {args.NewItem.Value.Score}");
                    break;

                case NotifyCollectionChangedAction.Replace:
                    // Same key, different value. Old payload comes via OldItem.
                    Debug.Log($"[~] {args.NewItem.Key}: {args.OldItem.Value.Score} -> {args.NewItem.Value.Score}");
                    break;

                case NotifyCollectionChangedAction.Remove:
                    Debug.Log($"[-] {args.OldItem.Key}: \"{args.OldItem.Value.DisplayName}\"");
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Debug.Log("[*] Leaderboard cleared");
                    break;
            }
        }
    }
}
