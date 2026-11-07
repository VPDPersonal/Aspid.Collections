// Sample 02 — Inventory Sync (Model -> View)
//
// Pattern: keep the model as a pure C# ObservableList<InventoryItem>, and let the
// view side be a synchronized projection ObservableList<InventoryItemView> built
// via CreateSync(). The MonoBehaviour owns the sync handle and disposes it in
// OnDestroy — that detaches the source subscription AND disposes every view.
//
// Two CreateSync overloads are demonstrated:
//   - isDisposable: true -> Dispose() is called on each view that leaves the sync
//   - Action<TTo> remove -> custom callback when a view leaves the sync
//
// Public surface: expose the view via IReadOnlyObservableListSync<InventoryItemView>,
// not the mutable wrapper, so callers can't mutate the projection directly.

using UnityEngine;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

namespace Aspid.Collections.Samples.InventorySync
{
    public sealed class InventorySyncSample : MonoBehaviour
    {
        private ObservableList<InventoryItem> _model;

        // Read-only projection of the view side. Callers iterate this; they can't
        // (and shouldn't) mutate the views directly — mutations go through the model.
        public IReadOnlyObservableListSync<InventoryItemView> Views { get; private set; }

        private void Start()
        {
            _model = new ObservableList<InventoryItem>();

            // The sync is configured to call Dispose() on each InventoryItemView whose model
            // row leaves the list (Remove/RemoveRange/Replace-old/Clear). InventoryItemView's
            // Dispose() destroys its GameObject.
            Views = _model.CreateSync(
                converter: item => new InventoryItemView(item, transform),
                isDisposable: true);

            // Driving the model — single-item and batch operations both propagate.
            _model.Add(new InventoryItem("sword", "Iron Sword", 1));
            _model.Add(new InventoryItem("shield", "Wooden Shield", 1));

            _model.AddRange(
                new InventoryItem("potion", "Healing Potion", 5),
                new InventoryItem("scroll", "Scroll of Teleport", 2));

            // Replace propagates as: old InventoryItemView gets Dispose()d, new one is built.
            _model[0] = new InventoryItem("sword", "Steel Sword", 1);

            // Range remove disposes both views' GameObjects in one event.
            _model.RemoveRange(1, 2);

            Debug.Log($"Model size = {_model.Count}, view size = {Views.Count}");
        }

        private void OnDestroy()
        {
            // ORDER MATTERS — dispose the view side first so it can detach its subscription
            // from the model. Disposing the model first would still work (Dispose just nulls
            // its handler list), but reversing the order makes the ownership graph obvious:
            // the wrapper depends on the source, not the other way round.
            Views?.Dispose();
            _model?.Dispose();
        }
    }
}
