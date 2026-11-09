using System;
using System.Collections.Specialized;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;
using Aspid.Collections.Samples.SquadRoster.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.SquadRoster
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for recruiting a squad with disposable, identity-preserving card projections.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(UIDocument)), DefaultExecutionOrder(10)]
    public sealed class SquadRosterSample : MonoBehaviour
    {
        private ObservableList<CrewMember> _members;
        private IReadOnlyObservableListSync<CrewCard> _cards;
        private SamplePanel _ui;
        private int _nextMember;
        private int _created;
        private int _released;

        private void OnEnable()
        {
            _ui = new SamplePanel(GetComponent<UIDocument>().rootVisualElement);
            _nextMember = _created = _released = 0;
            _members = new ObservableList<CrewMember>();
            _members.AddRange(NewMember(), NewMember(), NewMember());

            // CreateSync also converts members already present in the source.
            _cards = _members.CreateSync(CreateCard, isDisposable: true);
            _cards.CollectionChanged += Render;
            _members.CollectionChanged += DescribeChange;
            _ui.Bind("recruit", () => _members.Add(NewMember()));
            _ui.Bind("pair", () => _members.AddRange(NewMember(), NewMember()));
            _ui.Bind("rotate", () => _members.Move(0, _members.Count - 1));
            _ui.Bind("replace", () => _members[0] = NewMember());
            _ui.Bind("remove", () => _members.RemoveAt(0));
            _ui.Bind("clear", _members.Clear);
            Render(null);
            _ui.Text("status", "Ready: 3 members.");
            _ui.Log("3 members / 3 cards.");
        }

        private CrewMember NewMember()
        {
            var names = new[] { "Nova", "Mira", "Orin", "Ash", "Wren", "Sage", "Lark", "Rune" };
            var roles = new[] { "Pathfinder", "Field medic", "Engineer", "Scout" };
            var id = _nextMember++;
            return new CrewMember(id + 1, names[id % names.Length], roles[id % roles.Length]);
        }

        private CrewCard CreateCard(CrewMember member) =>
            new CrewCard(member, ++_created, () => _released++);

        private void Render(INotifyCollectionChangedEventArgs<CrewCard> change)
        {
            var items = _ui.Get<VisualElement>("items");
            items.Clear();
            // Reparent existing elements in source order; a Move never recreates a card.
            foreach (var card in _cards) items.Add(card.Element);
            SamplePanel.Empty(items, "No members. Recruit someone.");
            _ui.Text("metric0", _members.Count);
            _ui.Text("metric1", _created);
            _ui.Text("metric2", _released);
            _ui.Enabled("recruit", _members.Count < 8);
            _ui.Enabled("pair", _members.Count <= 6);
            _ui.Enabled("rotate", _members.Count > 1);
            foreach (var name in new[] { "replace", "remove", "clear" })
                _ui.Enabled(name, _members.Count > 0);
        }

        private void DescribeChange(INotifyCollectionChangedEventArgs<CrewMember> change)
        {
            string message;
            if (change.Action == NotifyCollectionChangedAction.Reset)
                message = "Squad cleared.";
            else
            {
                // This is the package's own event-args interface, including batch payloads.
                var count = change.IsSingleItem ? 1 : (change.NewItems ?? change.OldItems).Count;
                var payload = change.IsSingleItem ? "single" : $"batch of {count}";
                message = $"{change.Action} / {payload} / {change.OldStartingIndex} -> {change.NewStartingIndex}";
            }
            _ui.Log(message);
            _ui.Text("status", change.Action switch
            {
                NotifyCollectionChangedAction.Add => change.IsSingleItem ? "Member recruited." : $"{change.NewItems.Count} members recruited.",
                NotifyCollectionChangedAction.Remove => "Member dismissed.",
                NotifyCollectionChangedAction.Move => "First member moved to the end.",
                NotifyCollectionChangedAction.Replace => "First member replaced.",
                _ => "Squad cleared.",
            });
        }

        private void OnDisable()
        {
            if (_ui == null) return;
            _ui.Dispose();
            _members.CollectionChanged -= DescribeChange;
            _cards.CollectionChanged -= Render;
            // Release downstream ownership before disposing the source.
            _cards.Dispose();
            _members.Dispose();
            _ui = null;
        }
    }
}
