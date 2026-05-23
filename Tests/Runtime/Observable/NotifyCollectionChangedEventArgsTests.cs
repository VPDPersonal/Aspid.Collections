using NUnit.Framework;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class NotifyCollectionChangedEventArgsTests
    {
        #region Add (Single)
        [Test]
        public void Add_Single_SetsActionAddAndIsSingleItem()
        {
            var args = NotifyCollectionChangedEventArgs<int>.Add(42, 3);

            Assert.AreEqual(NotifyCollectionChangedAction.Add, args.Action);
            Assert.IsTrue(args.IsSingleItem);
        }

        [Test]
        public void Add_Single_SetsNewItemAndNewStartingIndex()
        {
            var args = NotifyCollectionChangedEventArgs<string>.Add("hello", 7);

            Assert.AreEqual("hello", args.NewItem);
            Assert.AreEqual(7, args.NewStartingIndex);
        }

        [Test]
        public void Add_Single_OldItemAndOldStartingIndexAreDefault()
        {
            var args = NotifyCollectionChangedEventArgs<int>.Add(1, 0);

            Assert.AreEqual(default(int), args.OldItem);
            Assert.AreEqual(-1, args.OldStartingIndex);
            Assert.IsNull(args.OldItems);
        }
        #endregion
        
        #region Add (Range)
        [Test]
        public void Add_Range_SetsActionAddAndIsNotSingleItem()
        {
            var items = new[] { 1, 2, 3 };
            var args = NotifyCollectionChangedEventArgs<int>.Add(items, 0);

            Assert.AreEqual(NotifyCollectionChangedAction.Add, args.Action);
            Assert.IsFalse(args.IsSingleItem);
        }

        [Test]
        public void Add_Range_SetsNewItemsAndNewStartingIndex()
        {
            var items = new[] { "a", "b" };
            var args = NotifyCollectionChangedEventArgs<string>.Add(items, 5);

            Assert.AreEqual(items, args.NewItems);
            Assert.AreEqual(5, args.NewStartingIndex);
        }
        #endregion
        
        #region Remove (Single)
        [Test]
        public void Remove_Single_SetsActionRemoveAndIsSingleItem()
        {
            var args = NotifyCollectionChangedEventArgs<int>.Remove(99, 2);

            Assert.AreEqual(NotifyCollectionChangedAction.Remove, args.Action);
            Assert.IsTrue(args.IsSingleItem);
        }

        [Test]
        public void Remove_Single_SetsOldItemAndOldStartingIndex()
        {
            var args = NotifyCollectionChangedEventArgs<string>.Remove("bye", 4);

            Assert.AreEqual("bye", args.OldItem);
            Assert.AreEqual(4, args.OldStartingIndex);
            Assert.IsNull(args.NewItems);
        }
        #endregion

        #region Remove (Range)
        [Test]
        public void Remove_Range_SetsOldItemsAndIsNotSingleItem()
        {
            var items = new[] { 10, 20 };
            var args = NotifyCollectionChangedEventArgs<int>.Remove(items, 1);

            Assert.IsFalse(args.IsSingleItem);
            Assert.AreEqual(items, args.OldItems);
            Assert.AreEqual(1, args.OldStartingIndex);
        }
        #endregion
        
        #region Replace (Single)
        [Test]
        public void Replace_Single_SetsActionReplaceAndBothIndexesEqual()
        {
            var args = NotifyCollectionChangedEventArgs<int>.Replace(1, 2, 3);

            Assert.AreEqual(NotifyCollectionChangedAction.Replace, args.Action);
            Assert.IsTrue(args.IsSingleItem);
            Assert.AreEqual(3, args.OldStartingIndex);
            Assert.AreEqual(3, args.NewStartingIndex);
        }

        [Test]
        public void Replace_Single_SetsOldAndNewItems()
        {
            var args = NotifyCollectionChangedEventArgs<string>.Replace("old", "new", 0);

            Assert.AreEqual("old", args.OldItem);
            Assert.AreEqual("new", args.NewItem);
        }
        #endregion
        
        #region Replace (Range)
        [Test]
        public void Replace_Range_SetsOldAndNewItemsAndIsNotSingleItem()
        {
            var oldItems = new[] { 1, 2 };
            var newItems = new[] { 3, 4 };
            var args = NotifyCollectionChangedEventArgs<int>.Replace(oldItems, newItems, 5);

            Assert.IsFalse(args.IsSingleItem);
            Assert.AreEqual(oldItems, args.OldItems);
            Assert.AreEqual(newItems, args.NewItems);
            Assert.AreEqual(5, args.OldStartingIndex);
            Assert.AreEqual(5, args.NewStartingIndex);
        }
        #endregion
        
        #region Move
        [Test]
        public void Move_SetsActionMoveAndOldEqualsNew()
        {
            var args = NotifyCollectionChangedEventArgs<string>.Move("item", 1, 4);

            Assert.AreEqual(NotifyCollectionChangedAction.Move, args.Action);
            Assert.IsTrue(args.IsSingleItem);
            Assert.AreEqual("item", args.OldItem);
            Assert.AreEqual("item", args.NewItem);
            Assert.AreEqual(1, args.OldStartingIndex);
            Assert.AreEqual(4, args.NewStartingIndex);
        }
        #endregion
        
        #region Reset
        [Test]
        public void Reset_SetsActionReset()
        {
            var args = NotifyCollectionChangedEventArgs<int>.Reset();

            Assert.AreEqual(NotifyCollectionChangedAction.Reset, args.Action);
            Assert.IsTrue(args.IsSingleItem);
            Assert.AreEqual(default(int), args.OldItem);
            Assert.AreEqual(default(int), args.NewItem);
        }
        #endregion
    }
}