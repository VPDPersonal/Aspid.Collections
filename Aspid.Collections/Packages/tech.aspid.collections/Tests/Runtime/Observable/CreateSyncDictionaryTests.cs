using NUnit.Framework;
using System.Collections.Generic;
using Aspid.Collections.Observable.Synchronizer;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests
{
    [TestFixture]
    public sealed class CreateSyncDictionaryTests
    {
        private ObservableDictionary<string, int> _source;
        private IReadOnlyObservableDictionarySync<string, string> _sync;

        [SetUp]
        public void SetUp()
        {
            _source = new ObservableDictionary<string, int>();
            _sync = _source.CreateSync(v => v.ToString());
        }

        [TearDown]
        public void TearDown()
        {
            _sync.Dispose();
            _source.Dispose();
        }

        #region Initial State
        [Test]
        public void CreateSync_EmptySource_CreatesEmptySync()
        {
            Assert.AreEqual(0, _sync.Count);
        }

        [Test]
        public void CreateSync_NonEmptySource_CopiesAllConvertedEntries()
        {
            _sync.Dispose();
            _source.Add("a", 1);
            _source.Add("b", 2);
            _sync = _source.CreateSync(v => v.ToString());

            Assert.AreEqual(2, _sync.Count);
            Assert.AreEqual("1", _sync["a"]);
            Assert.AreEqual("2", _sync["b"]);
        }
        #endregion
        
        [Test]
        public void SourceAdd_PropagatesConvertedEntry()
        {
            _source.Add("k", 42);

            Assert.AreEqual(1, _sync.Count);
            Assert.AreEqual("42", _sync["k"]);
        }

        [Test]
        public void SourceRemove_RemovesFromSync()
        {
            _source.Add("x", 10);
            _source.Add("y", 20);

            _source.Remove("x");

            Assert.AreEqual(1, _sync.Count);
            Assert.IsFalse(_sync.ContainsKey("x"));
        }

        [Test]
        public void SourceReplace_ReplacesConvertedValueInSync()
        {
            _source.Add("k", 1);

            _source["k"] = 99;

            Assert.AreEqual("99", _sync["k"]);
        }

        [Test]
        public void SourceClear_ClearsSync()
        {
            _source.Add("a", 1);
            _source.Add("b", 2);

            _source.Clear();

            Assert.AreEqual(0, _sync.Count);
        }

        [Test]
        public void SyncKeys_MatchSourceKeys()
        {
            _source.Add("p", 1);
            _source.Add("q", 2);

            Assert.That(new List<string>(_sync.Keys), Is.EquivalentTo(new[] { "p", "q" }));
        }
        
        #region Dispose
        [Test]
        public void Dispose_StopsSynchronization()
        {
            _source.Add("a", 1);
            _sync.Dispose();
            var countBefore = _sync.Count;

            _source.Add("b", 2);

            Assert.AreEqual(countBefore, _sync.Count);
        }

        [Test]
        public void Dispose_IsDisposableTrue_DisposesConvertedValues()
        {
            var source = new ObservableDictionary<string, int>();
            var item = new DisposableItem();
            var sync = source.CreateSync(_ => item, isDisposable: true);
            source.Add("k", 1);

            sync.Dispose();

            Assert.IsTrue(item.IsDisposed);
            source.Dispose();
        }
        #endregion
        
        #region Remove
        [Test]
        public void RemoveCallback_InvokedOnRemove()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(v => v.ToString(), removed.Add);
            _source.Add("k", 55);

            _source.Remove("k");

            Assert.AreEqual(1, removed.Count);
            Assert.AreEqual("55", removed[0]);
            syncWithCb.Dispose();
        }

        [Test]
        public void RemoveCallback_InvokedOnClear()
        {
            var removed = new List<string>();
            var syncWithCb = _source.CreateSync(v => v.ToString(), removed.Add);
            _source.Add("a", 1);
            _source.Add("b", 2);

            _source.Clear();

            Assert.AreEqual(2, removed.Count);
            syncWithCb.Dispose();
        }
        #endregion

        #region Custom Comparer
        [Test]
        public void CustomComparer_SourceReplaceByEquivalentKey_DoesNotDuplicate()
        {
            var source = new ObservableDictionary<string, int>(System.StringComparer.OrdinalIgnoreCase) { ["Key"] = 1 };
            using var sync = source.CreateSync(v => v.ToString());

            source["KEY"] = 2;

            Assert.AreEqual(1, sync.Count);
            Assert.AreEqual("2", sync["Key"]);
            source.Dispose();
        }

        [Test]
        public void CustomComparer_SourceRemoveByEquivalentKey_RemovesEntry()
        {
            var source = new ObservableDictionary<string, int>(System.StringComparer.OrdinalIgnoreCase) { ["Key"] = 1 };
            var removed = new List<string>();
            using var sync = source.CreateSync(v => v.ToString(), remove: removed.Add);

            source.Remove("KEY");

            Assert.AreEqual(0, sync.Count);
            CollectionAssert.AreEqual(new[] { "1" }, removed);
            source.Dispose();
        }
        #endregion

        #region Replace Converted Values
        [Test]
        public void SourceReplace_ConverterReturnsSameInstance_DoesNotDisposeLiveItem()
        {
            var cached = new DisposableItem();
            var source = new ObservableDictionary<string, int> { ["k"] = 1 };
            var sync = source.CreateSync(_ => cached, isDisposable: true);

            source["k"] = 2;

            Assert.AreSame(cached, sync["k"]);
            Assert.IsFalse(cached.IsDisposed);
            sync.Dispose();
            source.Dispose();
        }

        [Test]
        public void SourceReplace_ConverterReturnsNullAgain_InvokesRemoved()
        {
            using var source = new ObservableDictionary<string, int> { ["k"] = 1 };
            var removed = new List<int?>();
            using var sync = source.CreateSync(_ => (int?)null, remove: removed.Add);

            source["k"] = 2;

            Assert.AreEqual(1, removed.Count);
            Assert.IsNull(removed[0]);
        }
        #endregion

        [Test]
        public void CustomSource_BatchChanges_KeepKeysValuesAndRemovalCallbacksInSync()
        {
            using var source = new DictionarySource();
            var released = new List<string>();
            using var sync = source.CreateSync(value => $"value-{value}", released.Add);
            var first = new KeyValuePair<string, int>("a", 1);
            var second = new KeyValuePair<string, int>("b", 2);
            var added = new[] { first, second };

            source.Items.Add(first.Key, first.Value);
            source.Items.Add(second.Key, second.Value);
            source.Publish(NotifyCollectionChangedEventArgs<KeyValuePair<string, int>>.Add(added, -1));

            CollectionAssert.AreEquivalent(new[] { "a", "b" }, sync.Keys);
            Assert.That(sync["a"], Is.EqualTo("value-1"));
            Assert.That(sync["b"], Is.EqualTo("value-2"));
            Assert.That(released, Is.Empty);

            var replaced = new[] { new KeyValuePair<string, int>("b", 20), new KeyValuePair<string, int>("a", 10) };
            source.Items["a"] = 10;
            source.Items["b"] = 20;
            source.Publish(NotifyCollectionChangedEventArgs<KeyValuePair<string, int>>.Replace(new[] { second, first }, replaced, -1));

            Assert.That(sync["a"], Is.EqualTo("value-10"));
            Assert.That(sync["b"], Is.EqualTo("value-20"));
            CollectionAssert.AreEqual(new[] { "value-2", "value-1" }, released);

            source.Items.Clear();
            source.Publish(NotifyCollectionChangedEventArgs<KeyValuePair<string, int>>.Remove(replaced, -1));

            Assert.That(sync, Is.Empty);
            CollectionAssert.AreEqual(new[] { "value-2", "value-1", "value-20", "value-10" }, released);
        }

        [Test]
        public void SourceReplaceAndRemove_DisposePreviousValuesExactlyOnce()
        {
            _source.Add("key", 1);
            var oldValue = new DisposableItem();
            var newValue = new DisposableItem();
            using var sync = _source.CreateSync(value => value == 1 ? oldValue : newValue, isDisposable: true);

            _source["key"] = 2;

            Assert.That(oldValue.DisposeCount, Is.EqualTo(1));
            Assert.That(newValue.DisposeCount, Is.Zero);
            Assert.That(sync["key"], Is.SameAs(newValue));

            _source.Remove("key");
            sync.Dispose();

            Assert.That(sync, Is.Empty);
            Assert.That(oldValue.DisposeCount, Is.EqualTo(1));
            Assert.That(newValue.DisposeCount, Is.EqualTo(1));
        }

        private sealed class DictionarySource : CollectionChangedEvent<KeyValuePair<string, int>>, IReadOnlyObservableDictionary<string, int>
        {
            public Dictionary<string, int> Items { get; } = new();
            public int Count => Items.Count;
            public object SyncRoot { get; } = new();
            public IEnumerable<string> Keys => Items.Keys;
            public IEnumerable<int> Values => Items.Values;
            public int this[string key] => Items[key];

            public bool ContainsKey(string key) => Items.ContainsKey(key);

            public bool TryGetValue(string key, out int value) => Items.TryGetValue(key, out value);

            public void Publish(INotifyCollectionChangedEventArgs<KeyValuePair<string, int>> args) => Invoke(args);

            public IEnumerator<KeyValuePair<string, int>> GetEnumerator() => Items.GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
