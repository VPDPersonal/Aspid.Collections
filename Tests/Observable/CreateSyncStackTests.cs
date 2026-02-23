using NUnit.Framework;
using System.Collections.Generic;
using Aspid.Collections.Observable.Synchronizer;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class CreateSyncStackTests
    {
        private ObservableStack<int> _source;
        private IReadOnlyObservableCollectionSync<string> _sync;

        [SetUp]
        public void SetUp()
        {
            _source = new ObservableStack<int>();
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
        public void CreateSync_NonEmptySource_CopiesAllConvertedItems()
        {
            _sync.Dispose();
            var source = new ObservableStack<int>(new[] { 1, 2, 3 });
            _sync = source.CreateSync(i => i.ToString());

            Assert.AreEqual(3, _sync.Count);
            source.Dispose();
        }
        #endregion
        
        [Test]
        public void SourcePush_PropagatesConvertedItem()
        {
            _source.Push(10);
            Assert.AreEqual(1, _sync.Count);
        }

        [Test]
        public void SourcePushRange_PropagatesAllConvertedItems()
        {
            _source.PushRange(new[] { 1, 2, 3 });
            Assert.AreEqual(3, _sync.Count);
        }

        [Test]
        public void SourcePop_PopsFromSync()
        {
            _source.PushRange(new[] { 10, 20, 30 });

            _source.Pop();

            Assert.AreEqual(2, _sync.Count);
        }

        [Test]
        public void SourcePopRange_PopsMultipleFromSync()
        {
            _source.PushRange(new[] { 1, 2, 3 });
            _source.PopRange(dest: new int[2]);

            Assert.AreEqual(1, _sync.Count);
        }

        [Test]
        public void SourceClear_ClearsSync()
        {
            _source.PushRange(new[] { 1, 2 });
            _source.Clear();

            Assert.AreEqual(0, _sync.Count);
        }

        #region Dispose
        [Test]
        public void Dispose_StopsSynchronization()
        {
            _source.Push(1);
            _sync.Dispose();
            var countBefore = _sync.Count;

            _source.Push(2);

            Assert.AreEqual(countBefore, _sync.Count);
        }

        [Test]
        public void Dispose_IsDisposableTrue_DisposesConvertedItems()
        {
            var source = new ObservableStack<int>();
            var item = new DisposableItem();
            var sync = source.CreateSync(_ => item, isDisposable: true);
            source.Push(1);

            sync.Dispose();

            Assert.IsTrue(item.IsDisposed);
            source.Dispose();
        }
        #endregion
        
        #region Remove
        [Test]
        public void RemoveCallback_InvokedOnPop()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.Push(55);

            _source.Pop();

            Assert.AreEqual(1, removed.Count);
            Assert.AreEqual("55", removed[0]);
            syncWithCb.Dispose();
        }

        [Test]
        public void RemoveCallback_InvokedOnClear()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.PushRange(new[] { 1, 2 });

            _source.Clear();

            Assert.AreEqual(2, removed.Count);
            syncWithCb.Dispose();
        }
        #endregion
    }
}