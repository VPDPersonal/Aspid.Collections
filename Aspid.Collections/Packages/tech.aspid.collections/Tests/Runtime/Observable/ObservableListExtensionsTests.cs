using NUnit.Framework;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Extensions;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class ObservableListExtensionsTests
    {
        private ObservableList<int> _list;

        [SetUp]
        public void SetUp() =>
            _list = new ObservableList<int>(collection: new[] { 1, 2, 3, 4, 5 });

        [TearDown]
        public void TearDown() =>
            _list.Dispose();

        [Test]
        public void Swap_ExchangesElementsAtBothIndexes()
        {
            _list.Swap(index1: 0, index2: 4);

            Assert.AreEqual(5, _list[0]);
            Assert.AreEqual(1, _list[4]);
        }

        [Test]
        public void Swap_AdjacentElements_ExchangesCorrectly()
        {
            _list.Swap(index1: 1, index2: 2);

            Assert.AreEqual(3, _list[1]);
            Assert.AreEqual(2, _list[2]);
        }

        [TestCase(0, 2)]
        [TestCase(2, 0)]
        public void Swap_NonAdjacentElements_PublishesMovesThatReproduceTheSwap(int first, int second)
        {
            using var capture = new EventCapture<int>(_list);

            _list.Swap(first, second);

            CollectionAssert.AreEqual(new[] { 3, 2, 1, 4, 5 }, _list);
            Assert.That(capture.Count, Is.EqualTo(2));
            var firstMove = capture.Events[0];
            var secondMove = capture.Events[1];
            Assert.That(firstMove.Action, Is.EqualTo(NotifyCollectionChangedAction.Move));
            Assert.That(firstMove.OldStartingIndex, Is.EqualTo(first));
            Assert.That(firstMove.NewStartingIndex, Is.EqualTo(second));
            Assert.That(firstMove.NewItem, Is.EqualTo(first + 1));
            Assert.That(secondMove.Action, Is.EqualTo(NotifyCollectionChangedAction.Move));
            Assert.That(secondMove.OldStartingIndex, Is.EqualTo(1));
            Assert.That(secondMove.NewStartingIndex, Is.EqualTo(first));
            Assert.That(secondMove.NewItem, Is.EqualTo(second + 1));
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void Swap_AdjacentElements_FiresSingleMoveEvent(int index1, int index2)
        {
            using var capture = new EventCapture<int>(_list);

            _list.Swap(index1, index2);

            CollectionAssert.AreEqual(new[] { 2, 1, 3, 4, 5 }, _list);
            Assert.AreEqual(1, capture.Count);
        }

        [TestCase(-1, 2)]
        [TestCase(2, -1)]
        [TestCase(5, 0)]
        [TestCase(0, 5)]
        public void Swap_IndexOutOfRange_ThrowsWithoutLosingItem(int first, int second)
        {
            using var capture = new EventCapture<int>(_list);

            Assert.Throws<System.ArgumentOutOfRangeException>(() => _list.Swap(first, second));

            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, _list);
            Assert.AreEqual(0, capture.Count);
        }

        [Test]
        public void Swap_SameIndex_IsNoop()
        {
            using var capture = new EventCapture<int>(_list);

            _list.Swap(index1: 2, index2: 2);

            Assert.AreEqual(0, capture.Count);
            Assert.AreEqual(3, _list[2]);
        }

        [Test]
        public void Swap_ReverseOrder_ExchangesCorrectly()
        {
            _list.Swap(index1: 3, index2: 1);

            Assert.AreEqual(4, _list[1]);
            Assert.AreEqual(2, _list[3]);
        }
    }
}