namespace Aspid.Collections.Samples.InventorySync
{
    // Plain model. Lives in pure C# land — no MonoBehaviour, no GameObject.
    public sealed class InventoryItem
    {
        public string Id { get; }
        
        public int Quantity { get; }
        
        public string DisplayName { get; }

        public InventoryItem(string id, string displayName, int quantity)
        {
            Id = id;
            Quantity = quantity;
            DisplayName = displayName;
        }
    }
}
