using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Filtered
{
    public sealed class FilteredList<T> : IReadOnlyFilteredList<T>, IDisposable
    {
        public event Action? CollectionChanged;

        private List<int>? _indexes;
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
            SubscribeToSource();
        }

        public FilteredList(IReadOnlyList<T> list, IComparer<T>? comparer, Predicate<T>? filter = null)
            : this(list, filter, comparer) { }

        public FilteredList(IReadOnlyList<T> list, Predicate<T>? filter, IComparer<T>? comparer = null)
        {
            _list = list;
            _filter = filter;
            _comparer = comparer;

            SubscribeToSource();
            Update();
        }

        private void SubscribeToSource()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += Update; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += OnCollectionChanged; break;
            }
        }

        public void Update()
        {
            if (_comparer is not null && _filter is not null)
            {
                _indexes = Enumerable.Range(0, _list.Count)
                    .Where(index => _filter(_list[index]))
                    .OrderBy(index => _list[index], _comparer)
                    .ToList();
            }
            else if (_comparer is not null)
            {
                _indexes = Enumerable.Range(0, _list.Count)
                    .OrderBy(index => _list[index], _comparer)
                    .ToList();
            }
            else if (_filter is not null)
            {
                _indexes = Enumerable.Range(0, _list.Count)
                    .Where(index => _filter(_list[index]))
                    .ToList();
            }
            else
            {
                _indexes = null;
            }

            Count = _indexes?.Count ?? _list.Count;
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

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (args.IsSingleItem) ApplyAddSingle(args.NewStartingIndex, args.NewItem!);
                    else ApplyAddBatch(args.NewStartingIndex, args.NewItems!);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (args.IsSingleItem) ApplyRemoveSingle(args.OldStartingIndex);
                    else ApplyRemoveBatch(args.OldStartingIndex, args.OldItems!.Count);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (args.IsSingleItem)
                    {
                        ApplyReplaceSingle(args.NewStartingIndex, args.NewItem!);
                    }
                    else
                    {
                        Update();
                        return;
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    if (args.IsSingleItem)
                    {
                        ApplyMoveSingle(args.OldStartingIndex, args.NewStartingIndex);
                    }
                    else
                    {
                        Update();
                        return;
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                default: Update(); return;
            }

            Count = _indexes?.Count ?? _list.Count;
            CollectionChanged?.Invoke();
        }

        #region Apply Add
        private void ApplyAddSingle(int sourceIndex, T item)
        {
            if (_indexes is null) return;

            ShiftIndexesGreaterOrEqual(
                threshold: sourceIndex,
                delta: 1);

            if (_filter is not null && !_filter(item)) return;

            var index = _comparer is not null
                ? BinarySearchInsertByValue(sourceIndex)
                : BinarySearchInsertBySource(sourceIndex);

            _indexes.Insert(index, sourceIndex);
        }

        private void ApplyAddBatch(int startIndex, IReadOnlyList<T> items)
        {
            if (_indexes is null) return;

            var delta = items.Count;
            ShiftIndexesGreaterOrEqual(
                threshold: startIndex,
                delta: delta);

            for (var offset = 0; offset < delta; offset++)
            {
                var sourceIndex = startIndex + offset;
                var item = items[offset];
                if (_filter is not null && !_filter(item)) continue;

                var index = _comparer is not null
                    ? BinarySearchInsertByValue(sourceIndex)
                    : BinarySearchInsertBySource(sourceIndex);

                _indexes.Insert(index, sourceIndex);
            }
        }
        #endregion

        private void ApplyRemoveSingle(int sourceIndex)
        {
            if (_indexes is null) return;

            var position = FindViewPosition(sourceIndex);
            if (position >= 0) _indexes.RemoveAt(position);
            ShiftIndexesGreaterThan(sourceIndex, -1);
        }

        private void ApplyRemoveBatch(int startIndex, int count)
        {
            if (_indexes is null) return;

            for (var sourceIndex = startIndex + count - 1; sourceIndex >= startIndex; sourceIndex--)
            {
                var position = FindViewPosition(sourceIndex);
                if (position >= 0) _indexes.RemoveAt(position);
            }
            ShiftIndexesGreaterThan(startIndex + count - 1, -count);
        }

        private void ApplyReplaceSingle(int sourceIndex, T newItem)
        {
            if (_indexes is null) return;

            var oldPosition = FindViewPosition(sourceIndex);
            var wasIncluded = oldPosition >= 0;
            var nowIncluded = _filter is null || _filter(newItem);

            if (!wasIncluded && !nowIncluded) return;

            if (wasIncluded && !nowIncluded)
            {
                _indexes.RemoveAt(oldPosition);
                return;
            }

            if (!wasIncluded)
            {
                var position = _comparer is not null
                    ? BinarySearchInsertByValue(sourceIndex)
                    : BinarySearchInsertBySource(sourceIndex);
                _indexes.Insert(position, sourceIndex);
                return;
            }

            // wasIncluded && nowIncluded — reseat only when sort key may have changed.
            if (_comparer is null) return;

            _indexes.RemoveAt(oldPosition);
            var insertPosition = BinarySearchInsertByValue(sourceIndex);
            _indexes.Insert(insertPosition, sourceIndex);
        }

        private void ApplyMoveSingle(int oldIndex, int newIndex)
        {
            if (_indexes is null) return;
            if (oldIndex == newIndex) return;

            // Remap source indices stored in _indexes to reflect the source rearrangement.
            if (oldIndex < newIndex)
            {
                for (var index = 0; index < _indexes.Count; index++)
                {
                    var value = _indexes[index];
                    if (value == oldIndex) _indexes[index] = newIndex;
                    else if (value > oldIndex && value <= newIndex) _indexes[index] = value - 1;
                }
            }
            else
            {
                for (var index = 0; index < _indexes.Count; index++)
                {
                    var value = _indexes[index];
                    if (value == oldIndex) _indexes[index] = newIndex;
                    else if (value >= newIndex && value < oldIndex) _indexes[index] = value + 1;
                }
            }

            // Reseat the moved entry. Without Comparer _indexes must stay sorted ascending by
            // source index. With Comparer the comparer-equal tie-break is by source index, so a
            // Move can change the tie-break order even though the value didn't change — reseat to
            // match a fresh Update() rebuild.
            var position = _indexes.IndexOf(newIndex);
            if (position < 0) return;

            _indexes.RemoveAt(position);
            var insertPosition = _comparer is not null
                ? BinarySearchInsertByValue(newIndex)
                : BinarySearchInsertBySource(newIndex);
            _indexes.Insert(insertPosition, newIndex);
        }

        #region Shift Indexes
        private void ShiftIndexesGreaterOrEqual(int threshold, int delta)
        {
            for (var index = 0; index < _indexes?.Count; index++)
            {
                if (_indexes[index] >= threshold)
                    _indexes[index] += delta;
            }
        }

        private void ShiftIndexesGreaterThan(int threshold, int delta)
        {
            for (var index = 0; index < _indexes?.Count; index++)
            {
                if (_indexes[index] > threshold)
                    _indexes[index] += delta;
            }
        }
        #endregion

        // Stable: ties by Comparer tie-break on ascending source index, matching Enumerable.OrderBy.
        private int BinarySearchInsertByValue(int sourceIndex)
        {
            var item = _list[sourceIndex];
            int low = 0, high = _indexes!.Count;

            while (low < high)
            {
                var middle = (low + high) >> 1;
                var middleSourceIndex = _indexes[middle];
                var comparison = _comparer!.Compare(_list[middleSourceIndex], item);
                if (comparison < 0 || (comparison == 0 && middleSourceIndex < sourceIndex)) low = middle + 1;
                else high = middle;
            }

            return low;
        }

        private int BinarySearchInsertBySource(int sourceIndex)
        {
            int low = 0, high = _indexes!.Count;

            while (low < high)
            {
                var middle = (low + high) >> 1;

                if (_indexes[middle] < sourceIndex) low = middle + 1;
                else high = middle;
            }

            return low;
        }

        // Returns view position of sourceIndex, or -1 if not present.
        // Uses only source-index lookups, so it is safe to call after the source mutated _list[sourceIndex].
        private int FindViewPosition(int sourceIndex)
        {
            if (_indexes is null) return -1;

            if (_comparer is null)
            {
                var low = 0;
                var high = _indexes.Count;

                while (low < high)
                {
                    var middle = (low + high) >> 1;
                    var current = _indexes[middle];

                    if (current < sourceIndex) low = middle + 1;
                    else if (current > sourceIndex) high = middle;
                    else return middle;
                }

                return -1;
            }

            for (var index = 0; index < _indexes.Count; index++)
            {
                if (_indexes[index] == sourceIndex)
                    return index;
            }

            return -1;
        }

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
