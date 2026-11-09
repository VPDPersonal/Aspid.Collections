using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;
using Aspid.Collections.Samples.ZoneOccupancy.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.ZoneOccupancy
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for tracking unique visitors with an independently disposable alarm subscription.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(UIDocument)), DefaultExecutionOrder(10)]
    public sealed class ZoneOccupancySample : MonoBehaviour
    {
        private ObservableHashSet<string> _visitors;
        private IReadOnlyObservableCollectionSync<VisualElement> _markers;
        private IObservableEvents<string> _alarm;
        private SamplePanel _ui;
        private int _signals;
        private int _nextVisitor;

        private void OnEnable()
        {
            _ui = new SamplePanel(GetComponent<UIDocument>().rootVisualElement);
            _signals = _nextVisitor = 0;
            _visitors = new ObservableHashSet<string>(StringComparer.OrdinalIgnoreCase);
            _visitors.Add("Nova");
            _visitors.Add("Mira");
            _markers = _visitors.CreateSync(name => SamplePanel.Card(name.Substring(0, 1), name,
                "Inside the perimeter", "UNIQUE VISITOR"), marker => marker.RemoveFromHierarchy());
            _markers.CollectionChanged += Render;
            _ui.Bind("enter", Enter);
            _ui.Bind("duplicate", Duplicate);
            _ui.Bind("leave", Leave);
            _ui.Bind("alarm", ToggleAlarm);
            _ui.Bind("clear", _visitors.Clear);
            AttachAlarm();
            Render(null);
            _ui.Text("status", "Nova and Mira are inside.");
        }

        private void Enter()
        {
            var names = new[] { "Orin", "Ash", "Wren", "Sage", "Lark", "Rune" };
            // Each click picks the next absent visitor; HashSet remains the authority on uniqueness.
            for (var i = 0; i < names.Length; i++)
            {
                var name = names[_nextVisitor++ % names.Length];
                if (!_visitors.Add(name)) continue;
                _ui.Text("status", $"{name} entered.");
                break;
            }
        }

        private void Duplicate()
        {
            var added = _visitors.Add("NOVA");
            _ui.Text("status", added ? "Nova entered." : "Nova is already inside. No new signal.");
        }

        private void Leave()
        {
            string visitor = null;
            foreach (var name in _visitors) { visitor = name; break; }
            if (visitor != null) _visitors.Remove(visitor);
        }

        private void AttachAlarm()
        {
            // Keep the handle: it owns the subscription, separately from the live projection.
            _alarm = _visitors.SplitByEvents(
                added: (items, index) => Signal($"Entered / {items.Count} unique visitor."),
                removed: (items, index) => Signal($"Left / {items.Count} visitor."),
                reset: () => Signal("Reset / perimeter evacuated."));
            _ui.Log("Alarm attached.");
        }

        private void ToggleAlarm()
        {
            if (_alarm == null) AttachAlarm();
            else
            {
                _alarm.Dispose();
                _alarm = null;
                _ui.Log("Alarm detached.");
            }
            Render(null);
            _ui.Text("status", _alarm == null ? "Alarm off. Visitors still update." : "Alarm on. Listening for new changes.");
        }

        private void Signal(string message)
        {
            _signals++;
            _ui.Log(message);
            Render(null);
        }

        private void Render(INotifyCollectionChangedEventArgs<VisualElement> change)
        {
            var items = _ui.Get<VisualElement>("items");
            items.Clear();
            foreach (var marker in _markers) items.Add(marker);
            SamplePanel.Empty(items, "Nobody inside.");
            _ui.Text("metric0", _visitors.Count);
            _ui.Text("metric1", _signals);
            _ui.Text("metric2", _alarm == null ? "OFF" : "ON");
            _ui.Get<Button>("alarm").text = _alarm == null ? "Attach alarm" : "Detach alarm";
            _ui.Enabled("enter", HasAbsentVisitor());
            _ui.Enabled("leave", _visitors.Count > 0);
            _ui.Enabled("clear", _visitors.Count > 0);
            _ui.Text("status", $"{_visitors.Count} inside / {_signals} signals.");
        }

        private bool HasAbsentVisitor()
        {
            foreach (var name in new[] { "Orin", "Ash", "Wren", "Sage", "Lark", "Rune" })
                if (!_visitors.Contains(name)) return true;
            return false;
        }

        private void OnDisable()
        {
            if (_ui == null) return;
            _ui.Dispose();
            _alarm?.Dispose();
            _alarm = null;
            _markers.CollectionChanged -= Render;
            _markers.Dispose();
            _visitors.Dispose();
            _ui = null;
        }
    }
}
