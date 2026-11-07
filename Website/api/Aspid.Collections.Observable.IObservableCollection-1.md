---
title: "Interface IObservableCollection<T>"
sidebar_label: "IObservableCollection<T>"
description: "Interface IObservableCollection<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IObservableCollection\<T\> {#Aspid_Collections_Observable_IObservableCollection_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public interface IObservableCollection<out T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

#### Type Parameters

`T` 

#### Implements

[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)

#### Extension Methods

[SplitEventsExtensions.SplitByEvents\<T\>\(IObservableCollection\<T\>, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int, int\>?, Action\<IReadOnlyList\<T?\>, IReadOnlyList\<T?\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_)

## Properties

### SyncRoot {#Aspid_Collections_Observable_IObservableCollection_1_SyncRoot}

```csharp
object SyncRoot { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

### CollectionChanged {#Aspid_Collections_Observable_IObservableCollection_1_CollectionChanged}

```csharp
event NotifyCollectionChangedEventHandler<out T>? CollectionChanged
```

#### Event Type

 [NotifyCollectionChangedEventHandler](Aspid.Collections.Observable.NotifyCollectionChangedEventHandler-1.md)\<T\>?

