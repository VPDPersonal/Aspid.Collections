// Sample 03 — Filtered Inventory (Search + Categories)
//
// Demonstrates chaining: ObservableList -> FilteredList (category) -> FilteredList
// (search). Each FilteredList subscribes to its source and re-projects incrementally
// when the source changes. Set Filter / Comparer at any time to re-evaluate.
//
// Gotchas this sample makes visible:
//   1. Dispose order is bottom-up. The search wrapper depends on the category wrapper
//      which depends on the source. Dispose them in reverse construction order so each
//      detaches its subscription cleanly.
//   2. FilteredList is single-thread. The source ObservableList<T> guards itself with
//      SyncRoot, but the filter's internal index map is mutated in place from the
//      source's CollectionChanged callback. Touch the chain from one thread only.

using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

namespace Aspid.Collections.Samples.FilteredInventory
{
    public sealed class FilteredInventorySample : MonoBehaviour
    {
        [Tooltip("Category to keep. Change at runtime to re-evaluate the category filter.")]
        [SerializeField] private Category _categoryFilter = Category.Weapon;

        [Tooltip("Sub-string to keep. Empty string disables the search filter.")]
        [SerializeField] private string _searchFilter = "";

        private ObservableList<Item> _source;
        private FilteredList<Item> _bySearch;
        private FilteredList<Item> _byCategory;

        private void Start()
        {
            _source = new ObservableList<Item>(new Item[]
            {
                new("Iron Sword", Category.Weapon, 1),
                new("Steel Sword", Category.Weapon, 5),
                new("Healing Potion", Category.Potion, 1),
                new("Greater Potion", Category.Potion, 3),
                new("Royal Letter", Category.Quest, 1),
            });

            // First wrapper filters by category, and sorts alphabetically by name.
            // Positional args here — there are two CreateFiltered overloads with the same
            // parameter names in different positions, so calling by name would be ambiguous.
            // The lambda fixes the second-arg type to Predicate<T>, picking the correct overload.
            _byCategory = _source.CreateFiltered(
                item => item.Category == _categoryFilter,
                Comparer<Item>.Create((a, b) => string.CompareOrdinal(a.Name, b.Name)));

            // Second wrapper feeds off the first. CreateFiltered detects that the source is
            // an IReadOnlyFilteredList and subscribes to its untyped CollectionChanged event
            // (the inner wrapper re-projects fully whenever the outer source mutates).
            _bySearch = _byCategory.CreateFiltered(item => 
                string.IsNullOrEmpty(_searchFilter) 
                || item.Name.IndexOf(_searchFilter, StringComparison.OrdinalIgnoreCase) >= 0);

            _bySearch.CollectionChanged += OnViewChanged;
            OnViewChanged();

            // Drive the source — both wrappers re-evaluate incrementally.
            _source.Add(new Item("Dragon Slayer", Category.Weapon, 10));
            _source.Insert(0, new Item("Mana Potion", Category.Potion, 2));
        }

        // Public setters that mutate the filters at runtime. Set Filter on a FilteredList
        // and the wrapper rebuilds its index map and fires CollectionChanged.
        public void SetCategory(Category category)
        {
            _categoryFilter = category;
            // Re-assigning Filter forces an internal Update() — needed because we capture
            // _categoryFilter by closure, not via FilteredList's value.
            _byCategory.Filter = item => item.Category == category;
        }

        public void SetSearch(string search)
        {
            _searchFilter = search;
            _bySearch.Filter = item => string.IsNullOrEmpty(search)
                || item.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void OnViewChanged()
        {
            var snapshot = "(none)";
            if (_bySearch.Count > 0)
            {
                var names = new string[_bySearch.Count];
                for (var i = 0; i < _bySearch.Count; i++)
                    names[i] = _bySearch[i].Name;
                snapshot = string.Join(", ", names);
            }

            Debug.Log($"[Filtered view] count={_bySearch.Count}: {snapshot}");
        }

        private void OnDestroy()
        {
            // Reverse construction order: each wrapper detaches its subscription from
            // the source it was built on. Skipping any link here leaves the source
            // holding a live event-handler reference and keeps the chain in memory.
            if (_bySearch is not null)
            {
                _bySearch.CollectionChanged -= OnViewChanged;
                _bySearch.Dispose();
            }
            _byCategory?.Dispose();
            _source?.Dispose();
        }
    }
}
