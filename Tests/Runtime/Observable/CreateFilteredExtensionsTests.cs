using NUnit.Framework;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class CreateFilteredExtensionsTests
    {
        private ObservableList<int> _source;

        [SetUp]
        public void SetUp()
        {
            _source = new ObservableList<int>(new[] { 1, 2, 3, 4, 5 });
        }

        [TearDown]
        public void TearDown()
        {
            _source.Dispose();
        }

        [Test]
        public void CreateFiltered_NoArgs_ReturnsFilteredListWithAllItems()
        {
            using var fl = _source.CreateFiltered();

            Assert.AreEqual(5, fl.Count);
        }

        [Test]
        public void CreateFiltered_WithFilter_ReturnsFilteredResult()
        {
            using var fl = _source.CreateFiltered(i => i > 3);

            Assert.AreEqual(2, fl.Count);
            Assert.AreEqual(4, fl[0]);
            Assert.AreEqual(5, fl[1]);
        }

        [Test]
        public void CreateFiltered_WithComparer_ReturnsSortedResult()
        {
            var descending = Comparer<int>.Create((a, b) => b.CompareTo(a));
            using var fl = _source.CreateFiltered(descending);

            Assert.AreEqual(5, fl[0]);
            Assert.AreEqual(1, fl[4]);
        }

        [Test]
        public void CreateFiltered_WithFilterAndComparer_ReturnsFilteredAndSortedResult()
        {
            var descending = Comparer<int>.Create((a, b) => b.CompareTo(a));
            using var fl = _source.CreateFiltered(i => i > 2, descending);

            Assert.AreEqual(3, fl.Count);
            Assert.AreEqual(5, fl[0]);
            Assert.AreEqual(4, fl[1]);
            Assert.AreEqual(3, fl[2]);
        }

        [Test]
        public void CreateFiltered_OnObservableList_AutoUpdatesOnChanges()
        {
            using var fl = _source.CreateFiltered(i => i > 3);
            var initialCount = fl.Count;

            _source.Add(10);

            Assert.AreEqual(initialCount + 1, fl.Count);
        }
    }
}