---
title: "Interface IReadOnlyObservableListSync<T>"
sidebar_label: "IReadOnlyObservableListSync<T>"
description: "Interface IReadOnlyObservableListSync<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IReadOnlyObservableListSync\<T\> {#Aspid_Collections_Observable_Synchronizer_IReadOnlyObservableListSync_1}

Namespace: [Aspid.Collections.Observable.Synchronizer](Aspid.Collections.Observable.Synchronizer.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public interface IReadOnlyObservableListSync<out T> : IReadOnlyObservableList<T>, IReadOnlyList<T>, IReadOnlyObservableCollectionSync<T>, IObservableCollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IDisposable
```

#### Type Parameters

`T` 

#### Implements

[IReadOnlyObservableList\<T\>](Aspid.Collections.Observable.IReadOnlyObservableList-1.md),
[IReadOnlyList\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1),
[IReadOnlyObservableCollectionSync\<T\>](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableCollectionSync-1.md),
[IObservableCollection\<T\>](Aspid.Collections.Observable.IObservableCollection-1.md),
[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable),
[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Extension Methods

[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, IComparer\<T\>?, Predicate\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IComparer___0__System_Predicate___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, Predicate\<T\>?, IComparer\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Predicate___0__System_Collections_Generic_IComparer___0__),
[CreateSyncExtensions.CreateSync\<T, TTo\>\(IReadOnlyObservableList\<T\>, Func\<T, TTo\>, bool\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_IReadOnlyObservableList___0__System_Func___0___1__System_Boolean_),
[CreateSyncExtensions.CreateSync\<T, TTo\>\(IReadOnlyObservableList\<T\>, Func\<T, TTo\>, Action\<TTo\>\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_IReadOnlyObservableList___0__System_Func___0___1__System_Action___1__),
[SplitEventsExtensions.SplitByEvents\<T\>\(IObservableCollection\<T\>, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int, int\>?, Action\<IReadOnlyList\<T?\>, IReadOnlyList\<T?\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_)
