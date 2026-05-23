using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public class ObservableDictionaryTests
    {
        private ObservableDictionary<string, int> _dict;
        private EventCapture<KeyValuePair<string, int>> _events;

        [SetUp]
        public void SetUp()
        {
            _dict = new ObservableDictionary<string, int>();
            _events = new EventCapture<KeyValuePair<string, int>>(_dict);
        }

        [TearDown]
        public void TearDown()
        {
            _events.Dispose();
            _dict.Dispose();
        }
        
        #region Constructors
        [Test]
        public void Ctor_Default_CreatesEmptyDictionary()
        {
            Assert.AreEqual(0, _dict.Count);
        }

        [Test]
        public void Ctor_Capacity_CreatesEmptyDictionary()
        {
            var dict = new ObservableDictionary<string, int>(10);
            Assert.AreEqual(0, dict.Count);
        }

        [Test]
        public void Ctor_Comparer_UsesProvidedComparer()
        {
            var dict = new ObservableDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(StringComparer.OrdinalIgnoreCase, dict.Comparer);
        }

        [Test]
        public void Ctor_Collection_CopiesAllItems()
        {
            var source = new[]
            {
                new KeyValuePair<string, int>("a", 1),
                new KeyValuePair<string, int>("b", 2),
            };
            var dict = new ObservableDictionary<string, int>(source);

            Assert.AreEqual(2, dict.Count);
            Assert.AreEqual(1, dict["a"]);
            Assert.AreEqual(2, dict["b"]);
        }
        #endregion
        
        #region Add
        [Test]
        public void Add_KeyValue_IncreasesCountAndContainsKey()
        {
            _dict.Add("x", 10);

            Assert.AreEqual(1, _dict.Count);
            Assert.IsTrue(_dict.ContainsKey("x"));
            Assert.AreEqual(10, _dict["x"]);
        }

        [Test]
        public void Add_KeyValue_RaisesAddEventWithIndexMinusOne()
        {
            _dict.Add("x", 10);

            Assert.AreEqual(1, _events.Count);
            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
            Assert.IsTrue(e.IsSingleItem);
            Assert.AreEqual(new KeyValuePair<string, int>("x", 10), e.NewItem);
            Assert.AreEqual(-1, e.NewStartingIndex);
        }

        [Test]
        public void Add_KVP_IncreasesCountAndContainsKey()
        {
            _dict.Add(new KeyValuePair<string, int>("y", 20));

            Assert.AreEqual(1, _dict.Count);
            Assert.AreEqual(20, _dict["y"]);
        }

        [Test]
        public void Add_DuplicateKey_ThrowsArgumentException()
        {
            _dict.Add("a", 1);

            Assert.Throws<ArgumentException>(() => _dict.Add("a", 2));
        }
        #endregion
        
        #region Indexer
        [Test]
        public void Indexer_Get_ExistingKey_ReturnsValue()
        {
            _dict.Add("k", 42);

            Assert.AreEqual(42, _dict["k"]);
        }

        [Test]
        public void Indexer_Get_NonExistingKey_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => { var _ = _dict["missing"]; });
        }

        [Test]
        public void Indexer_Set_ExistingKey_ReplacesValueAndRaisesReplaceEvent()
        {
            _dict.Add("k", 1);
            _events.Clear();

            _dict["k"] = 99;

            Assert.AreEqual(99, _dict["k"]);
            Assert.AreEqual(1, _events.Count);
            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Replace, e.Action);
            Assert.AreEqual(new KeyValuePair<string, int>("k", 1), e.OldItem);
            Assert.AreEqual(new KeyValuePair<string, int>("k", 99), e.NewItem);
            Assert.AreEqual(-1, e.OldStartingIndex);
        }

        [Test]
        public void Indexer_Set_NonExistingKey_AddsEntryAndRaisesAddEvent()
        {
            _dict["newKey"] = 77;

            Assert.AreEqual(1, _dict.Count);
            Assert.AreEqual(1, _events.Count);
            Assert.AreEqual(NotifyCollectionChangedAction.Add, _events.Last.Action);
        }
        #endregion

        #region Remove
        [Test]
        public void Remove_ExistingKey_ReturnsTrueAndDecreasesCount()
        {
            _dict.Add("r", 5);

            var result = _dict.Remove("r");

            Assert.IsTrue(result);
            Assert.AreEqual(0, _dict.Count);
        }

        [Test]
        public void Remove_NonExistingKey_ReturnsFalseAndRaisesNoEvent()
        {
            _events.Clear();

            var result = _dict.Remove("missing");

            Assert.IsFalse(result);
            Assert.AreEqual(0, _events.Count);
        }

        [Test]
        public void Remove_ExistingKey_RaisesRemoveEventWithIndexMinusOne()
        {
            _dict.Add("r", 5);
            _events.Clear();

            _dict.Remove("r");

            var e = _events.Last;
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
            Assert.AreEqual(new KeyValuePair<string, int>("r", 5), e.OldItem);
            Assert.AreEqual(-1, e.OldStartingIndex);
        }

        [Test]
        public void Remove_KVP_MatchingValue_ReturnsTrueAndRaisesEvent()
        {
            _dict.Add("m", 7);
            _events.Clear();

            var result = _dict.Remove(new KeyValuePair<string, int>("m", 7));

            Assert.IsTrue(result);
            Assert.AreEqual(1, _events.Count);
        }

        [Test]
        public void Remove_KVP_KeyExistsButValueDiffers_ReturnsFalse()
        {
            _dict.Add("m", 7);
            _events.Clear();

            var result = _dict.Remove(new KeyValuePair<string, int>("m", 999));

            Assert.IsFalse(result);
            Assert.AreEqual(0, _events.Count);
        }

        [Test]
        public void Remove_KVP_KeyNotFound_ReturnsFalse()
        {
            var result = _dict.Remove(new KeyValuePair<string, int>("nope", 0));

            Assert.IsFalse(result);
        }
        #endregion
        
        [Test]
        public void ContainsKey_ExistingKey_ReturnsTrue()
        {
            _dict.Add("ck", 1);
            Assert.IsTrue(_dict.ContainsKey("ck"));
        }

        [Test]
        public void ContainsKey_NonExistingKey_ReturnsFalse()
        {
            Assert.IsFalse(_dict.ContainsKey("nope"));
        }

        [Test]
        public void TryGetValue_ExistingKey_ReturnsTrueAndSetsValue()
        {
            _dict.Add("t", 55);

            var result = _dict.TryGetValue("t", out var value);

            Assert.IsTrue(result);
            Assert.AreEqual(55, value);
        }

        [Test]
        public void TryGetValue_NonExistingKey_ReturnsFalse()
        {
            var result = _dict.TryGetValue("missing", out _);

            Assert.IsFalse(result);
        }

        [Test]
        public void Contains_MatchingKVP_ReturnsTrue()
        {
            _dict.Add("c", 3);
            Assert.IsTrue(_dict.Contains(new KeyValuePair<string, int>("c", 3)));
        }

        [Test]
        public void Contains_NonMatchingKVP_ReturnsFalse()
        {
            _dict.Add("c", 3);
            Assert.IsFalse(_dict.Contains(new KeyValuePair<string, int>("c", 999)));
        }

        [Test]
        public void Keys_ReturnsAllKeys()
        {
            _dict.Add("a", 1);
            _dict.Add("b", 2);

            var keys = new List<string>(_dict.Keys);

            Assert.That(keys, Is.EquivalentTo(new[] { "a", "b" }));
        }

        [Test]
        public void Values_ReturnsAllValues()
        {
            _dict.Add("a", 1);
            _dict.Add("b", 2);

            var values = new List<int>(_dict.Values);

            Assert.That(values, Is.EquivalentTo(new[] { 1, 2 }));
        }

        [Test]
        public void GetEnumerator_EnumeratesAllPairs()
        {
            _dict.Add("x", 10);
            _dict.Add("y", 20);
            var result = new List<KeyValuePair<string, int>>();

            foreach (var pair in _dict)
                result.Add(pair);

            Assert.AreEqual(2, result.Count);
        }
        
        #region Clear And Dispose
        [Test]
        public void Clear_RemovesAllEntriesAndRaisesResetEvent()
        {
            _dict.Add("a", 1);
            _events.Clear();

            _dict.Clear();

            Assert.AreEqual(0, _dict.Count);
            Assert.AreEqual(NotifyCollectionChangedAction.Reset, _events.Last.Action);
        }

        [Test]
        public void Dispose_ClearsAndRemovesHandlers()
        {
            _dict.Add("a", 1);
            _events.Dispose();

            _dict.Dispose();

            Assert.AreEqual(0, _dict.Count);
        }
        #endregion
    }
}