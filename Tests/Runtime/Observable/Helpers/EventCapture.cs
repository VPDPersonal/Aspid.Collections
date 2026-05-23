using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    /// <summary>
    /// Subscribes to CollectionChanged on a CollectionChangedEvent source and records
    /// all received INotifyCollectionChangedEventArgs for assertion in tests.
    /// </summary>
    internal sealed class EventCapture<T> : IDisposable
    {
        private readonly CollectionChangedEvent<T> _source;
        private readonly NotifyCollectionChangedEventHandler<T> _handler;

        public int Count => Events.Count;
        
        public INotifyCollectionChangedEventArgs<T> Last => Events[^1];
        
        public List<INotifyCollectionChangedEventArgs<T>> Events { get; } = new();

        public EventCapture(CollectionChangedEvent<T> source)
        {
            _source = source;
            _handler = e => Events.Add(e);
            source.CollectionChanged += _handler;
        }

        public void Clear() => 
            Events.Clear();

        public void Dispose() => 
            _source.CollectionChanged -= _handler;
    }
}