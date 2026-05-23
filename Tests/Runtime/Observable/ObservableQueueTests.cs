using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class ObservableQueueTests
    {
        private EventCapture<int> _events;
        private ObservableQueue<int> _queue;

        [SetUp]
        public void SetUp()
        {
            _queue = new ObservableQueue<int>();
            _events = new EventCapture<int>(_queue);
        }

        [TearDown]
        public void TearDown()
        {
            _events.Dispose();
            _queue.Dispose();
        }

        #region Constructors
        [Test]
        public void Ctor_Default_CreatesEmptyQueue() =>
            Assert.AreEqual(0, new ObservableQueue<int>().Count);

        [Test]
        public void Ctor_Capacity_CreatesEmptyQueue() =>
            Assert.AreEqual(0, new ObservableQueue<int>(capacity: 10).Count);

        [Test]
        public void Ctor_Collection_CopiesItemsInOrder()
        {
            var queue = new ObservableQueue<int>(collection: new[] { 0, 1, 2 });

            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(0, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
        }
        #endregion
        
        #region Enqueue
        [Test]
        public void Enqueue_SingleItem_IncreasesCount()
        {
            _queue.Enqueue(10);
            Assert.AreEqual(1, _queue.Count);
        }

        [Test]
        public void Enqueue_SingleItem_RaisesAddEventWithCorrectIndex()
        {
            _queue.Enqueue(10);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(10, e.NewItem);
            Assert.AreEqual(0, e.NewStartingIndex);
        }

        [Test]
        public void Enqueue_SecondItem_IndexIsOne()
        {
            _queue.Enqueue(1);
            _events.Clear();
            _queue.Enqueue(2);

            Assert.AreEqual(1, _events.Last.NewStartingIndex);
        }

        [Test]
        public void EnqueueRange_Array_AddsAllItems()
        {
            _queue.EnqueueRange(new[] { 0, 1, 2 });
            Assert.AreEqual(3, _queue.Count);
        }

        [Test]
        public void EnqueueRange_Array_RaisesAddEventWithMultipleItems()
        {
            _queue.EnqueueRange(new[] { 0, 1, 2 });

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsFalse(e.IsSingleItem);
            Assert.AreEqual(3, e.NewItems!.Count);
            Assert.AreEqual(0, e.NewStartingIndex);
        }

        [Test]
        public void EnqueueRange_ReadOnlyList_AddsAllItems()
        {
            IReadOnlyList<int> items = new[] { 4, 5 };
            _queue.EnqueueRange(items);

            Assert.AreEqual(2, _queue.Count);
        }

        [Test]
        public void EnqueueRange_ToExistingQueue_IndexStartsAtCurrentCount()
        {
            _queue.Enqueue(0);
            _events.Clear();
            _queue.EnqueueRange(new[] { 1, 2 });

            Assert.AreEqual(1, _events.Last.NewStartingIndex);
        }
        #endregion

        #region Dequeue
        [Test]
        public void Dequeue_ReturnsFirstItemAndDecreasesCount()
        {
            _queue.EnqueueRange(new[] { 10, 20, 30 });

            var item = _queue.Dequeue();

            Assert.AreEqual(10, item);
            Assert.AreEqual(2, _queue.Count);
        }

        [Test]
        public void Dequeue_RaisesRemoveEventWithIndexZero()
        {
            _queue.Enqueue(42);
            _events.Clear();
            _queue.Dequeue();

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(42, e.OldItem);
            Assert.AreEqual(0, e.OldStartingIndex);
        }

        [Test]
        public void TryDequeue_NonEmpty_ReturnsTrueAndCorrectItem()
        {
            _queue.Enqueue(7);
            var result = _queue.TryDequeue(out var value);

            Assert.IsTrue(result);
            Assert.AreEqual(7, value);
        }

        [Test]
        public void TryDequeue_Empty_ReturnsFalseAndRaisesNoEvent()
        {
            var result = _queue.TryDequeue(out _);

            Assert.IsFalse(result);
            Assert.AreEqual(0, _events.Count);
        }

        [Test]
        public void DequeueRange_DequeuesMultipleItemsInFIFOOrder()
        {
            _queue.EnqueueRange(new[] { 0, 1, 2 });
            _events.Clear();
            var dest = new int[2];

            _queue.DequeueRange(dest);

            Assert.AreEqual(0, dest[0]);
            Assert.AreEqual(1, dest[1]);
            Assert.AreEqual(1, _queue.Count);
        }

        [Test]
        public void DequeueRange_RaisesRemoveEventWithMultipleItems()
        {
            _queue.EnqueueRange(new[] { 1, 2 });
            _events.Clear();
            _queue.DequeueRange(dest: new int[2]);

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.IsFalse(e.IsSingleItem);
            Assert.AreEqual(0, e.OldStartingIndex);
        }
        #endregion

        #region Peek
        [Test]
        public void Peek_ReturnsFirstItemWithoutRemoving()
        {
            _queue.EnqueueRange(new[] { 5, 6 });
            var item = _queue.Peek();

            Assert.AreEqual(5, item);
            Assert.AreEqual(2, _queue.Count);
        }

        [Test]
        public void TryPeek_NonEmpty_ReturnsTrueAndFirstItem()
        {
            _queue.Enqueue(26);
            var result = _queue.TryPeek(out var value);

            Assert.IsTrue(result);
            Assert.AreEqual(26, value);
            Assert.AreEqual(1, _queue.Count);
        }

        [Test]
        public void TryPeek_Empty_ReturnsFalse()
        {
            var result = _queue.TryPeek(out _);
            Assert.IsFalse(result);
        }
        #endregion
        
        [Test]
        public void ToArray_ReturnsItemsInFIFOOrder()
        {
            _queue.EnqueueRange(new[] { 0, 1, 2 });

            var arr = _queue.ToArray();
            Assert.AreEqual(new[] { 0, 1, 2 }, arr);
        }

        [Test]
        public void GetEnumerator_EnumeratesInFIFOOrder()
        {
            _queue.EnqueueRange(new[] { 10, 20, 30 });
            Assert.AreEqual(new[] { 10, 20, 30 }, Enumerable.ToArray(_queue));
        }

        [Test]
        public void TrimExcess_DoesNotChangeCount()
        {
            _queue.EnqueueRange(new[] { 1, 2, 3 });
            _queue.TrimExcess();

            Assert.AreEqual(3, _queue.Count);
        }
        
        [Test]
        public void Clear_RemovesAllItemsAndRaisesResetEvent()
        {
            _queue.EnqueueRange(new[] { 1, 2 });
            
            _events.Clear();
            _queue.Clear();

            Assert.AreEqual(0, _queue.Count);
            Assert.AreEqual(NotifyCollectionChangedAction.Reset, _events.Last.Action);
        }

        [Test]
        public void Dispose_ClearsQueueAndRemovesHandlers()
        {
            _queue.Enqueue(1);
            
            _events.Dispose();
            _queue.Dispose();

            Assert.AreEqual(0, _queue.Count);
        }
    }
}