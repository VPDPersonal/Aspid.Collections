---
title: "Troubleshooting"
---

## Unity reports a duplicate assembly

If the Console says `Assembly with name 'Aspid.Collections.Observable' already exists`, look for a second copy under `Assets` and in Package Manager. Keep the installation you intend to use. Preserve any local changes before removing the extra copy.

## Unity cannot resolve the test framework

If the error mentions `com.unity.test-framework@1.6.0` and your project pins `#upm/1.1.0`, switch to `#upm/1.1.1` or the stable `#upm` branch. Release 1.1.1 removed that dependency. [Release fix](https://github.com/VPDPersonal/Aspid.Collections/pull/20)

## An extension method is missing

For `CreateFiltered`, add `using Aspid.Collections.Observable.Filtered;`. For `CreateSync`, add `using Aspid.Collections.Observable.Synchronizer;`. For `SplitByEvents`, use `Aspid.Collections.Observable`. In a custom assembly, also check its reference to `Aspid.Collections.Observable`.

## A filtered view shows stale items

Observable source edits trigger updates. After editing an ordinary list or changing a field inside an existing item that affects its filter or sort order, call `Update()` on the affected filtered view. Assigning a new `Filter` or `Comparer` also rebuilds it. Read and modify the chain on the same thread.

## A subscriber still receives events

Detach the original named handler with `-=`. If you used `SplitByEvents`, keep the returned handle and dispose it when the observer goes away. For chained views, dispose the outermost view first, then its source view. The component that owns the original collection controls its lifetime.

## Reporting a problem

Include the package version, Unity or .NET version, collection type, operation that triggered the problem and the smallest reproducible example. [Open an issue](https://github.com/VPDPersonal/Aspid.Collections/issues)
