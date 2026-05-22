using System;
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

        #region AutoUpdate Incremental
        [Test]
        public void AutoUpdate_AddWithComparer_InsertsAtSortedPosition()
        {
            _source.AddRange(1, 3, 5);
            using var fl = new FilteredList<int>(_source, Comparer<int>.Default);

            _source.Add(4);

            Assert.AreEqual(4, fl.Count);
            Assert.AreEqual(1, fl[0]);
            Assert.AreEqual(3, fl[1]);
            Assert.AreEqual(4, fl[2]);
            Assert.AreEqual(5, fl[3]);
        }

        [Test]
        public void AutoUpdate_AddWithFilter_FilteredInItemAppearsAtCorrectIndex()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);

            _source.Add(4);

            Assert.AreEqual(2, fl.Count);
            Assert.AreEqual(2, fl[0]);
            Assert.AreEqual(4, fl[1]);
        }

        [Test]
        public void AutoUpdate_AddWithFilterAndComparer_RespectsBothPredicates()
        {
            _source.AddRange(5, 1, 4, 2, 3);
            using var fl = new FilteredList<int>(_source, i => i > 2, Comparer<int>.Default);

            _source.Add(6);
            _source.Add(2); // filtered out

            Assert.AreEqual(4, fl.Count);
            Assert.AreEqual(3, fl[0]);
            Assert.AreEqual(4, fl[1]);
            Assert.AreEqual(5, fl[2]);
            Assert.AreEqual(6, fl[3]);
        }

        [Test]
        public void AutoUpdate_InsertInMiddle_ShiftsLaterIndexes()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);

            _source.Insert(2, 6);
            // source = [1, 2, 6, 3, 4, 5]; even = [2, 6, 4]
            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(2, fl[0]);
            Assert.AreEqual(6, fl[1]);
            Assert.AreEqual(4, fl[2]);
        }

        [Test]
        public void AutoUpdate_AddRange_BatchInsert_AllItemsFilteredAndSorted()
        {
            _source.AddRange(1, 5);
            using var fl = new FilteredList<int>(_source, i => i > 0, Comparer<int>.Default);

            _source.AddRange(3, 2, 4);

            Assert.AreEqual(5, fl.Count);
            Assert.AreEqual(1, fl[0]);
            Assert.AreEqual(2, fl[1]);
            Assert.AreEqual(3, fl[2]);
            Assert.AreEqual(4, fl[3]);
            Assert.AreEqual(5, fl[4]);
        }

        [Test]
        public void AutoUpdate_RemoveAt_RemovesFromViewAndShiftsTrailing()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.RemoveAt(1); // remove value 2
            // source = [1, 3, 4, 5]; filter > 1 => [3, 4, 5]
            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(3, fl[0]);
            Assert.AreEqual(4, fl[1]);
            Assert.AreEqual(5, fl[2]);
        }

        [Test]
        public void AutoUpdate_RemoveAt_FilteredOutItem_ViewUnchanged()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.RemoveAt(0); // remove value 1 (filtered out)

            Assert.AreEqual(4, fl.Count);
            Assert.AreEqual(2, fl[0]);
            Assert.AreEqual(3, fl[1]);
            Assert.AreEqual(4, fl[2]);
            Assert.AreEqual(5, fl[3]);
        }

        [Test]
        public void AutoUpdate_Remove_BinarySearchPositionCorrect()
        {
            _source.AddRange(5, 1, 3, 2, 4);
            using var fl = new FilteredList<int>(_source, Comparer<int>.Default);

            _source.Remove(3);
            // source = [5, 1, 2, 4]; sorted = [1, 2, 4, 5]
            Assert.AreEqual(4, fl.Count);
            Assert.AreEqual(1, fl[0]);
            Assert.AreEqual(2, fl[1]);
            Assert.AreEqual(4, fl[2]);
            Assert.AreEqual(5, fl[3]);
        }

        [Test]
        public void AutoUpdate_Replace_ItemPassesFilterToFails_RemovedFromView()
        {
            _source.AddRange(2, 4, 6);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);

            _source[1] = 3; // even -> odd
            // source = [2, 3, 6]; even = [2, 6]
            Assert.AreEqual(2, fl.Count);
            Assert.AreEqual(2, fl[0]);
            Assert.AreEqual(6, fl[1]);
        }

        [Test]
        public void AutoUpdate_Replace_ItemFailsFilterToPasses_AddedToView()
        {
            _source.AddRange(1, 3, 5);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);

            _source[1] = 4; // odd -> even
            // source = [1, 4, 5]; even = [4]
            Assert.AreEqual(1, fl.Count);
            Assert.AreEqual(4, fl[0]);
        }

        [Test]
        public void AutoUpdate_Replace_ItemStaysInFilter_ComparerReseats()
        {
            _source.AddRange(1, 5, 3);
            using var fl = new FilteredList<int>(_source, Comparer<int>.Default);

            _source[0] = 4;
            // source = [4, 5, 3]; sorted = [3, 4, 5]
            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(3, fl[0]);
            Assert.AreEqual(4, fl[1]);
            Assert.AreEqual(5, fl[2]);
        }

        [Test]
        public void AutoUpdate_Replace_NoFilterNoComparer_NoOp()
        {
            _source.AddRange(1, 2, 3);
            using var fl = new FilteredList<int>(_source);

            _source[1] = 99;

            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(1, fl[0]);
            Assert.AreEqual(99, fl[1]);
            Assert.AreEqual(3, fl[2]);
        }

        [Test]
        public void AutoUpdate_Move_NoComparer_PreservesSourceIndexOrder()
        {
            _source.AddRange(1, 2, 3, 4, 5);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.Move(1, 3); // move value 2 from idx 1 to idx 3
            // source = [1, 3, 4, 2, 5]; filter > 1 (in source order) = [3, 4, 2, 5]
            Assert.AreEqual(4, fl.Count);
            Assert.AreEqual(3, fl[0]);
            Assert.AreEqual(4, fl[1]);
            Assert.AreEqual(2, fl[2]);
            Assert.AreEqual(5, fl[3]);
        }

        [Test]
        public void AutoUpdate_Move_WithComparer_ViewUnchanged_IndexesRemapped()
        {
            _source.AddRange(3, 1, 2);
            using var fl = new FilteredList<int>(_source, Comparer<int>.Default);

            _source.Move(0, 2); // source becomes [1, 2, 3]; sorted view unchanged

            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(1, fl[0]);
            Assert.AreEqual(2, fl[1]);
            Assert.AreEqual(3, fl[2]);
        }

        [Test]
        public void AutoUpdate_Move_FilteredOutItem_ViewUnchanged()
        {
            _source.AddRange(1, 2, 3, 4);
            using var fl = new FilteredList<int>(_source, i => i > 1);

            _source.Move(0, 3); // moves value 1 (filtered out) to the end
            // source = [2, 3, 4, 1]; filter > 1 = [2, 3, 4]
            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(2, fl[0]);
            Assert.AreEqual(3, fl[1]);
            Assert.AreEqual(4, fl[2]);
        }

        [Test]
        public void AutoUpdate_SourceRemoveRange_BatchPropagatesIncrementally()
        {
            _source.AddRange(1, 2, 3, 4, 5, 6);
            using var fl = new FilteredList<int>(_source, i => i % 2 == 0);

            _source.RemoveRange(1, 3);
            // source = [1, 5, 6]; even = [6]
            Assert.AreEqual(1, fl.Count);
            Assert.AreEqual(6, fl[0]);
        }

        [Test]
        public void AutoUpdate_SourceRemoveRange_WithComparer_ViewStillSorted()
        {
            _source.AddRange(5, 1, 4, 2, 3);
            using var fl = new FilteredList<int>(_source, Comparer<int>.Default);

            _source.RemoveRange(1, 2); // removes values 1, 4
            // source = [5, 2, 3]; sorted = [2, 3, 5]
            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(2, fl[0]);
            Assert.AreEqual(3, fl[1]);
            Assert.AreEqual(5, fl[2]);
        }

        [Test]
        public void AutoUpdate_ParityWithFullRebuild_RandomSequence()
        {
            // Validates that the incremental dispatcher produces the same view as a fresh
            // full-rebuild FilteredList after each random operation, across all four
            // (filter? × comparer?) combinations.
            var combos = new (Predicate<int>? filter, IComparer<int>? comparer)[]
            {
                (null, null),
                (i => i % 3 != 0, null),
                (null, Comparer<int>.Default),
                (i => i > 25, Comparer<int>.Default),
            };

            foreach (var (filter, comparer) in combos)
            {
                var rng = new Random(42);
                var src = new ObservableList<int>();
                using var fl = new FilteredList<int>(src, filter, comparer);

                for (var i = 0; i < 10; i++)
                    src.Add(rng.Next(0, 100));

                for (var op = 0; op < 200; op++)
                {
                    var action = rng.Next(0, 7);
                    switch (action)
                    {
                        case 0:
                            src.Add(rng.Next(0, 100));
                            break;
                        case 1:
                            if (src.Count > 0) src.Insert(rng.Next(0, src.Count), rng.Next(0, 100));
                            else src.Add(rng.Next(0, 100));
                            break;
                        case 2:
                            if (src.Count > 0) src.RemoveAt(rng.Next(0, src.Count));
                            break;
                        case 3:
                            if (src.Count > 0) src[rng.Next(0, src.Count)] = rng.Next(0, 100);
                            break;
                        case 4:
                            if (src.Count >= 2)
                            {
                                var a = rng.Next(src.Count);
                                var b = rng.Next(src.Count);
                                src.Move(a, b);
                            }
                            break;
                        case 5:
                            if (src.Count > 5 && rng.Next(20) == 0) src.Clear();
                            break;
                        case 6:
                            if (src.Count >= 2)
                            {
                                var start = rng.Next(0, src.Count);
                                var maxCount = src.Count - start;
                                src.RemoveRange(start, rng.Next(1, maxCount + 1));
                            }
                            break;
                    }

                    using var reference = new FilteredList<int>(src, filter, comparer);
                    AssertViewEqual(reference, fl, op);
                }

                src.Dispose();
            }
        }

        private static void AssertViewEqual(FilteredList<int> expected, FilteredList<int> actual, int op)
        {
            Assert.AreEqual(expected.Count, actual.Count, $"Count mismatch after op {op}");
            for (var i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i], actual[i], $"Item[{i}] mismatch after op {op}");
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