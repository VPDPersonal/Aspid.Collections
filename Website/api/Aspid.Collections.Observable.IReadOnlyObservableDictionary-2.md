---
title: "Interface IReadOnlyObservableDictionary<TKey, TValue>"
sidebar_label: "IReadOnlyObservableDictionary<TKey, TValue>"
description: "Interface IReadOnlyObservableDictionary<TKey, TValue> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IReadOnlyObservableDictionary\<TKey, TValue\> {#Aspid_Collections_Observable_IReadOnlyObservableDictionary_2}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public interface IReadOnlyObservableDictionary<TKey, TValue> : IObservableCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
```

#### Type Parameters

`TKey` 

`TValue` 

#### Implements

[IObservableCollection\<KeyValuePair\<TKey, TValue\>\>](Aspid.Collections.Observable.IObservableCollection-1.md),
[IReadOnlyDictionary\<TKey, TValue\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary-2),
[IReadOnlyCollection\<KeyValuePair\<TKey, TValue\>\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<KeyValuePair\<TKey, TValue\>\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)

#### Extension Methods

[CreateSyncExtensions.CreateSync\<TKey, TValue, TTo\>\(IReadOnlyObservableDictionary\<TKey, TValue\>, Func\<TValue, TTo\>, bool\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__3_Aspid_Collections_Observable_IReadOnlyObservableDictionary___0___1__System_Func___1___2__System_Boolean_),
[CreateSyncExtensions.CreateSync\<TKey, TValue, TTo\>\(IReadOnlyObservableDictionary\<TKey, TValue\>, Func\<TValue, TTo\>, Action\<TTo\>\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__3_Aspid_Collections_Observable_IReadOnlyObservableDictionary___0___1__System_Func___1___2__System_Action___2__),
[SplitEventsExtensions.SplitByEvents\<KeyValuePair\<TKey, TValue\>\>\(IObservableCollection\<KeyValuePair\<TKey, TValue\>\>, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int\>?, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int\>?, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int, int\>?, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_)
