using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class FilteredListTests
    {
        private ObservableList<int> _source;

        [SetUp]
        public void SetUp()
        {
            _source = new ObservableList<int>();
        }

        [TearDown]
        public void TearDown()
        {
            _source.Dispose();
        }
        
        #region Constructors
        [Test]
        public void Ctor_ListOnly_CountMatchesSourceCount()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source);

            Assert.AreEqual(3, fl.Count);
        }

        [Test]
        public void Ctor_ListOnly_IndexerReturnsSourceItems()
        {
            _source.AddRange(10, 20, 30);
            using var fl = new FilteredList<int>(_source);

            Assert.AreEqual(10, fl[0]);
            Assert.AreEqual(20, fl[1]);
            Assert.AreEqual(30, fl[2]);
        }

        [Test]
        public void Ctor_WithFilter_CountOnlyIncludesMatchingItems()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);

            Assert.AreEqual(2, fl.Count);
        }

        [Test]
        public void Ctor_WithFilter_IndexerReturnsOnlyMatchingItems()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);

            Assert.AreEqual(2, fl[0]);
            Assert.AreEqual(4, fl[1]);
        }

        [Test]
        public void Ctor_WithComparer_ItemsReturnedInSortedOrder()
        {
            _source.AddRange(3, 1, 2);
            var descending = Comparer<int>.Create((a, b) => b.CompareTo(a));
            using var fl = new FilteredList<int>(_source, descending);

            Assert.AreEqual(3, fl[0]);
            Assert.AreEqual(2, fl[1]);
            Assert.AreEqual(1, fl[2]);
        }

        [Test]
        public void Ctor_WithFilterAndComparer_FiltersAndSorts()
        {
            _source.AddRange(5, 1, 4, 2, 3);
            var ascending = Comparer<int>.Default;
            using var fl = new FilteredList<int>(_source,  i => i > 2, ascending);

            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(3, fl[0]);
            Assert.AreEqual(4, fl[1]);
            Assert.AreEqual(5, fl[2]);
        }
        #endregion
        
        #region Filter
        [Test]
        public void Filter_SetNewFilter_UpdatesCountAndItems()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source);

            fl.Filter = i => i > 3;

            Assert.AreEqual(2, fl.Count);
            Assert.AreEqual(4, fl[0]);
            Assert.AreEqual(5, fl[1]);
        }

        [Test]
        public void Filter_SetNull_AllItemsVisible()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i > 3);

            fl.Filter = null;

            Assert.AreEqual(5, fl.Count);
        }

        [Test]
        public void Filter_Set_RaisesCollectionChanged()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source);
            var eventCount = 0;
            fl.CollectionChanged += () => eventCount++;

            fl.Filter = i => i > 1;

            Assert.AreEqual(1, eventCount);
        }
        #endregion
        
        #region Comparer
        [Test]
        public void Comparer_SetNewComparer_ResortsList()
        {
            _source.AddRange(1, 3, 2);
            using var fl = new FilteredList<int>(_source);

            fl.Comparer = Comparer<int>.Default;

            Assert.AreEqual(1, fl[0]);
            Assert.AreEqual(2, fl[1]);
            Assert.AreEqual(3, fl[2]);
        }

        [Test]
        public void Comparer_SetNull_RemovesSorting()
        {
            _source.AddRange(3, 1, 2);
            using var fl = new FilteredList<int>(_source, Comparer<int>.Default);

            fl.Comparer = null;

            // No sorting — source order
            Assert.AreEqual(3, fl[0]);
            Assert.AreEqual(1, fl[1]);
            Assert.AreEqual(2, fl[2]);
        }

        [Test]
        public void Comparer_Set_RaisesCollectionChanged()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source);
            var eventCount = 0;
            fl.CollectionChanged += () => eventCount++;

            fl.Comparer = Comparer<int>.Default;

            Assert.AreEqual(1, eventCount);
        }
        #endregion
        
        [Test]
        public void Update_ManualCall_RaisesCollectionChanged()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source, i => i > 1);
            var eventCount = 0;
            fl.CollectionChanged += () => eventCount++;

            fl.Update();

            Assert.AreEqual(1, eventCount);
        }

        #region AutoUpdate
        [Test]
        public void AutoUpdate_ObservableListAdd_UpdatesFilteredList()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.Add(5);

            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(5, fl[2]);
        }

        [Test]
        public void AutoUpdate_ObservableListAdd_FilteredItemNotVisible()
        {
            _source.AddRange(2, 3);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.Add(1);

            Assert.AreEqual(2, fl.Count);
        }

        [Test]
        public void AutoUpdate_ObservableListRemove_UpdatesFilteredList()
        {
            _source.AddRange(2, 3, 4);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.Remove(3);

            Assert.AreEqual(2, fl.Count);
        }

        [Test]
        public void AutoUpdate_ObservableListClear_ResetsFilteredList()
        {
            _source.AddRange(2, 3, 4);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.Clear();

            Assert.AreEqual(0, fl.Count);
        }
        #endregion
        
        #region Chaining
        [Test]
        public void Chaining_FilteredListSource_UpdatesProperly()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl1 = new FilteredList<int>(_source, i => i > 2);    // [3,4,5]
            using var fl2 = new FilteredList<int>(fl1, i => i % 2 == 0);   // [4]

            Assert.AreEqual(1, fl2.Count);
            Assert.AreEqual(4, fl2[0]);
        }

        [Test]
        public void Chaining_SourceUpdate_PropagatesThroughChain()
        {
            _source.AddRange(1, 2, 3, 4);
            using var fl1 = new FilteredList<int>(_source, i => i > 2);    // [3,4]
            using var fl2 = new FilteredList<int>(fl1, i => i % 2 == 0);   // [4]

            _source.Add(6);

            // fl1 → [3,4,6], fl2 → [4,6]
            Assert.AreEqual(2, fl2.Count);
        }
        #endregion
        
        #region GetEnumerator
        [Test]
        public void GetEnumerator_Generic_EnumeratesFilteredItems()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);
            var result = new List<int>();

            foreach (var item in fl)
                result.Add(item);

            Assert.AreEqual(new[] { 2, 4 }, result.ToArray());
        }

        [Test]
        public void GetEnumerator_NonGeneric_EnumeratesFilteredItems()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);
            var result = new List<int>();

            // This was a bug: previously returned unfiltered _list.GetEnumerator()
            foreach (int item in (IEnumerable)fl)
                result.Add(item);

            Assert.AreEqual(new[] { 2, 4 }, result.ToArray());
        }

        [Test]
        public void GetEnumerator_NoFilterNoComparer_EnumeratesAllSourceItems()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source);
            var result = new List<int>();

            foreach (var item in fl)
                result.Add(item);

            Assert.AreEqual(new[] { 1, 2, 3 }, result.ToArray());
        }
        #endregion
        
        #region Count
        [Test]
        public void Count_AfterFilter_MatchesEnumerationCount()
        {
            _source.AddRange(1, 2, 3, 4, 5, 6);
            using var fl = new FilteredList<int>(_source, i => i % 2 != 0);

            var enumerationCount = 0;
            foreach (var _ in fl)
                enumerationCount++;

            Assert.AreEqual(fl.Count, enumerationCount);
        }

        [Test]
        public void Count_AfterFilterAndComparer_MatchesEnumerationCount()
        {
            _source.AddRange(5, 1, 4, 2, 3);
            using var fl = new FilteredList<int>(_source, i => i > 2, Comparer<int>.Default);

            var enumerationCount = 0;
            foreach (var _ in fl)
                enumerationCount++;

            Assert.AreEqual(fl.Count, enumerationCount);
        }
        #endregion
        
        [Test]
        public void EmptySource_CountIsZero()
        {
            using var fl = new FilteredList<int>(_source, i => i > 0);

            Assert.AreEqual(0, fl.Count);
        }

        [Test]
        public void AllItemsFilteredOut_CountIsZero()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source, i => i > 100);

            Assert.AreEqual(0, fl.Count);
        }

        [Test]
        public void NullFilterNullComparer_Passthrough()
        {
            _source.AddRange(3, 1, 2);
            using var fl = new FilteredList<int>(_source);

            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(3, fl[0]);
        }
        
        #region Dispose
        [Test]
        public void Dispose_ObservableListSource_UnsubscribesFromEvents()
        {
            _source.AddRange(1, 2, 3);
            var fl = new FilteredList<int>(_source, i => i > 1);
            var countBefore = fl.Count;

            fl.Dispose();
            _source.Add(5); // should NOT update fl

            Assert.AreEqual(countBefore, fl.Count);
        }

        [Test]
        public void Dispose_ClearsCollectionChangedEvent()
        {
            using var fl = new FilteredList<int>(_source);
            var invoked = false;
            fl.CollectionChanged += () => invoked = true;

            fl.Dispose();
            // After dispose, CollectionChanged is cleared — no more notifications
            fl.Update();

            Assert.IsFalse(invoked);
        }

        [Test]
        public void Dispose_ChainedFilteredList_UnsubscribesFromSource()
        {
            _source.AddRange(1, 2, 3, 4);
            using var fl1 = new FilteredList<int>(_source, i => i > 2);
            var fl2 = new FilteredList<int>(fl1, i => i % 2 == 0);
            var countBefore = fl2.Count;

            fl2.Dispose();
            _source.Add(6); // triggers fl1 update → should NOT update fl2

            Assert.AreEqual(countBefore, fl2.Count);
        }
        #endregion
    }
}