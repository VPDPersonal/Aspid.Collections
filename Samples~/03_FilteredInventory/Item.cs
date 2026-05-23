namespace Aspid.Collections.Samples.FilteredInventory
{
    public sealed class Item
    {
        public int Level { get; }
        
        public string Name { get; }
        
        public Category Category { get; }

        public Item(string name, Category category, int level)
        {
            Name = name;
            Level = level;
            Category = category;
        }

        public override string ToString() => 
            $"{Name} ({Category}, lvl {Level})";
    }
}
