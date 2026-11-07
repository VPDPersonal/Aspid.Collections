using System;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    /// <summary>
    /// A simple IDisposable that tracks whether Dispose was called and how many times.
    /// Used to verify isDisposable=true behavior in sync collections.
    /// </summary>
    internal sealed class DisposableItem : IDisposable
    {
        public bool IsDisposed => DisposeCount is not 0;
        
        public int DisposeCount { get; private set; }

        public void Dispose() => DisposeCount++;
    }
}