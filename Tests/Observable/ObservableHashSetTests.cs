using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class ObservableHashSetTests
    {
        private EventCapture<int> _events;
        private ObservableHashSet<int> _set;

        [SetUp]
        public void SetUp()
        {
            _set = new ObservableHashSet<int>();
            _events = new EventCapture<int>(_set);
        }

        [TearDown]
        public void TearDown()
        {
            _events.Dispose();
            _set.Dispose();
        }

        #region Constructors
        [Test]
        public void Ctor_Default_CreatesEmptySet()
        {
            Assert.AreEqual(0, _set.Count);
        }

        [Test]
        public void Ctor_Capacity_CreatesEmptySet()
        {
            var set = new ObservableHashSet<int>(16);
            Assert.AreEqual(0, set.Count);
        }

        [Test]
        public void Ctor_Collection_CopiesItems()
        {
            var set = new ObservableHashSet<int>(new[] { 1, 2, 3 });

            Assert.AreEqual(3, set.Count);
            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsTrue(set.Contains(3));
        }

        [Test]
        public void Ctor_Comparer_UsesProvidedComparer()
        {
            var comparer = EqualityComparer<int>.Default;
            var set = new ObservableHashSet<int>(comparer);

            Assert.AreEqual(comparer, set.Comparer);
        }

        [Test]
        public void Ctor_CollectionAndComparer_CopiesItemsWithComparer()
        {
            var set = new ObservableHashSet<int>(new[] { 5, 6 }, EqualityComparer<int>.Default);
            Assert.AreEqual(2, set.Count);
        }
        #endregion

        #region Add
        [Test]
        public void Add_NewItem_ReturnsTrueAndIncreasesCount()
        {
            var result = _set.Add(42);

            Assert.IsTrue(result);
            Assert.AreEqual(1, _set.Count);
        }

        [Test]
        public void Add_NewItem_RaisesAddEventWithIndexMinusOne()
        {
            _set.Add(42);

            Assert.AreEqual(1, _events.Count);
            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(42, e.NewItem);
            Assert.AreEqual(-1, e.NewStartingIndex);
        }

        [Test]
        public void Add_DuplicateItem_ReturnsFalseAndRaisesNoEvent()
        {
            _set.Add(10);
            _events.Clear();

            var result = _set.Add(10);

            Assert.IsFalse(result);
            Assert.AreEqual(0, _events.Count);
            Assert.AreEqual(1, _set.Count);
        }
        #endregion

        #region Remove
        [Test]
        public void Remove_ExistingItem_ReturnsTrueAndDecreasesCount()
        {
            _set.Add(5);

            var result = _set.Remove(5);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _set.Count);
        }

        [Test]
        public void Remove_NonExistingItem_ReturnsFalseAndRaisesNoEvent()
        {
            _events.Clear();

            var result = _set.Remove(999);

            Assert.IsFalse(result);
            Assert.AreEqual(0, _events.Count);
        }

        [Test]
        public void Remove_ExistingItem_RaisesRemoveEventWithIndexMinusOne()
        {
            _set.Add(5);
            _events.Clear();

            _set.Remove(5);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(5, e.OldItem);
            Assert.AreEqual(-1, e.OldStartingIndex);
        }
        #endregion

        #region Constains
        [Test]
        public void Contains_ExistingItem_ReturnsTrue()
        {
            _set.Add(7);
            Assert.IsTrue(_set.Contains(7));
        }

        [Test]
        public void Contains_NonExistingItem_ReturnsFalse()
        {
            Assert.IsFalse(_set.Contains(7));
        }
        #endregion
        
        [Test]
        public void IsProperSubsetOf_ProperSubset_ReturnsTrue()
        {
            _set.Add(1);
            _set.Add(2);
            Assert.IsTrue(_set.IsProperSubsetOf(new[] { 1, 2, 3 }));
        }

        [Test]
        public void IsProperSubsetOf_EqualSet_ReturnsFalse()
        {
            _set.Add(1);
            _set.Add(2);
            Assert.IsFalse(_set.IsProperSubsetOf(new[] { 1, 2 }));
        }

        [Test]
        public void IsProperSupersetOf_ProperSuperset_ReturnsTrue()
        {
            _set.Add(1);
            _set.Add(2);
            _set.Add(3);
            Assert.IsTrue(_set.IsProperSupersetOf(new[] { 1, 2 }));
        }

        [Test]
        public void IsProperSupersetOf_EqualSet_ReturnsFalse()
        {
            _set.Add(1);
            _set.Add(2);
            Assert.IsFalse(_set.IsProperSupersetOf(new[] { 1, 2 }));
        }

        [Test]
        public void IsSubsetOf_Subset_ReturnsTrue()
        {
            _set.Add(1);
            _set.Add(2);
            Assert.IsTrue(_set.IsSubsetOf(new[] { 1, 2, 3 }));
        }

        [Test]
        public void IsSubsetOf_EqualSet_ReturnsTrue()
        {
            _set.Add(1);
            Assert.IsTrue(_set.IsSubsetOf(new[] { 1 }));
        }

        [Test]
        public void IsSupersetOf_Superset_ReturnsTrue()
        {
            _set.Add(1);
            _set.Add(2);
            _set.Add(3);
            Assert.IsTrue(_set.IsSupersetOf(new[] { 1, 2 }));
        }

        [Test]
        public void IsSupersetOf_NotSuperset_ReturnsFalse()
        {
            _set.Add(1);
            Assert.IsFalse(_set.IsSupersetOf(new[] { 1, 2 }));
        }

        [Test]
        public void Overlaps_OverlappingSets_ReturnsTrue()
        {
            _set.Add(1);
            _set.Add(2);
            Assert.IsTrue(_set.Overlaps(new[] { 2, 3 }));
        }

        [Test]
        public void Overlaps_DisjointSets_ReturnsFalse()
        {
            _set.Add(1);
            Assert.IsFalse(_set.Overlaps(new[] { 2, 3 }));
        }

        [Test]
        public void SetEquals_EqualSets_ReturnsTrue()
        {
            _set.Add(1);
            _set.Add(2);
            Assert.IsTrue(_set.SetEquals(new[] { 2, 1 }));
        }

        [Test]
        public void SetEquals_DifferentSets_ReturnsFalse()
        {
            _set.Add(1);
            Assert.IsFalse(_set.SetEquals(new[] { 1, 2 }));
        }
        
        [Test]
        public void GetEnumerator_EnumeratesAllItems()
        {
            _set.Add(10);
            _set.Add(20);
            var result = new List<int>();

            foreach (var item in _set)
                result.Add(item);

            Assert.That(result, Is.EquivalentTo(new[] { 10, 20 }));
        }

        #region Clear And Dispose
        [Test]
        public void Clear_RemovesAllItemsAndRaisesResetEvent()
        {
            _set.Add(1);
            _set.Add(2);
            _events.Clear();

            _set.Clear();

            Assert.AreEqual(0, _set.Count);
            Assert.AreEqual(NotifyCollectionChangedAction.Reset, _events.Last.Action);
        }

        [Test]
        public void Dispose_ClearsSetAndRemovesHandlers()
        {
            _set.Add(1);
            _events.Dispose();

            _set.Dispose();

            Assert.AreEqual(0, _set.Count);
        }
        #endregion
    }
}
