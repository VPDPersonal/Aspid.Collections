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
    }
}
