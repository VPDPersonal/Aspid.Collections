---
title: "Class CreateFilteredExtensions"
sidebar_label: "CreateFilteredExtensions"
description: "Class CreateFilteredExtensions — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CreateFilteredExtensions {#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions}

Namespace: [Aspid.Collections.Observable.Filtered](Aspid.Collections.Observable.Filtered.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public static class CreateFilteredExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CreateFilteredExtensions](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md)


## Methods

### CreateFiltered\<T\>\(IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__}

```csharp
public static FilteredList<T> CreateFiltered<T>(this IReadOnlyList<T> list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

#### Returns

 [FilteredList](Aspid.Collections.Observable.Filtered.FilteredList-1.md)\<T\>

#### Type Parameters

`T` 

### CreateFiltered\<T\>\(IReadOnlyList\<T\>, IComparer\<T\>?, Predicate\<T\>?\) {#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IComparer___0__System_Predicate___0__}

```csharp
public static FilteredList<T> CreateFiltered<T>(this IReadOnlyList<T> list, IComparer<T>? comparer, Predicate<T>? filter = null)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`comparer` [IComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)\<T\>?

`filter` [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate-1)\<T\>?

#### Returns

 [FilteredList](Aspid.Collections.Observable.Filtered.FilteredList-1.md)\<T\>

#### Type Parameters

`T` 

### CreateFiltered\<T\>\(IReadOnlyList\<T\>, Predicate\<T\>?, IComparer\<T\>?\) {#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Predicate___0__System_Collections_Generic_IComparer___0__}

```csharp
public static FilteredList<T> CreateFiltered<T>(this IReadOnlyList<T> list, Predicate<T>? filter, IComparer<T>? comparer = null)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`filter` [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate-1)\<T\>?

`comparer` [IComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.icomparer-1)\<T\>?

#### Returns

 [FilteredList](Aspid.Collections.Observable.Filtered.FilteredList-1.md)\<T\>

#### Type Parameters

`T` 

