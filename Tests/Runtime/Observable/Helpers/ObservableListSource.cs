using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    internal sealed class ObservableListSource<T> : CollectionChangedEvent<T>, IReadOnlyObservableList<T>
    {
        public List<T> Items { get; } = new();
        public int Count => Items.Count;
        public object SyncRoot { get; } = new();
        public T this[int index] => Items[index];

        public void Publish(INotifyCollectionChangedEventArgs<T> args) => Invoke(args);

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
