using NUnit.Framework;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class SplitEventsExtensionsTests
    {
        private ObservableList<int> _source;

        [SetUp]
        public void SetUp() =>
            _source = new ObservableList<int>();

        [TearDown]
        public void TearDown() =>
            _source.Dispose();

        #region Add
        [Test]
        public void Added_InvokedOnAdd()
        {
            var capturedIndex = -1;
            IReadOnlyList<int> capturedItems = null;
            
            using var events = _source.SplitByEvents(added: (items, index) =>
            {
                capturedIndex = index;
                capturedItems = items;
            });

            _source.Add(26);

            Assert.IsNotNull(capturedItems);
            Assert.AreEqual(0, capturedIndex);
            
            Assert.AreEqual(1, capturedItems.Count);
            Assert.AreEqual(26, capturedItems[0]);
        }

        [Test]
        public void Added_InvokedOnAddRange()
        {
            var capturedIndex = -1;
            IReadOnlyList<int> capturedItems = null;
            
            using var events = _source.SplitByEvents(added: (items, index) =>
            {
                capturedIndex = index;
                capturedItems = items;
            });

            _source.AddRange(0, 1, 2);

            Assert.IsNotNull(capturedItems);
            Assert.AreEqual(0, capturedIndex);
            
            Assert.AreEqual(3, capturedItems.Count);

            for (var i = 0; i < capturedItems.Count; i++)
                Assert.AreEqual(i, capturedItems[i]);
        }
        #endregion
        
        [Test]
        public void Removed_InvokedOnRemove()
        {
            _source.Add(26);
            
            var capturedIndex = -1;
            IReadOnlyList<int> capturedItems = null;
            
            using var events = _source.SplitByEvents(removed: (items, index) =>
            {
                capturedItems = items;
                capturedIndex = index;
            });

            _source.RemoveAt(index: 0);

            Assert.IsNotNull(capturedItems);
            Assert.AreEqual(0, capturedIndex);
            Assert.AreEqual(26, capturedItems[0]);
        }
        
        [Test]
        public void Moved_InvokedOnMove()
        {
            _source.AddRange(0, 1, 2);
            
            var capturedOld = -1;
            var capturedNew = -1;
            IReadOnlyList<int> capturedItems = null;
            
            using var events = _source.SplitByEvents(moved: (items, oldIndex, newIndex) =>
            {
                capturedItems = items;
                capturedOld = oldIndex;
                capturedNew = newIndex;
            });

            _source.Move(0, 2);
            
            Assert.AreEqual(0, capturedOld);
            Assert.AreEqual(2, capturedNew);
            Assert.AreEqual(0, capturedItems[0]);
        }
        
        [Test]
        public void Replaced_InvokedOnIndexerSet()
        {
            _source.Add(11);
            
            var capturedIndex = -1;
            IReadOnlyList<int> capturedOld = null;
            IReadOnlyList<int> capturedNew = null;
            
            using var events = _source.SplitByEvents(replaced: (oldItems, newItems, index) =>
            {
                capturedIndex = index;
                capturedOld = oldItems;
                capturedNew = newItems;
            });

            _source[0] = 26;

            Assert.IsNotNull(capturedOld);
            
            Assert.AreEqual(0, capturedIndex);
            Assert.AreEqual(11, capturedOld[0]);
            Assert.AreEqual(26, capturedNew[0]);
        }
        
        [Test]
        public void Reset_InvokedOnClear()
        {
            _source.AddRange(0, 1);
            var resetCalled = false;
            
            using var events = _source.SplitByEvents(reset: () => resetCalled = true);
            _source.Clear();

            Assert.IsTrue(resetCalled);
        }
        
        [Test]
        public void NullCallbacks_DoNotThrow()
        {
            using var events = _source.SplitByEvents();

            Assert.DoesNotThrow(() =>
            {
                _source.Add(1);
                _source[0] = 2;
                _source.Move(0, 0);
                _source.RemoveAt(index: 0);
                _source.Clear();
            });
        }

        [Test]
        public void Dispose_StopsReceivingEvents()
        {
            var addedCount = 0;
            var events = _source.SplitByEvents(added: (_, _) => addedCount++);

            _source.Add(1);
            events.Dispose();
            _source.Add(2);

            Assert.AreEqual(1, addedCount);
        }
    }
}