using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class CollectionChangedEventTests
    {
        [Test]
        public void NestedDispatch_UsesCurrentSubscriptionsAndSkipsRemovedOuterSubscription()
        {
            using var source = new ObservableList<int>();
            var calls = new List<string>();
            NotifyCollectionChangedEventHandler<int> second = args => calls.Add($"second:{args.NewItem}");
            NotifyCollectionChangedEventHandler<int> third = args => calls.Add($"third:{args.NewItem}");

            source.CollectionChanged += args =>
            {
                if (args.Action != NotifyCollectionChangedAction.Add) return;
                calls.Add($"first:{args.NewItem}");
                if (args.NewItem != 1) return;

                source.CollectionChanged -= second;
                source.CollectionChanged += second;
                source.CollectionChanged += third;
                source.Add(2);
            };
            source.CollectionChanged += second;

            source.Add(1);
            source.Add(3);

            CollectionAssert.AreEqual(new[]
            {
                "first:1", "first:2", "second:2", "third:2",
                "first:3", "second:3", "third:3",
            }, calls);
        }

        [Test]
        public void SubscriberThrows_AfterUnsubscribing_LeavesNextDispatchUsable()
        {
            using var source = new ObservableList<int>();
            var observed = new List<int>();
            NotifyCollectionChangedEventHandler<int> recorder = args => observed.Add(args.NewItem);
            var expected = new InvalidOperationException("Subscriber failed");

            source.CollectionChanged += args =>
            {
                if (args.Action != NotifyCollectionChangedAction.Add || args.NewItem != 1) return;

                source.CollectionChanged -= recorder;
                throw expected;
            };
            source.CollectionChanged += recorder;

            Assert.That(Assert.Throws<InvalidOperationException>(() => source.Add(1)), Is.SameAs(expected));
            Assert.That(observed, Is.Empty);
            CollectionAssert.AreEqual(new[] { 1 }, source);

            source.CollectionChanged += recorder;
            source.Add(2);

            CollectionAssert.AreEqual(new[] { 2 }, observed);
            CollectionAssert.AreEqual(new[] { 1, 2 }, source);
        }
    }
}
