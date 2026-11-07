namespace Aspid.Collections.Samples.ItemBrowser
{
    internal sealed class CatalogItem
    {
        internal string Name { get; }
        internal string Category { get; }
        internal int Price { get; set; }

        internal CatalogItem(string name, string category, int price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
