---
title: "Class ObservableList<T>"
sidebar_label: "ObservableList<T>"
description: "Class ObservableList<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableList\<T\> {#Aspid_Collections_Observable_ObservableList_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public class ObservableList<T> : CollectionChangedEvent<T>, IDisposable, IList<T>, ICollection<T>, IReadOnlyObservableList<T>, IObservableCollection<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

#### Type Parameters

`T` 

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionChangedEvent\<T\>](Aspid.Collections.Observable.CollectionChangedEvent-1.md) ← 
[ObservableList\<T\>](Aspid.Collections.Observable.ObservableList-1.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IList\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1),
[ICollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1),
[IReadOnlyObservableList\<T\>](Aspid.Collections.Observable.IReadOnlyObservableList-1.md),
[IObservableCollection\<T\>](Aspid.Collections.Observable.IObservableCollection-1.md),
[IReadOnlyList\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1),
[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)


#### Extension Methods

[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, IComparer\<T\>?, Predicate\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IComparer___0__System_Predicate___0__),
[CreateFilteredExtensions.CreateFiltered\<T\>\(IReadOnlyList\<T\>, Predicate\<T\>?, IComparer\<T\>?\)](Aspid.Collections.Observable.Filtered.CreateFilteredExtensions.md#Aspid_Collections_Observable_Filtered_CreateFilteredExtensions_CreateFiltered__1_System_Collections_Generic_IReadOnlyList___0__System_Predicate___0__System_Collections_Generic_IComparer___0__),
[CreateSyncExtensions.CreateSync\<T, TTo\>\(IReadOnlyObservableList\<T\>, Func\<T, TTo\>, bool\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_IReadOnlyObservableList___0__System_Func___0___1__System_Boolean_),
[CreateSyncExtensions.CreateSync\<T, TTo\>\(IReadOnlyObservableList\<T\>, Func\<T, TTo\>, Action\<TTo\>\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_IReadOnlyObservableList___0__System_Func___0___1__System_Action___1__),
[SplitEventsExtensions.SplitByEvents\<T\>\(IObservableCollection\<T\>, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int, int\>?, Action\<IReadOnlyList\<T?\>, IReadOnlyList\<T?\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_),
[ObservableListExtensions.Swap\<T\>\(ObservableList\<T\>, int, int\)](Aspid.Collections.Observable.Extensions.ObservableListExtensions.md#Aspid_Collections_Observable_Extensions_ObservableListExtensions_Swap__1_Aspid_Collections_Observable_ObservableList___0__System_Int32_System_Int32_)

## Constructors

### ObservableList\(\) {#Aspid_Collections_Observable_ObservableList_1__ctor}

```csharp
public ObservableList()
```

### ObservableList\(int\) {#Aspid_Collections_Observable_ObservableList_1__ctor_System_Int32_}

```csharp
public ObservableList(int capacity)
```

#### Parameters

`capacity` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### ObservableList\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableList_1__ctor_System_Collections_Generic_IEnumerable__0__}

```csharp
public ObservableList(IEnumerable<T> collection)
```

#### Parameters

`collection` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

## Properties

### Count {#Aspid_Collections_Observable_ObservableList_1_Count}

Gets the number of elements contained in the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public int Count { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### IsReadOnly {#Aspid_Collections_Observable_ObservableList_1_IsReadOnly}

Gets a value indicating whether the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

```csharp
public bool IsReadOnly { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### SyncRoot {#Aspid_Collections_Observable_ObservableList_1_SyncRoot}

```csharp
public object SyncRoot { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

### this\[int\] {#Aspid_Collections_Observable_ObservableList_1_Item_System_Int32_}

Gets or sets the element at the specified index.

```csharp
public T this[int index] { get; set; }
```

#### Property Value

 T

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">index</code> is not a valid index in the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1).

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The property is set and the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) is read-only.

## Methods

### Add\(T\) {#Aspid_Collections_Observable_ObservableList_1_Add__0_}

Adds an item to the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public void Add(T item)
```

#### Parameters

`item` T

The object to add to the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

### AddRange\(params T\[\]\) {#Aspid_Collections_Observable_ObservableList_1_AddRange__0___}

```csharp
public void AddRange(params T[] items)
```

#### Parameters

`items` T\[\]

### AddRange\(IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableList_1_AddRange_System_Collections_Generic_IReadOnlyList__0__}

```csharp
public void AddRange(IReadOnlyList<T> items)
```

#### Parameters

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### Clear\(\) {#Aspid_Collections_Observable_ObservableList_1_Clear}

Removes all items from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public void Clear()
```

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

### Contains\(T\) {#Aspid_Collections_Observable_ObservableList_1_Contains__0_}

Determines whether the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) contains a specific value.

```csharp
public bool Contains(T item)
```

#### Parameters

`item` T

The object to locate in the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if <code class="paramref">item</code> is found in the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### CopyTo\(T\[\], int\) {#Aspid_Collections_Observable_ObservableList_1_CopyTo__0___System_Int32_}

Copies the elements of the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) to an [`Array`](https://learn.microsoft.com/dotnet/api/system.array), starting at a particular [`Array`](https://learn.microsoft.com/dotnet/api/system.array) index.

```csharp
public void CopyTo(T[] array, int arrayIndex)
```

#### Parameters

`array` T\[\]

The one-dimensional [`Array`](https://learn.microsoft.com/dotnet/api/system.array) that is the destination of the elements copied from [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1). The [`Array`](https://learn.microsoft.com/dotnet/api/system.array) must have zero-based indexing.

`arrayIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The zero-based index in <code class="paramref">array</code> at which copying begins.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">array</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">arrayIndex</code> is less than 0.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

The number of elements in the source [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is greater than the available space from <code class="paramref">arrayIndex</code> to the end of the destination <code class="paramref">array</code>.

### Dispose\(\) {#Aspid_Collections_Observable_ObservableList_1_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public override void Dispose()
```

### ForEach\(Action\<T\>\) {#Aspid_Collections_Observable_ObservableList_1_ForEach_System_Action__0__}

```csharp
public void ForEach(Action<T> action)
```

#### Parameters

`action` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<T\>

### GetEnumerator\(\) {#Aspid_Collections_Observable_ObservableList_1_GetEnumerator}

Returns an enumerator that iterates through the collection.

```csharp
public IEnumerator<T> GetEnumerator()
```

#### Returns

 [IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator-1)\<T\>

An enumerator that can be used to iterate through the collection.

### IndexOf\(T\) {#Aspid_Collections_Observable_ObservableList_1_IndexOf__0_}

Determines the index of a specific item in the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1).

```csharp
public int IndexOf(T item)
```

#### Parameters

`item` T

The object to locate in the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1).

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The index of <code class="paramref">item</code> if found in the list; otherwise, -1.

### Insert\(int, T\) {#Aspid_Collections_Observable_ObservableList_1_Insert_System_Int32__0_}

Inserts an item to the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) at the specified index.

```csharp
public void Insert(int index, T item)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The zero-based index at which <code class="paramref">item</code> should be inserted.

`item` T

The object to insert into the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1).

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">index</code> is not a valid index in the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1).

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) is read-only.

### InsertRange\(int, params T\[\]\) {#Aspid_Collections_Observable_ObservableList_1_InsertRange_System_Int32__0___}

```csharp
public void InsertRange(int index, params T[] items)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`items` T\[\]

### InsertRange\(int, IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableList_1_InsertRange_System_Int32_System_Collections_Generic_IReadOnlyList__0__}

```csharp
public void InsertRange(int index, IReadOnlyList<T> items)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### Move\(int, int\) {#Aspid_Collections_Observable_ObservableList_1_Move_System_Int32_System_Int32_}

```csharp
public void Move(int oldIndex, int newIndex)
```

#### Parameters

`oldIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`newIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### OnAdded\(in T\) {#Aspid_Collections_Observable_ObservableList_1_OnAdded__0__}

```csharp
protected virtual void OnAdded(in T item)
```

#### Parameters

`item` T

### OnAddedRange\(in IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableList_1_OnAddedRange_System_Collections_Generic_IReadOnlyList__0___}

```csharp
protected virtual void OnAddedRange(in IReadOnlyList<T> items)
```

#### Parameters

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### OnClearing\(\) {#Aspid_Collections_Observable_ObservableList_1_OnClearing}

```csharp
protected virtual void OnClearing()
```

### OnMoved\(int, int, in T\) {#Aspid_Collections_Observable_ObservableList_1_OnMoved_System_Int32_System_Int32__0__}

```csharp
protected virtual void OnMoved(int oldIndex, int newIndex, in T item)
```

#### Parameters

`oldIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`newIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`item` T

### OnRemoved\(in T\) {#Aspid_Collections_Observable_ObservableList_1_OnRemoved__0__}

```csharp
protected virtual void OnRemoved(in T item)
```

#### Parameters

`item` T

### OnRemovedRange\(in IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableList_1_OnRemovedRange_System_Collections_Generic_IReadOnlyList__0___}

```csharp
protected virtual void OnRemovedRange(in IReadOnlyList<T> items)
```

#### Parameters

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### OnReplaced\(int, in T, in T\) {#Aspid_Collections_Observable_ObservableList_1_OnReplaced_System_Int32__0___0__}

```csharp
protected virtual void OnReplaced(int index, in T oldItem, in T newItem)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`oldItem` T

`newItem` T

### Remove\(T\) {#Aspid_Collections_Observable_ObservableList_1_Remove__0_}

Removes the first occurrence of a specific object from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public bool Remove(T item)
```

#### Parameters

`item` T

The object to remove from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if <code class="paramref">item</code> was successfully removed from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>. This method also returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> if <code class="paramref">item</code> is not found in the original [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

### RemoveAt\(int\) {#Aspid_Collections_Observable_ObservableList_1_RemoveAt_System_Int32_}

Removes the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) item at the specified index.

```csharp
public void RemoveAt(int index)
```

#### Parameters

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The zero-based index of the item to remove.

#### Exceptions

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

<code class="paramref">index</code> is not a valid index in the [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1).

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`IList<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) is read-only.

### RemoveRange\(int, int\) {#Aspid_Collections_Observable_ObservableList_1_RemoveRange_System_Int32_System_Int32_}

```csharp
public void RemoveRange(int startIndex, int count)
```

#### Parameters

`startIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`count` [int](https://learn.microsoft.com/dotnet/api/system.int32)

