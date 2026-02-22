using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Filtered
{
    public sealed class FilteredList<T> : IReadOnlyFilteredList<T>, IDisposable
    {
        public event Action? CollectionChanged;
        
        private int[]? _indexes;
        private Predicate<T>? _filter;
        private IComparer<T>? _comparer;
        private readonly IReadOnlyList<T> _list;
        
        public Predicate<T>? Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                Update();
            }
        }
        
        public IComparer<T>? Comparer
        {
            get => _comparer;
            set
            {
                _comparer = value;
                Update();
            }
        }
        
        public int Count { get; private set; }
        
        public T this[int index] => _indexes is null 
            ? _list[index] 
            : _list[_indexes[index]];

        public FilteredList(IReadOnlyList<T> list)
        {
            _list = list;
            Count = _list.Count;

            switch (list)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += Update; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += OnCollectionChanged; break;
            }
        }
        
        public FilteredList(IReadOnlyList<T> list, IComparer<T>? comparer, Predicate<T>? filter = null)
            : this(list, filter, comparer) { }

        public FilteredList(IReadOnlyList<T> list, Predicate<T>? filter, IComparer<T>? comparer = null)
            : this(list)
        {
            _filter = filter;
            _comparer = comparer;
            Update();
        }
        
        public void Update()
        {
            if (Comparer is not null && Filter is not null)
            {
                _indexes = Enumerable.Range(0, _list.Count)
                    .Where(i => Filter(_list[i]))
                    .OrderBy(i => _list[i], Comparer)
                    .ToArray();
            }
            else if (Comparer is not null)
            {
                _indexes = Enumerable.Range(0, _list.Count)
                    .OrderBy(i => _list[i], Comparer)
                    .ToArray();
            }
            else if (Filter is not null)
            {
                _indexes = Enumerable.Range(0, _list.Count)
                    .Where(i => Filter(_list[i]))
                    .ToArray();
            }
            else
            {
                _indexes = null;
            }

            Count = _indexes?.Length ?? _list.Count;
            CollectionChanged?.Invoke();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            if (_indexes is not null)
            {
                foreach (var index in _indexes)
                    yield return _list[index];
            }
            else
            {
                foreach (var item in _list)
                    yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        
        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e) =>
            Update();

        public void Dispose()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged -= Update; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged -= OnCollectionChanged; break;
            }
            
            CollectionChanged = null;
        }
    }
}