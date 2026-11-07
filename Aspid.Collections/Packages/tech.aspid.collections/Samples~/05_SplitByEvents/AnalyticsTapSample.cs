// Sample 05 — SplitByEvents (Analytics Tap)
//
// SplitByEvents() takes one fat CollectionChanged event and splits it into
// per-action callbacks (Added / Removed / Replaced / Moved / Reset). The
// extension returns an IObservableEvents<T> handle that owns the subscription:
// hold the handle for as long as you want the tap alive, Dispose() it to detach.
//
// Why this matters: subscribing to CollectionChanged += ... directly is easy to
// get wrong — people forget to unsubscribe on scene unload and leak the source.
// The Dispose-as-subscription-token pattern makes ownership explicit.

using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable;

namespace Aspid.Collections.Samples.SplitByEvents
{
    public sealed class AnalyticsTapSample : MonoBehaviour
    {
        private ObservableList<string> _list;

        // The tap is the source of truth for "this MonoBehaviour holds an active
        // subscription on _list". Dispose it in OnDestroy.
        private IObservableEvents<string> _tap;

        private void Start()
        {
            _list = new ObservableList<string>();

            _tap = _list.SplitByEvents(
                added: OnAdded,
                removed: OnRemoved,
                replaced: OnReplaced,
                moved: OnMoved,
                reset: OnReset);

            // Each operation hits exactly one callback above. Single-item events
            // surface as a one-element IReadOnlyList<T> on the Added/Removed side,
            // so callers don't have to branch on IsSingleItem themselves.
            _list.Add("first");
            _list.AddRange("second", "third");
            _list[0] = "FIRST";
            _list.Move(0, 2);
            _list.RemoveAt(0);
            _list.Clear();
        }

        private void OnDestroy()
        {
            // Disposing the tap unsubscribes from _list and frees the callback delegates.
            // After this, mutations to _list will no longer reach OnAdded / etc.
            _tap?.Dispose();
            _list?.Dispose();
        }

        private static void OnAdded(IReadOnlyList<string> items, int startIndex) =>
            Debug.Log($"[analytics:add]     start={startIndex} items=[{string.Join(",", items)}]");

        private static void OnRemoved(IReadOnlyList<string> items, int startIndex) =>
            Debug.Log($"[analytics:remove]  start={startIndex} items=[{string.Join(",", items)}]");

        private static void OnReplaced(IReadOnlyList<string> oldItems, IReadOnlyList<string> newItems, int startIndex) =>
            Debug.Log($"[analytics:replace] at={startIndex} [{string.Join(",", oldItems)}] -> [{string.Join(",", newItems)}]");

        private static void OnMoved(IReadOnlyList<string> items, int oldIndex, int newIndex) =>
            Debug.Log($"[analytics:move]    {oldIndex}->{newIndex} items=[{string.Join(",", items)}]");

        private static void OnReset() =>
            Debug.Log("[analytics:reset]");
    }
}
