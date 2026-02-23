using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class ObservableListTests
    {
        private EventCapture<int> _events;
        private ObservableList<int> _list;

        [SetUp]
        public void SetUp()
        {
            _list = new ObservableList<int>();
            _events = new EventCapture<int>(_list);
        }

        [TearDown]
        public void TearDown()
        {
            _events.Dispose();
            _list.Dispose();
        }
        
        #region Constructors
        [Test]
        public void Ctor_Default_CreatesEmptyList() =>
            Assert.AreEqual(0, new ObservableList<int>().Count);

        [Test]
        public void Ctor_Capacity_CreatesEmptyList() =>
            Assert.AreEqual(0, new ObservableList<int>(capacity: 10).Count);

        [Test]
        public void Ctor_Collection_CopiesItems()
        {
            var list = new ObservableList<int>(collection: new[] { 0, 1, 2 });

            Assert.AreEqual(3, list.Count);

            for (var i = 0; i < list.Count; i++)
                Assert.AreEqual(i, list[i]);
        }
        #endregion
        
        #region Add
        [Test]
        public void Add_SingleItem_IncreasesCountAndContainsItem()
        {
            _list.Add(26);

            Assert.AreEqual(1, _list.Count);
            Assert.IsTrue(_list.Contains(26));
        }

        [Test]
        public void Add_SingleItem_RaisesAddEventWithCorrectArgs()
        {
            _list.Add(26);

            Assert.AreEqual(1, _events.Count);
            
            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(26, e.NewItem);
            Assert.AreEqual(0, e.NewStartingIndex);
        }

        [Test]
        public void Add_MultipleItems_IndexIncrementsCorrectly()
        {
            _list.Add(1);
            _list.Add(2);

            Assert.AreEqual(0, _events.Events[0].NewStartingIndex);
            Assert.AreEqual(1, _events.Events[1].NewStartingIndex);
        }

        [Test]
        public void AddRange_Array_AddsAllItems()
        {
            _list.AddRange(1, 2, 3);

            Assert.AreEqual(3, _list.Count);
            Assert.AreEqual(1, _list[0]);
            Assert.AreEqual(2, _list[1]);
            Assert.AreEqual(3, _list[2]);
        }

        [Test]
        public void AddRange_Array_RaisesAddEventWithMultipleItems()
        {
            _list.AddRange(1, 2, 3);

            Assert.AreEqual(1, _events.Count);
            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsFalse(e.IsSingleItem);
            Assert.AreEqual(3, e.NewItems!.Count);
            Assert.AreEqual(0, e.NewStartingIndex);
        }

        [Test]
        public void AddRange_ToExistingList_IndexStartsAtCurrentCount()
        {
            _list.Add(0);
            _events.Clear();
            _list.AddRange(1, 2);

            Assert.AreEqual(1, _events.Last.NewStartingIndex);
        }

        [Test]
        public void AddRange_ReadOnlyList_AddsAllItems()
        {
            IReadOnlyList<int> items = new[] { 10, 20 };
            _list.AddRange(items);

            Assert.AreEqual(2, _list.Count);
            Assert.AreEqual(10, _list[0]);
        }
        #endregion

        #region Insert
        [Test]
        public void Insert_AtBeginning_ShiftsExistingItems()
        {
            _list.AddRange(1, 2);
            _events.Clear();
            _list.Insert(0, 99);

            Assert.AreEqual(3, _list.Count);
            Assert.AreEqual(99, _list[0]);
            Assert.AreEqual(1, _list[1]);
        }

        [Test]
        public void Insert_RaisesAddEventWithCorrectIndex()
        {
            _list.AddRange(1, 2);
            _events.Clear();
            _list.Insert(1, 55);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(55, e.NewItem);
            Assert.AreEqual(1, e.NewStartingIndex);
        }

        [Test]
        public void InsertRange_Array_InsertsAllAtIndex()
        {
            _list.AddRange(1, 4);
            _events.Clear();
            _list.InsertRange(1, 2, 3);

            Assert.AreEqual(4, _list.Count);
            Assert.AreEqual(1, _list[0]);
            Assert.AreEqual(2, _list[1]);
            Assert.AreEqual(3, _list[2]);
            Assert.AreEqual(4, _list[3]);
        }

        [Test]
        public void InsertRange_ReadOnlyList_InsertsAllAtIndex()
        {
            _list.AddRange(1, 4);
            _events.Clear();
            IReadOnlyList<int> items = new[] { 2, 3 };
            _list.InsertRange(1, items);

            Assert.AreEqual(4, _list.Count);
            Assert.AreEqual(2, _list[1]);
        }
        #endregion

        #region Remove
        [Test]
        public void Remove_ExistingItem_ReturnsTrueAndDecreasesCount()
        {
            _list.Add(10);

            var result = _list.Remove(10);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _list.Count);
        }

        [Test]
        public void Remove_NonExistingItem_ReturnsFalseAndRaisesNoEvent()
        {
            _list.Add(10);
            _events.Clear();

            var result = _list.Remove(999);

            Assert.IsFalse(result);
            Assert.AreEqual(0, _events.Count);
        }

        [Test]
        public void Remove_RaisesRemoveEventWithCorrectArgs()
        {
            _list.AddRange(10, 20, 30);
            _events.Clear();
            _list.Remove(20);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(20, e.OldItem);
            Assert.AreEqual(1, e.OldStartingIndex);
        }

        [Test]
        public void RemoveAt_ValidIndex_RemovesItem()
        {
            _list.AddRange(1, 2, 3);

            _list.RemoveAt(1);

            Assert.AreEqual(2, _list.Count);
            Assert.AreEqual(1, _list[0]);
            Assert.AreEqual(3, _list[1]);
        }

        [Test]
        public void RemoveAt_RaisesRemoveEventWithCorrectIndex()
        {
            _list.AddRange(1, 2, 3);
            _events.Clear();
            _list.RemoveAt(2);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.AreEqual(3, e.OldItem);
            Assert.AreEqual(2, e.OldStartingIndex);
        }
        #endregion

        #region Indexer
        [Test]
        public void Indexer_Get_ReturnsCorrectItem()
        {
            _list.AddRange(10, 20, 30);
            Assert.AreEqual(20, _list[1]);
        }

        [Test]
        public void Indexer_Set_ReplacesItem()
        {
            _list.AddRange(1, 2, 3);

            _list[1] = 99;

            Assert.AreEqual(99, _list[1]);
            Assert.AreEqual(3, _list.Count);
        }

        [Test]
        public void Indexer_Set_RaisesReplaceEventWithCorrectArgs()
        {
            _list.AddRange(1, 2, 3);
            _events.Clear();
            _list[1] = 99;

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Replace, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(2, e.OldItem);
            Assert.AreEqual(99, e.NewItem);
            Assert.AreEqual(1, e.OldStartingIndex);
            Assert.AreEqual(1, e.NewStartingIndex);
        }
        #endregion
        
        #region Move
        [Test]
        public void Move_Forward_MovesItemCorrectly()
        {
            _list.AddRange(1, 2, 3);

            _list.Move(0, 2);

            Assert.AreEqual(2, _list[0]);
            Assert.AreEqual(3, _list[1]);
            Assert.AreEqual(1, _list[2]);
        }

        [Test]
        public void Move_Backward_MovesItemCorrectly()
        {
            _list.AddRange(1, 2, 3);

            _list.Move(2, 0);

            Assert.AreEqual(3, _list[0]);
            Assert.AreEqual(1, _list[1]);
            Assert.AreEqual(2, _list[2]);
        }

        [Test]
        public void Move_RaisesMoveEventWithCorrectIndexes()
        {
            _list.AddRange(1, 2, 3);
            _events.Clear();
            _list.Move(0, 2);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Move, e.Action);
            Assert.AreEqual(1, e.OldItem);
            Assert.AreEqual(0, e.OldStartingIndex);
            Assert.AreEqual(2, e.NewStartingIndex);
        }
        #endregion

        #region Enumeration
        [Test]
        public void IndexOf_ExistingItem_ReturnsIndex()
        {
            _list.AddRange(10, 20, 30);
            Assert.AreEqual(1, _list.IndexOf(20));
        }

        [Test]
        public void IndexOf_NonExistingItem_ReturnsMinusOne()
        {
            _list.Add(10);
            Assert.AreEqual(-1, _list.IndexOf(999));
        }

        [Test]
        public void Contains_ExistingItem_ReturnsTrue()
        {
            _list.Add(5);
            Assert.IsTrue(_list.Contains(5));
        }

        [Test]
        public void Contains_NonExistingItem_ReturnsFalse()
        {
            Assert.IsFalse(_list.Contains(5));
        }

        [Test]
        public void ForEach_ExecutesActionOnAllItems()
        {
            _list.AddRange(1, 2, 3);
            var collected = new List<int>();

            _list.ForEach(collected.Add);

            Assert.AreEqual(new[] { 1, 2, 3 }, collected.ToArray());
        }

        [Test]
        public void CopyTo_CopiesItemsToArray()
        {
            _list.AddRange(10, 20, 30);
            var dest = new int[3];

            _list.CopyTo(dest, arrayIndex: 0);

            Assert.AreEqual(new[] { 10, 20, 30 }, dest);
        }

        [Test]
        public void GetEnumerator_Generic_EnumeratesAllItems()
        {
            _list.AddRange(1, 2, 3);
            Assert.AreEqual(new[] { 1, 2, 3 }, _list.ToArray());
        }

        [Test]
        public void GetEnumerator_NonGeneric_EnumeratesAllItems()
        {
            _list.AddRange(4, 5, 6);
            Assert.AreEqual(new[] { 4, 5, 6 }, _list.ToArray());
        }

        [Test]
        public void IsReadOnly_ReturnsFalse()
        {
            Assert.IsFalse(_list.IsReadOnly);
        }
        #endregion
        
        #region Clear And Dispose
        [Test]
        public void Clear_RemovesAllItems()
        {
            _list.AddRange(1, 2, 3);

            _list.Clear();

            Assert.AreEqual(0, _list.Count);
        }

        [Test]
        public void Clear_RaisesResetEvent()
        {
            _list.AddRange(1, 2);
            _events.Clear();

            _list.Clear();

            Assert.AreEqual(1, _events.Count);
            Assert.AreEqual(NotifyCollectionChangedAction.Reset, _events.Last.Action);
        }

        [Test]
        public void Dispose_ClearsItemsAndHandlers()
        {
            _list.AddRange(1, 2, 3);
            _events.Dispose();

            _list.Dispose();

            Assert.AreEqual(0, _list.Count);
        }
        #endregion
        
        #region Event
        [Test]
        public void CollectionChanged_MultipleHandlers_AllInvoked()
        {
            var count1 = 0;
            var count2 = 0;
            _list.CollectionChanged += _ => count1++;
            _list.CollectionChanged += _ => count2++;

            _list.Add(1);

            Assert.AreEqual(1, count1);
            Assert.AreEqual(1, count2);
        }

        [Test]
        public void CollectionChanged_UnsubscribedHandler_NotInvoked()
        {
            var count = 0;
            NotifyCollectionChangedEventHandler<int> handler = _ => count++;
            _list.CollectionChanged += handler;
            _list.CollectionChanged -= handler;

            _list.Add(1);

            Assert.AreEqual(0, count);
        }
        #endregion
    }
}
