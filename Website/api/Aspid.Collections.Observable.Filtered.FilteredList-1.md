---
title: "Class FilteredList<T>"
sidebar_label: "FilteredList<T>"
description: "Class FilteredList<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class FilteredList\<T\> {#Aspid_Collections_Observable_Filtered_FilteredList_1}

Namespace: [Aspid.Collections.Observable.Filtered](Aspid.Collections.Observable.Filtered.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public sealed class FilteredList<T> : IReadOnlyFilteredList<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IDisposable
```

#### Type Parameters

`T` 

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[FilteredList\<T\>](Aspid.Collections.Observable.Filtered.FilteredList-1.md)

#### Implements

[IReadOnlyFilteredList\<T\>](Aspid.Collections.Observable.Filtered.IReadOnlyFilteredList-1.md),
[IReadOnlyList\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1),
[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable),
[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)


#### Extension Methods

[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, IComparer\<T\>?, Predicate\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IComparer___0__System_Predicate___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, Predicate\<T\>?, IComparer\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Predicate___0__System_Collections_Generic_IComparer___0__)

## Constructors

### FilteredList\(IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_Filtered_FilteredList_1__ctor_System_Collections_Generic_IReadOnlyList__0__}

```csharp
public FilteredList(IReadOnlyList<T> list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### FilteredList\(IReadOnlyList\<T\>, IComparer\<T\>?, Predicate\<T\>?\) {#Aspid_Collections_Observable_Filtered_FilteredList_1__ctor_System_Collections_Generic_IReadOnlyList__0__System_Collections_Generic_IComparer__0__System_Predicate__0__}

```csharp
public FilteredList(IReadOnlyList<T> list, IComparer<T>? comparer, Predicate<T>? filter = null)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`comparer` [IComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)\<T\>?

`filter` [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate-1)\<T\>?

### FilteredList\(IReadOnlyList\<T\>, Predicate\<T\>?, IComparer\<T\>?\) {#Aspid_Collections_Observable_Filtered_FilteredList_1__ctor_System_Collections_Generic_IReadOnlyList__0__System_Predicate__0__System_Collections_Generic_IComparer__0__}

```csharp
public FilteredList(IReadOnlyList<T> list, Predicate<T>? filter, IComparer<T>? comparer = null)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`filter` [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate-1)\<T\>?

`comparer` [IComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)\<T\>?

## Properties

### Comparer {#Aspid_Collections_Observable_Filtered_FilteredList_1_Comparer}

```csharp
public IComparer<T>? Comparer { get; set; }
```

#### Property Value

 [IComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)\<T\>?

### Count {#Aspid_Collections_Observable_Filtered_FilteredList_1_Count}

Gets the number of elements in the collection.

```csharp
public int Count { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### Filter {#Aspid_Collections_Observable_Filtered_FilteredList_1_Filter}

```csharp
public Predicate<T>? Filter { get; set; }
```

#### Property Value

 [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate-1)\<T\>?

### this\[int\] {#Aspid_Collections_Observable_Filtered_FilteredList_1_Item_System_Int32_}

Gets the element at the specified index in the read-only list.

```csharp
public T this[int index] { get; }
```

#### Property Value

 T

## Methods

### Dispose\(\) {#Aspid_Collections_Observable_Filtered_FilteredList_1_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public void Dispose()
```

### GetEnumerator\(\) {#Aspid_Collections_Observable_Filtered_FilteredList_1_GetEnumerator}

Returns an enumerator that iterates through the collection.

```csharp
public IEnumerator<T> GetEnumerator()
```

#### Returns

 [IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator-1)\<T\>

An enumerator that can be used to iterate through the collection.

### Update\(\) {#Aspid_Collections_Observable_Filtered_FilteredList_1_Update}

```csharp
public void Update()
```

### CollectionChanged {#Aspid_Collections_Observable_Filtered_FilteredList_1_CollectionChanged}

```csharp
public event Action? CollectionChanged
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action)?

