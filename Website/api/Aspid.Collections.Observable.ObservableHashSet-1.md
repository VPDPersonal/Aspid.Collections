---
title: "Class ObservableHashSet<T>"
sidebar_label: "ObservableHashSet<T>"
description: "Class ObservableHashSet<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableHashSet\<T\> {#Aspid_Collections_Observable_ObservableHashSet_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public class ObservableHashSet<T> : CollectionChangedEvent<T>, IDisposable, IObservableCollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable where T : notnull
```

#### Type Parameters

`T` 

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionChangedEvent\<T\>](Aspid.Collections.Observable.CollectionChangedEvent-1.md) ← 
[ObservableHashSet\<T\>](Aspid.Collections.Observable.ObservableHashSet-1.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IObservableCollection\<T\>](Aspid.Collections.Observable.IObservableCollection-1.md),
[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)


#### Extension Methods

[CreateSyncExtensions.CreateSync\<T, TTo\>\(ObservableHashSet\<T\>, Func\<T, TTo\>, bool\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableHashSet___0__System_Func___0___1__System_Boolean_),
[CreateSyncExtensions.CreateSync\<T, TTo\>\(ObservableHashSet\<T\>, Func\<T, TTo\>, Action\<TTo\>\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableHashSet___0__System_Func___0___1__System_Action___1__),
[SplitEventsExtensions.SplitByEvents\<T\>\(IObservableCollection\<T\>, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int, int\>?, Action\<IReadOnlyList\<T?\>, IReadOnlyList\<T?\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_)

## Constructors

### ObservableHashSet\(\) {#Aspid_Collections_Observable_ObservableHashSet_1__ctor}

```csharp
public ObservableHashSet()
```

### ObservableHashSet\(int\) {#Aspid_Collections_Observable_ObservableHashSet_1__ctor_System_Int32_}

```csharp
public ObservableHashSet(int capacity)
```

#### Parameters

`capacity` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### ObservableHashSet\(IEqualityComparer\<T\>?\) {#Aspid_Collections_Observable_ObservableHashSet_1__ctor_System_Collections_Generic_IEqualityComparer__0__}

```csharp
public ObservableHashSet(IEqualityComparer<T>? comparer)
```

#### Parameters

`comparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<T\>?

### ObservableHashSet\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableHashSet_1__ctor_System_Collections_Generic_IEnumerable__0__}

```csharp
public ObservableHashSet(IEnumerable<T> collection)
```

#### Parameters

`collection` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

### ObservableHashSet\(IEnumerable\<T\>, IEqualityComparer\<T\>?\) {#Aspid_Collections_Observable_ObservableHashSet_1__ctor_System_Collections_Generic_IEnumerable__0__System_Collections_Generic_IEqualityComparer__0__}

```csharp
public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
```

#### Parameters

`collection` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

`comparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<T\>?

## Properties

### Comparer {#Aspid_Collections_Observable_ObservableHashSet_1_Comparer}

```csharp
public IEqualityComparer<T> Comparer { get; }
```

#### Property Value

 [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<T\>

### Count {#Aspid_Collections_Observable_ObservableHashSet_1_Count}

Gets the number of elements in the collection.

```csharp
public int Count { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### IsReadOnly {#Aspid_Collections_Observable_ObservableHashSet_1_IsReadOnly}

```csharp
public bool IsReadOnly { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### SyncRoot {#Aspid_Collections_Observable_ObservableHashSet_1_SyncRoot}

```csharp
public object SyncRoot { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

## Methods

### Add\(T\) {#Aspid_Collections_Observable_ObservableHashSet_1_Add__0_}

```csharp
public bool Add(T item)
```

#### Parameters

`item` T

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Clear\(\) {#Aspid_Collections_Observable_ObservableHashSet_1_Clear}

```csharp
public void Clear()
```

### Contains\(T\) {#Aspid_Collections_Observable_ObservableHashSet_1_Contains__0_}

```csharp
public bool Contains(T item)
```

#### Parameters

`item` T

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Dispose\(\) {#Aspid_Collections_Observable_ObservableHashSet_1_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public override void Dispose()
```

### GetEnumerator\(\) {#Aspid_Collections_Observable_ObservableHashSet_1_GetEnumerator}

Returns an enumerator that iterates through the collection.

```csharp
public IEnumerator<T> GetEnumerator()
```

#### Returns

 [IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator-1)\<T\>

An enumerator that can be used to iterate through the collection.

### IsProperSubsetOf\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableHashSet_1_IsProperSubsetOf_System_Collections_Generic_IEnumerable__0__}

```csharp
public bool IsProperSubsetOf(IEnumerable<T> other)
```

#### Parameters

`other` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsProperSupersetOf\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableHashSet_1_IsProperSupersetOf_System_Collections_Generic_IEnumerable__0__}

```csharp
public bool IsProperSupersetOf(IEnumerable<T> other)
```

#### Parameters

`other` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsSubsetOf\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableHashSet_1_IsSubsetOf_System_Collections_Generic_IEnumerable__0__}

```csharp
public bool IsSubsetOf(IEnumerable<T> other)
```

#### Parameters

`other` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### IsSupersetOf\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableHashSet_1_IsSupersetOf_System_Collections_Generic_IEnumerable__0__}

```csharp
public bool IsSupersetOf(IEnumerable<T> other)
```

#### Parameters

`other` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### OnAdded\(T\) {#Aspid_Collections_Observable_ObservableHashSet_1_OnAdded__0_}

```csharp
protected virtual void OnAdded(T item)
```

#### Parameters

`item` T

### OnClearing\(\) {#Aspid_Collections_Observable_ObservableHashSet_1_OnClearing}

```csharp
protected virtual void OnClearing()
```

### OnRemoved\(T\) {#Aspid_Collections_Observable_ObservableHashSet_1_OnRemoved__0_}

```csharp
protected virtual void OnRemoved(T item)
```

#### Parameters

`item` T

### Overlaps\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableHashSet_1_Overlaps_System_Collections_Generic_IEnumerable__0__}

```csharp
public bool Overlaps(IEnumerable<T> other)
```

#### Parameters

`other` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Remove\(T\) {#Aspid_Collections_Observable_ObservableHashSet_1_Remove__0_}

```csharp
public bool Remove(T item)
```

#### Parameters

`item` T

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### SetEquals\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableHashSet_1_SetEquals_System_Collections_Generic_IEnumerable__0__}

```csharp
public bool SetEquals(IEnumerable<T> other)
```

#### Parameters

`other` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

