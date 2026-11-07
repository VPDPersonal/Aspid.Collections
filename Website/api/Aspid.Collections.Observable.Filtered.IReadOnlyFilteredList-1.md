---
title: "Interface IReadOnlyFilteredList<T>"
sidebar_label: "IReadOnlyFilteredList<T>"
description: "Interface IReadOnlyFilteredList<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IReadOnlyFilteredList\<T\> {#Aspid_Collections_Observable_Filtered_IReadOnlyFilteredList_1}

Namespace: [Aspid.Collections.Observable.Filtered](Aspid.Collections.Observable.Filtered.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public interface IReadOnlyFilteredList<out T> : IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

#### Type Parameters

`T` 

#### Implements

[IReadOnlyList\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1),
[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)

#### Extension Methods

[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, IComparer\<T\>?, Predicate\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IComparer___0__System_Predicate___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, Predicate\<T\>?, IComparer\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Predicate___0__System_Collections_Generic_IComparer___0__)

### CollectionChanged {#Aspid_Collections_Observable_Filtered_IReadOnlyFilteredList_1_CollectionChanged}

```csharp
event Action? CollectionChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action)?

