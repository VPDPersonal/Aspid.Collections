namespace Aspid.Collections.Samples.CraftingQueue
{
    internal sealed class WorkOrder
    {
        internal int Id { get; }
        internal string Recipe { get; }

        internal WorkOrder(int id, string recipe)
        {
            Id = id;
            Recipe = recipe;
        }
    }
}
