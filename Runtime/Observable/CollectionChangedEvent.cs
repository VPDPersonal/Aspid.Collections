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
        private List<NotifyCollectionChangedEventHandler<T>>? _handlers;

        // Bumped while Invoke iterates _handlers; add/remove fork the list (copy-on-write) only
        // when this is non-zero, so the in-flight foreach iterates an undisturbed list.
        private int _invokeDepth;

        // Handlers unsubscribed while any Invoke is on the stack. Populated lazily on the first
        // removal during an invoke, cleared when _invokeDepth returns to zero. Lookup is O(1).
        private HashSet<NotifyCollectionChangedEventHandler<T>>? _removedDuringInvoke;

        public event NotifyCollectionChangedEventHandler<T>? CollectionChanged
        {
            add
            {
                var v = value ?? throw ThrowValueNullReferenceException();

                if (_invokeDepth > 0)
                {
                    _handlers = _handlers is null
                        ? new List<NotifyCollectionChangedEventHandler<T>> { v }
                        : new List<NotifyCollectionChangedEventHandler<T>>(_handlers) { v };
                }
                else
                {
                    _handlers ??= new List<NotifyCollectionChangedEventHandler<T>>();
                    _handlers.Add(v);
                }
            }
            remove
            {
                var v = value ?? throw ThrowValueNullReferenceException();
                if (_handlers is null) return;

                var index = IndexOf(_handlers, v);
                if (index < 0) return;

                if (_invokeDepth > 0)
                {
                    var forked = new List<NotifyCollectionChangedEventHandler<T>>(_handlers);
                    var removed = forked[index];
                    forked.RemoveAt(index);

                    // Track the stored instance: the caller's delegate may differ in runtime type.
                    (_removedDuringInvoke ??= new HashSet<NotifyCollectionChangedEventHandler<T>>()).Add(removed);
                    _handlers = forked;
                }
                else
                {
                    _handlers.RemoveAt(index);
                }
            }
        }

        // Delegate.Equals also compares runtime types, so a handler subscribed as Handler<Cat> never
        // equals the same method group unsubscribed through a covariant view as Handler<Animal>.
        // Match by method and target instead; search from the end, like Delegate.Remove.
        private static int IndexOf(List<NotifyCollectionChangedEventHandler<T>> handlers, NotifyCollectionChangedEventHandler<T> value)
        {
            for (var i = handlers.Count - 1; i >= 0; i--)
            {
                var handler = handlers[i];
                if (handler.Method == value.Method && ReferenceEquals(handler.Target, value.Target))
                    return i;
            }

            return -1;
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
                handlers[0].Invoke(e);
                return;
            }

            _invokeDepth++;

            try
            {
                foreach (var handler in handlers)
                {
                    // Skip handlers that were unsubscribed at any point during this invoke chain.
                    if (_removedDuringInvoke is not null && _removedDuringInvoke.Contains(handler))
                        continue;

                    handler.Invoke(e);
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
    }
}
