using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class ObservableStackTests
    {
        private EventCapture<int> _events;
        private ObservableStack<int> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new ObservableStack<int>();
            _events = new EventCapture<int>(_stack);
        }

        [TearDown]
        public void TearDown()
        {
            _events.Dispose();
            _stack.Dispose();
        }

        #region Constructors
        [Test]
        public void Ctor_Default_CreatesEmptyStack() =>
            Assert.AreEqual(0, new ObservableStack<int>().Count);

        [Test]
        public void Ctor_Capacity_CreatesEmptyStack() =>
            Assert.AreEqual(0, new ObservableStack<int>(capacity: 10).Count);

        [Test]
        public void Ctor_Collection_CopiesItems()
        {
            var stack = new ObservableStack<int>(collection: new[] { 0, 1, 2 });

            Assert.AreEqual(3, stack.Count);
            Assert.AreEqual(2, stack.Pop());
        }
        #endregion
        
        #region Push
        [Test]
        public void Push_SingleItem_IncreasesCount()
        {
            _stack.Push(10);
            Assert.AreEqual(1, _stack.Count);
        }

        [Test]
        public void Push_SingleItem_RaisesAddEventWithIndexZero()
        {
            _stack.Push(10);
            var e = _events.Last;
            
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(10, e.NewItem);
            Assert.AreEqual(0, e.NewStartingIndex);
        }

        [Test]
        public void Push_PlacesItemOnTop()
        {
            _stack.Push(1);
            _stack.Push(2);

            Assert.AreEqual(2, _stack.Peek());
        }

        [Test]
        public void PushRange_Array_AddsAllItemsAndTopIsLastElement()
        {
            _stack.PushRange(new[] { 0, 1, 2 });

            Assert.AreEqual(3, _stack.Count);
            Assert.AreEqual(2, _stack.Peek());
        }

        [Test]
        public void PushRange_Array_RaisesAddEventWithMultipleItemsAndIndexZero()
        {
            _stack.PushRange(new[] { 0, 1, 2 });

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsFalse(e.IsSingleItem);
            Assert.AreEqual(3, e.NewItems!.Count);
            Assert.AreEqual(0, e.NewStartingIndex);
        }

        [Test]
        public void PushRange_ReadOnlyList_AddsAllItems()
        {
            IReadOnlyList<int> items = new[] { 0, 1 };
            _stack.PushRange(items);

            Assert.AreEqual(2, _stack.Count);
        }
        #endregion
        
        #region Pop
        [Test]
        public void Pop_ReturnsTopItemAndDecreasesCount()
        {
            _stack.Push(0);
            _stack.Push(1);

            var item = _stack.Pop();

            Assert.AreEqual(1, item);
            Assert.AreEqual(1, _stack.Count);
        }

        [Test]
        public void Pop_RaisesRemoveEventWithIndexZero()
        {
            _stack.Push(26);
            _events.Clear();
            _stack.Pop();

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(26, e.OldItem);
            Assert.AreEqual(0, e.OldStartingIndex);
        }

        [Test]
        public void TryPop_NonEmpty_ReturnsTrueAndTopItem()
        {
            _stack.Push(26);
            var result = _stack.TryPop(out var value);

            Assert.IsTrue(result);
            Assert.AreEqual(26, value);
            Assert.AreEqual(0, _stack.Count);
        }

        [Test]
        public void TryPop_Empty_ReturnsFalseAndRaisesNoEvent()
        {
            var result = _stack.TryPop(out _);

            Assert.IsFalse(result);
            Assert.AreEqual(0, _events.Count);
        }

        [Test]
        public void PopRange_PopsItemsFromTop()
        {
            _stack.PushRange(new[] { 0, 1, 2 });
            _events.Clear();
            var dest = new int[2];

            _stack.PopRange(dest);
            
            Assert.AreEqual(2, dest[0]);
            Assert.AreEqual(1, dest[1]);
            Assert.AreEqual(1, _stack.Count);
        }

        [Test]
        public void PopRange_RaisesRemoveEventWithMultipleItems()
        {
            _stack.PushRange(new[] { 1, 2 });
            _events.Clear();
            _stack.PopRange(new int[2]);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.IsFalse(e.IsSingleItem);
            Assert.AreEqual(0, e.OldStartingIndex);
        }
        #endregion
        
        #region Peek
        [Test]
        public void Peek_ReturnsTopItemWithoutRemoving()
        {
            _stack.Push(5);
            _stack.Push(6);

            var item = _stack.Peek();

            Assert.AreEqual(6, item);
            Assert.AreEqual(2, _stack.Count);
        }

        [Test]
        public void TryPeek_NonEmpty_ReturnsTrueAndTopItem()
        {
            _stack.Push(26);

            var result = _stack.TryPeek(out var value);

            Assert.IsTrue(result);
            Assert.AreEqual(26, value);
            Assert.AreEqual(1, _stack.Count);
        }

        [Test]
        public void TryPeek_Empty_ReturnsFalse()
        {
            var result = _stack.TryPeek(out _);
            Assert.IsFalse(result);
        }
        #endregion

        [Test]
        public void ToArray_ReturnsItemsInLIFOOrder()
        {
            _stack.Push(1);
            _stack.Push(2);
            _stack.Push(3);
            
            var array = _stack.ToArray();

            Assert.AreEqual(3, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(1, array[2]);
        }

        [Test]
        public void GetEnumerator_EnumeratesTopToBottom()
        {
            _stack.Push(0);
            _stack.Push(1);
            _stack.Push(2);
            var result = _stack.ToList();
            
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(0, result[2]);
        }

        [Test]
        public void GetEnumerator_NonGeneric_EnumeratesAllItems()
        {
            _stack.Push(10);
            _stack.Push(20);
            var result = _stack.ToList();

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void TrimExcess_DoesNotChangeCount()
        {
            _stack.Push(1);
            _stack.Push(2);

            _stack.TrimExcess();

            Assert.AreEqual(2, _stack.Count);
        }
        
        [Test]
        public void Clear_RemovesAllItemsAndRaisesResetEvent()
        {
            _stack.Push(1);
            _stack.Push(2);
            
            _events.Clear();
            _stack.Clear();

            Assert.AreEqual(0, _stack.Count);
            Assert.AreEqual(NotifyCollectionChangedAction.Reset, _events.Last.Action);
        }

        [Test]
        public void Dispose_ClearsStackAndRemovesHandlers()
        {
            _stack.Push(1);
            
            _events.Dispose();
            _stack.Dispose();

            Assert.AreEqual(0, _stack.Count);
        }
    }
}