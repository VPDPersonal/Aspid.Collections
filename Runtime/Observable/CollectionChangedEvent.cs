using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable
{
    public abstract class CollectionChangedEvent<T> : IDisposable
    {
        // We store handlers in a List instead of combining them via Delegate.Combine because
        // subscribers with different but variance-compatible T parameters cannot be combined by
        // the runtime — each handler must be kept and invoked independently.
        private List<Subscription>? _handlers;
        private long _nextSubscriptionId;

        // Bumped while Invoke iterates _handlers; add/remove fork the list (copy-on-write) only
        // when this is non-zero, so the in-flight foreach iterates an undisturbed list.
        private int _invokeDepth;

        // Handlers unsubscribed while any Invoke is on the stack. Populated lazily on the first
        // removal during an invoke, cleared when _invokeDepth returns to zero. Lookup is O(1).
        private HashSet<long>? _removedDuringInvoke;

        public event NotifyCollectionChangedEventHandler<T>? CollectionChanged
        {
            add
            {
                var handler = value ?? throw ThrowValueNullReferenceException();
                var subscription = new Subscription(_nextSubscriptionId++, handler);

                if (_invokeDepth > 0)
                {
                    _handlers = _handlers is null
                        ? new List<Subscription> { subscription }
                        : new List<Subscription>(_handlers) { subscription };
                }
                else
                {
                    _handlers ??= new List<Subscription>();
                    _handlers.Add(subscription);
                }
            }
            remove
            {
                var handler = value ?? throw ThrowValueNullReferenceException();
                if (_handlers is null) return;

                var index = LastIndexOf(_handlers, handler);
                if (index < 0) return;

                if (_invokeDepth > 0)
                {
                    var forked = new List<Subscription>(_handlers);
                    var removed = forked[index];
                    forked.RemoveAt(index);

                    (_removedDuringInvoke ??= new HashSet<long>()).Add(removed.Id);
                    _handlers = forked;
                }
                else
                {
                    _handlers.RemoveAt(index);
                }
            }
        }

        private static int LastIndexOf(List<Subscription> handlers, NotifyCollectionChangedEventHandler<T> value)
        {
            var valueType = value.GetType();
            Delegate[]? invocationList = null;

            for (var i = handlers.Count - 1; i >= 0; i--)
            {
                var handler = handlers[i].Handler;
                var matches = handler.GetType() == valueType
                    ? handler.Equals(value)
                    : MatchesInvocationList(handler.GetInvocationList(), invocationList ??= value.GetInvocationList());

                if (matches) return i;
            }

            return -1;
        }

        private static bool MatchesInvocationList(Delegate[] registered, Delegate[] requested)
        {
            if (registered.Length != requested.Length) return false;

            for (var i = 0; i < registered.Length; i++)
            {
                if (registered[i].Method != requested[i].Method || !ReferenceEquals(registered[i].Target, requested[i].Target))
                    return false;
            }

            return true;
        }

        protected void Invoke(INotifyCollectionChangedEventArgs<T> e)
        {
            var handlers = _handlers;
            if (handlers is null) return;

            var count = handlers.Count;
            if (count is 0) return;

            if (count is 1)
            {
                // Single subscriber: nothing to iterate after the call, so the handler is free
                // to mutate _handlers in place (no fork needed, no depth tracking).
                handlers[0].Handler.Invoke(e);
                return;
            }

            _invokeDepth++;

            try
            {
                foreach (var subscription in handlers)
                {
                    // Skip handlers that were unsubscribed at any point during this invoke chain.
                    if (_removedDuringInvoke is not null && _removedDuringInvoke.Contains(subscription.Id))
                        continue;

                    subscription.Handler.Invoke(e);
                }
            }
            finally
            {
                if (--_invokeDepth == 0) _removedDuringInvoke = null;
            }
        }

        private static NullReferenceException ThrowValueNullReferenceException() =>
            throw new NullReferenceException("value");

        public virtual void Dispose() =>
            _handlers = null;

        private readonly struct Subscription
        {
            public long Id { get; }
            public NotifyCollectionChangedEventHandler<T> Handler { get; }

            public Subscription(long id, NotifyCollectionChangedEventHandler<T> handler)
            {
                Id = id;
                Handler = handler;
            }
        }
    }
}
