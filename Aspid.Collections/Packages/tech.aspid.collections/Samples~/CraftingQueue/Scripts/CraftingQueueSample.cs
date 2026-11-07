using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;
using Aspid.Collections.Samples.CraftingQueue.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.CraftingQueue
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for completing FIFO work orders and undoing them through a LIFO history.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(UIDocument)), DefaultExecutionOrder(10)]
    public sealed class CraftingQueueSample : MonoBehaviour
    {
        private ObservableQueue<WorkOrder> _pending;
        private ObservableStack<WorkOrder> _history;
        private IReadOnlyObservableCollectionSync<VisualElement> _pendingTickets;
        private IReadOnlyObservableCollectionSync<VisualElement> _historyTickets;
        private SamplePanel _ui;
        private int _nextOrder;
        private int _operations;

        private void OnEnable()
        {
            _ui = new SamplePanel(GetComponent<UIDocument>().rootVisualElement);
            _nextOrder = _operations = 0;
            _pending = new ObservableQueue<WorkOrder>();
            _history = new ObservableStack<WorkOrder>();
            _pending.EnqueueRange(new[] { NewOrder(), NewOrder(), NewOrder() });
            _pendingTickets = _pending.CreateSync(CreateTicket, ticket => ticket.RemoveFromHierarchy());
            _historyTickets = _history.CreateSync(CreateTicket, ticket => ticket.RemoveFromHierarchy());
            _pendingTickets.CollectionChanged += Render;
            _historyTickets.CollectionChanged += Render;
            _ui.Bind("queue", () => Enqueue(false));
            _ui.Bind("batch", () => Enqueue(true));
            _ui.Bind("complete", Complete);
            _ui.Bind("undo", Undo);
            _ui.Bind("clear", Clear);
            Render(null);
            _ui.Text("status", "Next: #01 Iron sword.");
            _ui.Log("3 pending orders.");
        }

        private WorkOrder NewOrder()
        {
            var recipes = new[] { "Iron sword", "Field tonic", "Scout cloak", "Oak bow" };
            var id = ++_nextOrder;
            return new WorkOrder(id, recipes[(id - 1) % recipes.Length]);
        }

        private static VisualElement CreateTicket(WorkOrder order)
        {
            var ticket = new VisualElement();
            ticket.AddToClassList("ticket");
            ticket.Add(SamplePanel.Label(order.Recipe, "card-title"));
            ticket.Add(SamplePanel.Label($"ORDER #{order.Id:00}", "card-detail"));
            return ticket;
        }

        private void Enqueue(bool batch)
        {
            _operations++;
            if (batch) _pending.EnqueueRange(new[] { NewOrder(), NewOrder(), NewOrder() });
            else _pending.Enqueue(NewOrder());
            _ui.Log(batch ? "Queued 3 orders." : "Order queued.");
        }

        private void Complete()
        {
            _operations++;
            if (!_pending.TryDequeue(out var order)) return;
            // The history owns completed domain orders, not the released queue elements.
            _history.Push(order);
            _ui.Log($"Complete #{order.Id:00} / Dequeue -> Push.");
        }

        private void Undo()
        {
            _operations++;
            if (!_history.TryPop(out var order)) return;
            // Undo schedules rework at the tail; it does not rewind other pending orders.
            _pending.Enqueue(order);
            _ui.Log($"Undo #{order.Id:00} / Pop -> queue tail.");
        }

        private void Clear()
        {
            _operations++;
            _pending.Clear();
            _history.Clear();
            _ui.Log("Workbench cleared.");
        }

        private void Render(INotifyCollectionChangedEventArgs<VisualElement> change)
        {
            Fill("pending", _pendingTickets, "No pending orders.");
            Fill("history", _historyTickets, "Nothing completed yet.");
            _ui.Text("metric0", _pending.Count);
            _ui.Text("metric1", _history.Count);
            _ui.Text("metric2", _operations);
            _ui.Text("status", _pending.TryPeek(out var next) ? $"Next: #{next.Id:00} {next.Recipe}." : "Workbench is idle.");
            _ui.Enabled("queue", _pending.Count + _history.Count < 12);
            _ui.Enabled("batch", _pending.Count + _history.Count <= 9);
            _ui.Enabled("complete", _pending.Count > 0);
            _ui.Enabled("undo", _history.Count > 0);
            _ui.Enabled("clear", _pending.Count + _history.Count > 0);
        }

        private void Fill(string name, IReadOnlyObservableCollectionSync<VisualElement> source, string empty)
        {
            var container = _ui.Get<VisualElement>(name);
            container.Clear();
            foreach (var ticket in source) container.Add(ticket);
            SamplePanel.Empty(container, empty);
        }

        private void OnDisable()
        {
            if (_ui == null) return;
            _ui.Dispose();
            _pendingTickets.CollectionChanged -= Render;
            _historyTickets.CollectionChanged -= Render;
            _historyTickets.Dispose();
            _pendingTickets.Dispose();
            _history.Dispose();
            _pending.Dispose();
            _ui = null;
        }
    }
}
