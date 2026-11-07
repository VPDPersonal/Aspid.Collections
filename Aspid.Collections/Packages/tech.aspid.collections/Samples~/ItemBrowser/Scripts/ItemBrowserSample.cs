using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using Aspid.Collections.Samples.ItemBrowser.Presentation;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aspid.Collections.Samples.ItemBrowser
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> for searching and sorting a live catalog through chained filtered views.
    /// </summary>
    [DisallowMultipleComponent, RequireComponent(typeof(UIDocument)), DefaultExecutionOrder(10)]
    public sealed class ItemBrowserSample : MonoBehaviour
    {
        private ObservableList<CatalogItem> _stock;
        private FilteredList<CatalogItem> _search;
        private FilteredList<CatalogItem> _visible;
        private SamplePanel _ui;
        private string _query;
        private string _category;
        private bool _descending;
        private int _refreshes;
        private int _delivery;

        private void OnEnable()
        {
            _ui = new SamplePanel(GetComponent<UIDocument>().rootVisualElement);
            _query = _category = "";
            _descending = false;
            _ui.Get<Button>("sort").text = "Price: low first";
            _refreshes = _delivery = 0;
            _stock = new ObservableList<CatalogItem>();
            _search = _stock.CreateFiltered(MatchesSearch);
            _visible = _search.CreateFiltered(MatchesCategory, PriceComparer());
            _visible.CollectionChanged += Render;
            _ui.Search("search", query => { _query = query.Trim(); _search.Update(); });
            _ui.Get<TextField>("search").SetValueWithoutNotify("");
            _ui.Bind("all", () => Category(""));
            _ui.Bind("gear", () => Category("Gear"));
            _ui.Bind("supplies", () => Category("Supplies"));
            _ui.Bind("sort", ToggleSort);
            _ui.Bind("add", Deliver);
            _ui.Bind("discount", Discount);
            _ui.Bind("remove", () => _stock.RemoveAt(0));
            _ui.Bind("reset", Restock);
            Category("");
            Restock();
        }

        private bool MatchesSearch(CatalogItem item) =>
            item.Name.IndexOf(_query, StringComparison.OrdinalIgnoreCase) >= 0;

        private bool MatchesCategory(CatalogItem item) =>
            _category.Length == 0 || item.Category == _category;

        private IComparer<CatalogItem> PriceComparer() => Comparer<CatalogItem>.Create((left, right) =>
        {
            var price = _descending ? right.Price.CompareTo(left.Price) : left.Price.CompareTo(right.Price);
            return price != 0 ? price : StringComparer.OrdinalIgnoreCase.Compare(left.Name, right.Name);
        });

        private void Category(string category)
        {
            _category = category;
            _ui.Get<Button>("all").EnableInClassList("selected", category.Length == 0);
            _ui.Get<Button>("gear").EnableInClassList("selected", category == "Gear");
            _ui.Get<Button>("supplies").EnableInClassList("selected", category == "Supplies");
            // The predicate captures _category; assigning the field alone cannot notify the view.
            _visible.Update();
        }

        private void ToggleSort()
        {
            _descending = !_descending;
            _ui.Get<Button>("sort").text = _descending ? "Price: high first" : "Price: low first";
            _visible.Comparer = PriceComparer();
            _ui.Log(_descending ? "Price: high first." : "Price: low first.");
        }

        private void Deliver()
        {
            _stock.Add(new CatalogItem($"Iron kit {++_delivery:00}", "Gear", 35 + _delivery * 7));
            _ui.Log("Delivery added.");
        }

        private void Discount()
        {
            var item = _stock[0];
            item.Price = Math.Max(1, item.Price - 25);
            // Collections observe structural changes, not properties inside their items.
            // Refresh the upstream view so every downstream filter also updates.
            _search.Update();
            _ui.Log($"{item.Name}: {item.Price} gold / explicit Update().");
        }

        private void Restock()
        {
            _stock.Clear();
            _stock.AddRange(new CatalogItem("Iron sword", "Gear", 120),
                new CatalogItem("Field tonic", "Supplies", 25), new CatalogItem("Oak bow", "Gear", 95),
                new CatalogItem("Iron shield", "Gear", 85), new CatalogItem("Travel ration", "Supplies", 15),
                new CatalogItem("Signal flare", "Supplies", 40), new CatalogItem("Scout cloak", "Gear", 70),
                new CatalogItem("Repair kit", "Supplies", 55));
            _ui.Log("Restocked: 8 items.");
        }

        private void Render()
        {
            _refreshes++;
            var items = _ui.Get<VisualElement>("items");
            items.Clear();
            foreach (var item in _visible)
                items.Add(SamplePanel.Card(item.Category == "Gear" ? "G" : "S", item.Name,
                    item.Category, $"{item.Price} GOLD"));
            SamplePanel.Empty(items, "No matches.");
            _ui.Text("metric0", _visible.Count);
            _ui.Text("metric1", _stock.Count);
            _ui.Text("metric2", _refreshes);
            _ui.Text("status", $"{_visible.Count} of {_stock.Count} items.");
            _ui.Enabled("add", _stock.Count < 24);
            _ui.Enabled("discount", _stock.Count > 0);
            _ui.Enabled("remove", _stock.Count > 0);
        }

        private void OnDisable()
        {
            if (_ui == null) return;
            _ui.Dispose();
            _visible.CollectionChanged -= Render;
            _visible.Dispose();
            _search.Dispose();
            _stock.Dispose();
            _ui = null;
        }
    }
}
