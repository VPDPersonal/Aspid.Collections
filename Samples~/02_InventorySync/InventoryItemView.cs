using System;
using UnityEngine;

namespace Aspid.Collections.Samples.InventorySync
{
    // The view side of the sync — a GameObject wrapper around a single InventoryItem.
    // Implements IDisposable so the *Sync wrapper can clean it up when its model row
    // is removed (we pass isDisposable: true when creating the sync below).
    public sealed class InventoryItemView : IDisposable
    {
        public InventoryItem Item { get; }
        
        public GameObject GameObject { get; }

        public InventoryItemView(InventoryItem item, Transform parent)
        {
            Item = item;
            GameObject = new GameObject($"InventoryItemView[{item.Id}]");
            GameObject.transform.SetParent(parent, worldPositionStays: false);
        }

        public void Dispose()
        {
            // The *Sync wrapper invokes Dispose() on us when our row leaves the source list
            // (Remove, RemoveRange, Replace's old element, or Clear). Destroy the visual.
            if (GameObject != null)
                UnityEngine.Object.Destroy(GameObject);
        }
    }
}
