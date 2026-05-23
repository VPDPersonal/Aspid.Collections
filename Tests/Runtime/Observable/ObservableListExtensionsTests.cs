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

        [Test]
        public void Swap_FiresTwoMoveEvents()
        {
            using var capture = new EventCapture<int>(_list);

            _list.Swap(index1: 0, index2: 2);

            Assert.AreEqual(2, capture.Count);
            Assert.AreEqual(NotifyCollectionChangedAction.Move, capture.Events[0].Action);
            Assert.AreEqual(NotifyCollectionChangedAction.Move, capture.Events[1].Action);
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