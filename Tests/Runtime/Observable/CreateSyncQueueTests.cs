using NUnit.Framework;
using System.Collections.Generic;
using Aspid.Collections.Observable.Synchronizer;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class CreateSyncQueueTests
    {
        private ObservableQueue<int> _source;
        private IReadOnlyObservableCollectionSync<string> _sync;

        [SetUp]
        public void SetUp()
        {
            _source = new ObservableQueue<int>();
            _sync = _source.CreateSync(i => i.ToString());
        }

        [TearDown]
        public void TearDown()
        {
            _sync.Dispose();
            _source.Dispose();
        }
        
        #region Initial State
        [Test]
        public void CreateSync_EmptySource_CreatesEmptySync()
        {
            Assert.AreEqual(0, _sync.Count);
        }

        [Test]
        public void CreateSync_NonEmptySource_CopiesAllConvertedItemsInFIFOOrder()
        {
            _sync.Dispose();
            var source = new ObservableQueue<int>(new[] { 1, 2, 3 });
            _sync = source.CreateSync(i => i.ToString());

            Assert.AreEqual(3, _sync.Count);
            source.Dispose();
        }
        #endregion
        
        [Test]
        public void SourceEnqueue_PropagatesConvertedItem()
        {
            _source.Enqueue(10);

            Assert.AreEqual(1, _sync.Count);
        }

        [Test]
        public void SourceEnqueueRange_PropagatesAllConvertedItems()
        {
            _source.EnqueueRange(new[] { 1, 2, 3 });

            Assert.AreEqual(3, _sync.Count);
        }

        [Test]
        public void SourceDequeue_DequeuesFromSync()
        {
            _source.EnqueueRange(new[] { 10, 20, 30 });

            _source.Dequeue();

            Assert.AreEqual(2, _sync.Count);
        }

        [Test]
        public void SourceDequeueRange_DequeuesMultipleFromSync()
        {
            _source.EnqueueRange(new[] { 1, 2, 3 });

            _source.DequeueRange(new int[2]);

            Assert.AreEqual(1, _sync.Count);
        }

        [Test]
        public void SourceClear_ClearsSync()
        {
            _source.EnqueueRange(new[] { 1, 2 });

            _source.Clear();

            Assert.AreEqual(0, _sync.Count);
        }
        
        #region Dispose
        [Test]
        public void Dispose_StopsSynchronization()
        {
            _source.Enqueue(1);
            _sync.Dispose();
            var countBefore = _sync.Count;

            _source.Enqueue(2);

            Assert.AreEqual(countBefore, _sync.Count);
        }

        [Test]
        public void Dispose_IsDisposableTrue_DisposesConvertedItems()
        {
            var source = new ObservableQueue<int>();
            var item = new DisposableItem();
            var sync = source.CreateSync(_ => item, isDisposable: true);
            source.Enqueue(1);

            sync.Dispose();

            Assert.IsTrue(item.IsDisposed);
            source.Dispose();
        }
        #endregion
        
        #region Remove
        [Test]
        public void RemoveCallback_InvokedOnDequeue()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.Enqueue(55);

            _source.Dequeue();

            Assert.AreEqual(1, removed.Count);
            Assert.AreEqual("55", removed[0]);
            syncWithCb.Dispose();
        }

        [Test]
        public void RemoveCallback_InvokedOnClear()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.EnqueueRange(new[] { 1, 2 });

            _source.Clear();

            Assert.AreEqual(2, removed.Count);
            syncWithCb.Dispose();
        }
        #endregion
    }
}