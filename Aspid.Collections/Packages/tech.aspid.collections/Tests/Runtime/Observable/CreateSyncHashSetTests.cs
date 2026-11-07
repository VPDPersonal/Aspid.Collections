using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using Aspid.Collections.Observable.Synchronizer;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class CreateSyncHashSetTests
    {
        private ObservableHashSet<int> _source;
        private IReadOnlyObservableCollectionSync<string> _sync;

        [SetUp]
        public void SetUp()
        {
            _source = new ObservableHashSet<int>();
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
            var source = new ObservableHashSet<int>(new[] { 1, 2, 3 });
            _sync = source.CreateSync(i => i.ToString());

            Assert.AreEqual(3, _sync.Count);
            source.Dispose();
        }
        #endregion
        
        [Test]
        public void SourceAdd_PropagatesConvertedItem()
        {
            _source.Add(42);

            Assert.AreEqual(1, _sync.Count);
            Assert.IsTrue(_sync.Contains("42"));
        }

        [Test]
        public void SourceRemove_RemovesConvertedItemFromSync()
        {
            _source.Add(5);

            _source.Remove(5);

            Assert.AreEqual(0, _sync.Count);
            Assert.IsFalse(_sync.Contains("5"));
        }

        [Test]
        public void SourceClear_ClearsSync()
        {
            _source.Add(1);
            _source.Add(2);

            _source.Clear();

            Assert.AreEqual(0, _sync.Count);
        }

        #region Dispose
        [Test]
        public void Dispose_StopsSynchronization()
        {
            _source.Add(1);
            _sync.Dispose();
            var countBefore = _sync.Count;

            _source.Add(2);

            Assert.AreEqual(countBefore, _sync.Count);
        }

        [Test]
        public void Dispose_IsDisposableTrue_DisposesConvertedItems()
        {
            var source = new ObservableHashSet<int>();
            var item = new DisposableItem();
            var sync = source.CreateSync(_ => item, isDisposable: true);
            source.Add(1);

            sync.Dispose();

            Assert.IsTrue(item.IsDisposed);
            source.Dispose();
        }
        #endregion

        #region Remove
        [Test]
        public void RemoveCallback_InvokedOnRemove()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.Add(7);

            _source.Remove(7);

            Assert.AreEqual(1, removed.Count);
            Assert.AreEqual("7", removed[0]);
            syncWithCb.Dispose();
        }

        [Test]
        public void RemoveCallback_InvokedOnClear()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(i => i.ToString(), removed.Add);
            _source.Add(1);
            _source.Add(2);

            _source.Clear();

            Assert.AreEqual(2, removed.Count);
            syncWithCb.Dispose();
        }
        #endregion

        #region Custom Comparer
        [Test]
        public void CustomComparer_SourceRemoveByEquivalentItem_RemovesProjection()
        {
            var source = new ObservableHashSet<string>(System.StringComparer.OrdinalIgnoreCase) { "A" };
            var removed = new List<string>();
            using var sync = source.CreateSync(s => s + "!", remove: removed.Add);

            source.Remove("a");

            Assert.AreEqual(0, sync.Count);
            CollectionAssert.AreEqual(new[] { "A!" }, removed);
            source.Dispose();
        }
        #endregion

        #region Dispose
        [Test]
        public void DisposeTwice_InvokesRemoveOncePerItem()
        {
            var source = new ObservableHashSet<int> { 1, 2 };
            var removed = new List<string>();
            var sync = source.CreateSync(i => i.ToString(), remove: removed.Add);

            sync.Dispose();
            sync.Dispose();

            CollectionAssert.AreEquivalent(new[] { "1", "2" }, removed);
            source.Dispose();
        }

        [Test]
        public void DisposeTwice_IsDisposable_DisposesItemsOnce()
        {
            var source = new ObservableHashSet<int> { 1 };
            DisposableItem created = null;
            var sync = source.CreateSync(_ => created = new DisposableItem(), isDisposable: true);

            sync.Dispose();
            sync.Dispose();

            Assert.AreEqual(1, created.DisposeCount);
            source.Dispose();
        }

        [Test]
        public void Clear_ThenDispose_ReleasesItemsOnce()
        {
            var source = new ObservableHashSet<int> { 1, 2 };
            var removed = new List<string>();
            var sync = source.CreateSync(i => i.ToString(), remove: removed.Add);

            source.Clear();
            sync.Dispose();

            Assert.AreEqual(2, removed.Count);
            source.Dispose();
        }
        #endregion

        [Test]
        public void CreateSync_ConverterCollision_Throws()
        {
            _source.Add(1);
            _source.Add(2);

            Assert.Throws<System.InvalidOperationException>(() => _source.CreateSync(_ => "duplicate"));
        }

        [Test]
        public void SourceAdd_ConverterCollision_RemovingRejectedItemKeepsOriginalProjection()
        {
            using var sync = _source.CreateSync(_ => "same");
            using var capture = new EventCapture<string>(sync);
            _source.Add(1);
            capture.Clear();

            Assert.Throws<System.InvalidOperationException>(() => _source.Add(2));
            Assert.That(_source.Remove(2), Is.True);
            CollectionAssert.AreEqual(new[] { "same" }, sync);
            Assert.That(capture.Count, Is.Zero);

            Assert.That(_source.Remove(1), Is.True);
            _source.Add(3);
            CollectionAssert.AreEqual(new[] { "same" }, sync);
        }

        [Test]
        public void SourceRemove_DisposesOnlyTheRemovedProjection()
        {
            _source.Add(1);
            _source.Add(2);
            var items = new[] { new DisposableItem(), new DisposableItem() };
            using var sync = _source.CreateSync(value => items[value - 1], isDisposable: true);

            _source.Remove(2);

            CollectionAssert.AreEqual(new[] { items[0] }, sync);
            Assert.That(items[0].DisposeCount, Is.Zero);
            Assert.That(items[1].DisposeCount, Is.EqualTo(1));
        }

    }
}
