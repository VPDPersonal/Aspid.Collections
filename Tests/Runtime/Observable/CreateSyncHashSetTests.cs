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
    }
}