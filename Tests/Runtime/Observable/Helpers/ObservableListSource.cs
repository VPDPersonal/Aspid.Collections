#nullable enable annotations

using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    internal sealed class ObservableListSource<T> : CollectionChangedEvent<T>, IReadOnlyObservableList<T>
    {
        public Action? BeforeSubscribe { get; set; }
        public List<T> Items { get; } = new();
        public int Count => Items.Count;
        public object SyncRoot { get; } = new();
        public T this[int index] => Items[index];

        event NotifyCollectionChangedEventHandler<T>? IObservableCollection<T>.CollectionChanged
        {
            add
            {
                BeforeSubscribe?.Invoke();
                CollectionChanged += value;
            }
            remove => CollectionChanged -= value;
        }

        public void Publish(INotifyCollectionChangedEventArgs<T> args) => Invoke(args);

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
