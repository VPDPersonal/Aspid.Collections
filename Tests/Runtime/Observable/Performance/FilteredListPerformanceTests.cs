using System;
using NUnit.Framework;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
using Unity.PerformanceTesting;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Tests.Performance
{
    [TestFixture]
    [Category("Performance")]
    public sealed class FilteredListPerformanceTests
    {
        private const int WarmupCount = 3;
        private const int MeasurementCount = 10;

        #region Incremental Add Scaling
        // Pure incremental Add at increasing N. Tracks how the dispatcher scales on its
        // own — the full-rebuild comparison lives in a separate test at smaller N, since
        // forcing Update() per Add at N=10_000 pushes a single iteration past minutes.
        [Test, Performance]
        [TestCase(1_000)]
        [TestCase(10_000)]
        [TestCase(50_000)]
        public void Add_Incremental_Scaling(int size)
        {
            Measure.Method(() =>
                {
                    var source = new ObservableList<int>();
                    using var filtered = new FilteredList<int>(source, x => x >= 0, Comparer<int>.Default);
                    for (var i = 0; i < size; i++) source.Add(i);
                })
                .SampleGroup(new SampleGroup($"Incremental_{size}", SampleUnit.Millisecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
        }
        #endregion

        #region Incremental Add vs Full Rebuild
        // Contrasts the incremental Add path against a forced full rebuild on the same
        // workload. Kept at small N because the rebuild path is O(N² log N) here:
        // at N=2_000 a single iteration is already ~100 ms × 13 iterations ≈ 1.5 s.
        [Test, Performance]
        [TestCase(500)]
        [TestCase(2_000)]
        public void Add_Incremental_VsFullRebuild(int size)
        {
            Measure.Method(() =>
                {
                    var source = new ObservableList<int>();
                    using var filtered = new FilteredList<int>(source, x => x >= 0, Comparer<int>.Default);
                    for (var i = 0; i < size; i++) source.Add(i);
                })
                .SampleGroup(new SampleGroup($"Incremental_{size}", SampleUnit.Millisecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();

            Measure.Method(() =>
                {
                    var source = new ObservableList<int>();
                    using var filtered = new FilteredList<int>(source, x => x >= 0, Comparer<int>.Default);
                    for (var i = 0; i < size; i++)
                    {
                        source.Add(i);
                        filtered.Update();
                    }
                })
                .SampleGroup(new SampleGroup($"FullRebuildPerAdd_{size}", SampleUnit.Millisecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
        }
        #endregion

        #region FindViewPosition: Comparer vs NoComparer
        // FindViewPosition is O(log n) without a Comparer and O(n) with one. This
        // test pins the gap so a future switch to an auxiliary reverse index would
        // show up as a large win here.
        [Test, Performance]
        public void Remove_Middle_ComparerIsSlowerThanNoComparer()
        {
            const int size = 10_000;
            ObservableList<int> source = null;
            FilteredList<int> filtered = null;

            Measure.Method(() => source!.RemoveAt(size / 2))
                .SetUp(() =>
                {
                    source = new ObservableList<int>();
                    for (var i = 0; i < size; i++) source.Add(i);
                    filtered = new FilteredList<int>(source);
                })
                .CleanUp(() =>
                {
                    filtered.Dispose();
                    source.Dispose();
                })
                .SampleGroup(new SampleGroup("Remove_Middle_NoComparer", SampleUnit.Microsecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();

            Measure.Method(() => source!.RemoveAt(size / 2))
                .SetUp(() =>
                {
                    source = new ObservableList<int>();
                    for (var i = 0; i < size; i++) source.Add(i);
                    filtered = new FilteredList<int>(source, Comparer<int>.Default);
                })
                .CleanUp(() =>
                {
                    filtered.Dispose();
                    source.Dispose();
                })
                .SampleGroup(new SampleGroup("Remove_Middle_Comparer", SampleUnit.Microsecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
        }
        #endregion

        #region Batch AddRange
        // AddRange should stay roughly linear in the batch size. Non-linear growth
        // would suggest index-shift or binary-search cost is dominating.
        [Test, Performance]
        [TestCase(100)]
        [TestCase(1_000)]
        [TestCase(10_000)]
        public void AddRange_ScalesLinearlyWithBatchSize(int batchSize)
        {
            var items = new int[batchSize];
            for (var i = 0; i < batchSize; i++) items[i] = i;

            ObservableList<int> source = null;
            FilteredList<int> filtered = null;

            Measure.Method(() => source!.AddRange(items))
                .SetUp(() =>
                {
                    source = new ObservableList<int>();
                    for (var i = 0; i < 1_000; i++) source.Add(-i);
                    filtered = new FilteredList<int>(source, x => x >= 0, Comparer<int>.Default);
                })
                .CleanUp(() => filtered.Dispose())
                .SampleGroup(new SampleGroup($"AddRange_{batchSize}", SampleUnit.Microsecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
        }
        #endregion

        #region Chain Depth
        // Each FilteredList in a chain forwards notifications. A single source mutation
        // pays the dispatch cost at every level; measures the multiplier.
        [Test, Performance]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Add_ChainDepth_PropagationCost(int depth)
        {
            const int seed = 1_000;
            ObservableList<int> source = null;
            var chain = new List<FilteredList<int>>();

            Measure.Method(() => source!.Add(seed))
                .SetUp(() =>
                {
                    source = new ObservableList<int>();
                    for (var i = 0; i < seed; i++) source.Add(i);

                    chain.Clear();
                    IReadOnlyList<int> upstream = source;
                    for (var level = 0; level < depth; level++)
                    {
                        var link = new FilteredList<int>(upstream, x => x >= 0);
                        chain.Add(link);
                        upstream = link;
                    }
                })
                .CleanUp(() =>
                {
                    for (var i = chain.Count - 1; i >= 0; i--) chain[i].Dispose();
                })
                .SampleGroup(new SampleGroup($"Add_ChainDepth_{depth}", SampleUnit.Microsecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
        }
        #endregion

        #region Mixed Random Workload
        // Realistic end-to-end workload: interleaves Add/Insert/Remove/Replace/Move/
        // RemoveRange to exercise the full dispatcher path. Deterministic seed keeps
        // samples comparable across runs.
        [Test, Performance]
        public void MixedWorkload_5000Items_1000Ops()
        {
            const int initialSize = 5_000;
            const int operations = 1_000;

            ObservableList<int> source = null;
            FilteredList<int> filtered = null;

            Measure.Method(() =>
                {
                    var rng = new Random(42);
                    for (var op = 0; op < operations; op++)
                    {
                        switch (rng.Next(6))
                        {
                            case 0:
                                source!.Add(rng.Next(0, 10_000));
                                break;
                            case 1:
                                if (source!.Count > 0)
                                    source.Insert(rng.Next(source.Count), rng.Next(0, 10_000));
                                break;
                            case 2:
                                if (source!.Count > 0) source.RemoveAt(rng.Next(source.Count));
                                break;
                            case 3:
                                if (source!.Count > 0)
                                    source[rng.Next(source.Count)] = rng.Next(0, 10_000);
                                break;
                            case 4:
                                if (source!.Count >= 2)
                                    source.Move(rng.Next(source.Count), rng.Next(source.Count));
                                break;
                            case 5:
                                if (source!.Count >= 4)
                                {
                                    var start = rng.Next(source.Count - 2);
                                    source.RemoveRange(start, rng.Next(1, 4));
                                }
                                break;
                        }
                    }
                })
                .SetUp(() =>
                {
                    source = new ObservableList<int>();
                    var seedRng = new Random(7);
                    for (var i = 0; i < initialSize; i++) source.Add(seedRng.Next(0, 10_000));
                    filtered = new FilteredList<int>(source, x => x % 3 != 0, Comparer<int>.Default);
                })
                .CleanUp(() => filtered.Dispose())
                .SampleGroup(new SampleGroup("MixedWorkload_5000_1000ops", SampleUnit.Millisecond))
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .Run();
        }
        #endregion
    }
}
