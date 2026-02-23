using NUnit.Framework;
using System.Collections.Generic;
using Aspid.Collections.Observable.Synchronizer;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class CreateSyncListTests
    {
        private ObservableList<int> _source;
        private IReadOnlyObservableListSync<string> _sync;

        [SetUp]
        public void SetUp()
        {
            _source = new ObservableList<int>();
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
            _source = new ObservableList<int>(new[] { 1, 2, 3 });
            _sync = _source.CreateSync(i => i.ToString());

            Assert.AreEqual(3, _sync.Count);
            Assert.AreEqual("1", _sync[0]);
            Assert.AreEqual("2", _sync[1]);
            Assert.AreEqual("3", _sync[2]);
        }
        #endregion
        
        [Test]
        public void SourceAdd_PropagatesConvertedItem()
        {
            _source.Add(42);

            Assert.AreEqual(1, _sync.Count);
            Assert.AreEqual("42", _sync[0]);
        }

        [Test]
        public void SourceAddRange_PropagatesAllConvertedItems()
        {
            _source.AddRange(1, 2, 3);

            Assert.AreEqual(3, _sync.Count);
            Assert.AreEqual("1", _sync[0]);
            Assert.AreEqual("3", _sync[2]);
        }

        [Test]
        public void SourceInsert_PropagatesAtCorrectIndex()
        {
            _source.AddRange(1, 3);
            _source.Insert(1, 2);

            Assert.AreEqual(3, _sync.Count);
            Assert.AreEqual("2", _sync[1]);
        }

        [Test]
        public void SourceRemove_RemovesFromSync()
        {
            _source.AddRange(10, 20, 30);

            _source.Remove(20);

            Assert.AreEqual(2, _sync.Count);
            Assert.AreEqual("10", _sync[0]);
            Assert.AreEqual("30", _sync[1]);
        }

        [Test]
        public void SourceMove_MovesInSync()
        {
            _source.AddRange(1, 2, 3);

            _source.Move(0, 2);

            Assert.AreEqual("2", _sync[0]);
            Assert.AreEqual("3", _sync[1]);
            Assert.AreEqual("1", _sync[2]);
        }

        [Test]
        public void SourceReplace_ReplacesConvertedItemInSync()
        {
            _source.AddRange(1, 2, 3);

            _source[1] = 99;

            Assert.AreEqual("99", _sync[1]);
        }

        [Test]
        public void SourceClear_ClearsSync()
        {
            _source.AddRange(1, 2, 3);

            _source.Clear();

            Assert.AreEqual(0, _sync.Count);
        }
        
        #region Dispose
        [Test]
        public void Dispose_StopsSynchronization()
        {
            _source.Add(1);
            _sync.Dispose();
            var countAfterDispose = _sync.Count;

            _source.Add(2);

            Assert.AreEqual(countAfterDispose, _sync.Count);
        }

        [Test]
        public void Dispose_IsDisposableTrue_DisposesConvertedItems()
        {
            var dispSource = new ObservableList<int>(new[] { 1, 2 });
            var items = new[] { new DisposableItem(), new DisposableItem() };
            var idx = 0;
            using var sync = dispSource.CreateSync(_ => items[idx++], isDisposable: true);

            sync.Dispose();

            Assert.IsTrue(items[0].IsDisposed);
            Assert.IsTrue(items[1].IsDisposed);
            dispSource.Dispose();
        }

        [Test]
        public void Dispose_IsDisposableFalse_DoesNotDisposeItems()
        {
            var dispSource = new ObservableList<int>(new[] { 1 });
            var item = new DisposableItem();
            using var sync = dispSource.CreateSync(_ => item, isDisposable: false);

            sync.Dispose();

            Assert.IsFalse(item.IsDisposed);
            dispSource.Dispose();
        }
        #endregion
        
        #region Remove
        [Test]
        public void RemoveCallback_InvokedOnRemoveAt()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.AddRange(10, 20, 30);

            _source.RemoveAt(1);

            Assert.AreEqual(1, removed.Count);
            Assert.AreEqual("20", removed[0]);
            syncWithCb.Dispose();
        }

        [Test]
        public void RemoveCallback_InvokedOnReplace()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.AddRange(1, 2, 3);

            _source[1] = 99;

            Assert.AreEqual(1, removed.Count);
            Assert.AreEqual("2", removed[0]);
            syncWithCb.Dispose();
        }

        [Test]
        public void RemoveCallback_InvokedOnClear()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.AddRange(1, 2, 3);

            _source.Clear();

            Assert.AreEqual(3, removed.Count);
            syncWithCb.Dispose();
        }
        #endregion
    }
}