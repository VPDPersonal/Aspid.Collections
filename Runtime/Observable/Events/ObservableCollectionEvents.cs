using System;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable
{
    internal sealed class ObservableCollectionEvents<T> : IObservableEvents<T>
    {
        public event Action? Reset
        {
            add => AddHandler(ref _reset, value);
            remove => RemoveHandler(ref _reset, value);
        }
        
        public event Action<IReadOnlyList<T>, int>? Added
        {
            add => AddHandler(ref _added, value);
            remove => RemoveHandler(ref _added, value);
        }
        
        public event Action<IReadOnlyList<T>, int>? Removed
        {
            add => AddHandler(ref _removed, value);
            remove => RemoveHandler(ref _removed, value);
        }
        
        public event Action<IReadOnlyList<T>, int, int>? Moved
        {
            add => AddHandler(ref _moved, value);
            remove => RemoveHandler(ref _moved, value);
        }
        
        public event Action<IReadOnlyList<T>, IReadOnlyList<T?>, int>? Replaced
        {
            add => AddHandler(ref _replaced, value);
            remove => RemoveHandler(ref _replaced, value);
        }
        
        private Action[]? _reset;
        private Action<IReadOnlyList<T>, int>[]? _added;
        private Action<IReadOnlyList<T>, int>[]? _removed;
        private Action<IReadOnlyList<T>, int, int>[]? _moved;
        private Action<IReadOnlyList<T>, IReadOnlyList<T?>, int>[]? _replaced;
        
        private readonly IObservableCollection<T> _list;
        
        public ObservableCollectionEvents(
            IObservableCollection<T> list,
            Action<IReadOnlyList<T?>, int>? added = null, 
            Action<IReadOnlyList<T?>, int>? removed = null, 
            Action<IReadOnlyList<T?>, int, int>? moved = null,
            Action<IReadOnlyList<T?>, IReadOnlyList<T?>, int>? replaced = null,
            Action? reset = null)
        {
            _list = list;
            AddHandler(ref _reset, reset);
            AddHandler(ref _added, added);
            AddHandler(ref _moved, moved);
            AddHandler(ref _removed, removed);
            AddHandler(ref _replaced, replaced);
            
            _list.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        var handlers = _added;
                        if (handlers is null) break;

                        var items = e.IsSingleItem ? new[] { e.NewItem! } : e.NewItems!;
                        foreach (var handler in handlers)
                            handler(items, e.NewStartingIndex);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Move:
                    {
                        var handlers = _moved;
                        if (handlers is null) break;

                        var items = e.IsSingleItem ? new[] { e.NewItem! } : e.NewItems!;
                        foreach (var handler in handlers)
                            handler(items, e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    {
                        var handlers = _removed;
                        if (handlers is null) break;

                        var items = e.IsSingleItem ? new[] { e.OldItem }! : e.OldItems!;
                        foreach (var handler in handlers)
                            handler(items, e.OldStartingIndex);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Replace:
                    {
                        var handlers = _replaced;
                        if (handlers is null) break;

                        var oldItems = e.IsSingleItem ? new[] { e.OldItem }! : e.OldItems!;
                        var newItems = e.IsSingleItem ? new[] { e.NewItem }! : e.NewItems!;
                        foreach (var handler in handlers)
                            handler(oldItems, newItems, e.NewStartingIndex);
                    }
                    break;
                
                case NotifyCollectionChangedAction.Reset:
                    {
                        var handlers = _reset;
                        if (handlers is null) break;

                        foreach (var handler in handlers)
                            handler();
                    }
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private static void AddHandler<TDelegate>(ref TDelegate[]? handlers, TDelegate? value)
            where TDelegate : Delegate
        {
            if (value is null) return;

            var invocationList = value.GetInvocationList();
            var count = handlers?.Length ?? 0;
            var copy = new TDelegate[count + invocationList.Length];
            if (handlers is not null) Array.Copy(handlers, copy, count);

            for (var i = 0; i < invocationList.Length; i++)
                copy[count + i] = (TDelegate)invocationList[i];

            handlers = copy;
        }

        private static void RemoveHandler<TDelegate>(ref TDelegate[]? handlers, TDelegate? value)
            where TDelegate : Delegate
        {
            if (value is null || handlers is null) return;

            var invocationList = value.GetInvocationList();
            var remainingCount = handlers.Length - invocationList.Length;
            for (var index = remainingCount; index >= 0; index--)
            {
                if (!MatchesAt(handlers, index, invocationList)) continue;

                if (remainingCount == 0)
                {
                    handlers = null;
                    return;
                }

                var copy = new TDelegate[remainingCount];
                Array.Copy(handlers, 0, copy, 0, index);
                Array.Copy(handlers, index + invocationList.Length, copy, index, remainingCount - index);
                handlers = copy;
                return;
            }
        }

        private static bool MatchesAt(Delegate[] handlers, int index, Delegate[] invocationList)
        {
            for (var i = 0; i < invocationList.Length; i++)
            {
                var existing = handlers[index + i];
                var requested = invocationList[i];
                var matches = existing.GetType() == requested.GetType()
                    ? existing.Equals(requested)
                    : existing.Method == requested.Method && ReferenceEquals(existing.Target, requested.Target);

                if (!matches) return false;
            }

            return true;
        }

        public void Dispose()
        {
            _reset = null;
            _added = null;
            _moved = null;
            _removed = null;
            _replaced = null;
            
            _list.CollectionChanged -= OnCollectionChanged;
        }
    }
}