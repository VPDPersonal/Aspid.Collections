---
title: "Class ObservableDictionary<TKey, TValue>"
sidebar_label: "ObservableDictionary<TKey, TValue>"
description: "Class ObservableDictionary<TKey, TValue> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableDictionary\<TKey, TValue\> {#Aspid_Collections_Observable_ObservableDictionary_2}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public class ObservableDictionary<TKey, TValue> : CollectionChangedEvent<KeyValuePair<TKey, TValue>>, IDisposable, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IReadOnlyObservableDictionary<TKey, TValue>, IObservableCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : notnull
```

#### Type Parameters

`TKey` 

`TValue` 

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionChangedEvent\<KeyValuePair\<TKey, TValue\>\>](Aspid.Collections.Observable.CollectionChangedEvent-1.md) ← 
[ObservableDictionary\<TKey, TValue\>](Aspid.Collections.Observable.ObservableDictionary-2.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IDictionary\<TKey, TValue\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2),
[ICollection\<KeyValuePair\<TKey, TValue\>\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1),
[IReadOnlyObservableDictionary\<TKey, TValue\>](Aspid.Collections.Observable.IReadOnlyObservableDictionary-2.md),
[IObservableCollection\<KeyValuePair\<TKey, TValue\>\>](Aspid.Collections.Observable.IObservableCollection-1.md),
[IReadOnlyDictionary\<TKey, TValue\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlydictionary-2),
[IReadOnlyCollection\<KeyValuePair\<TKey, TValue\>\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<KeyValuePair\<TKey, TValue\>\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)


#### Extension Methods

[CreateSyncExtensions.CreateSync\<TKey, TValue, TTo\>\(IReadOnlyObservableDictionary\<TKey, TValue\>, Func\<TValue, TTo\>, bool\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__3_Aspid_Collections_Observable_IReadOnlyObservableDictionary___0___1__System_Func___1___2__System_Boolean_),
[CreateSyncExtensions.CreateSync\<TKey, TValue, TTo\>\(IReadOnlyObservableDictionary\<TKey, TValue\>, Func\<TValue, TTo\>, Action\<TTo\>\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__3_Aspid_Collections_Observable_IReadOnlyObservableDictionary___0___1__System_Func___1___2__System_Action___2__),
[SplitEventsExtensions.SplitByEvents\<KeyValuePair\<TKey, TValue\>\>\(IObservableCollection\<KeyValuePair\<TKey, TValue\>\>, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int\>?, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int\>?, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int, int\>?, Action\<IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, IReadOnlyList\<KeyValuePair\<TKey, TValue\>\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_)

## Constructors

### ObservableDictionary\(\) {#Aspid_Collections_Observable_ObservableDictionary_2__ctor}

```csharp
public ObservableDictionary()
```

### ObservableDictionary\(int\) {#Aspid_Collections_Observable_ObservableDictionary_2__ctor_System_Int32_}

```csharp
public ObservableDictionary(int capacity)
```

#### Parameters

`capacity` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### ObservableDictionary\(IEqualityComparer\<TKey\>\) {#Aspid_Collections_Observable_ObservableDictionary_2__ctor_System_Collections_Generic_IEqualityComparer__0__}

```csharp
public ObservableDictionary(IEqualityComparer<TKey> comparer)
```

#### Parameters

`comparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<TKey\>

### ObservableDictionary\(int, IEqualityComparer\<TKey\>\) {#Aspid_Collections_Observable_ObservableDictionary_2__ctor_System_Int32_System_Collections_Generic_IEqualityComparer__0__}

```csharp
public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
```

#### Parameters

`capacity` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`comparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<TKey\>

### ObservableDictionary\(IEnumerable\<KeyValuePair\<TKey, TValue\>\>, IEqualityComparer\<TKey\>?\) {#Aspid_Collections_Observable_ObservableDictionary_2__ctor_System_Collections_Generic_IEnumerable_System_Collections_Generic_KeyValuePair__0__1___System_Collections_Generic_IEqualityComparer__0__}

```csharp
public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer = null)
```

#### Parameters

`collection` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<[KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>\>

`comparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<TKey\>?

## Properties

### Comparer {#Aspid_Collections_Observable_ObservableDictionary_2_Comparer}

```csharp
public IEqualityComparer<TKey> Comparer { get; }
```

#### Property Value

 [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer-1)\<TKey\>

### Count {#Aspid_Collections_Observable_ObservableDictionary_2_Count}

Gets the number of elements contained in the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public int Count { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### IsReadOnly {#Aspid_Collections_Observable_ObservableDictionary_2_IsReadOnly}

Gets a value indicating whether the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

```csharp
public bool IsReadOnly { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### Keys {#Aspid_Collections_Observable_ObservableDictionary_2_Keys}

Gets an enumerable collection that contains the keys in the read-only dictionary.

```csharp
public IEnumerable<TKey> Keys { get; }
```

#### Property Value

 [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<TKey\>

### SyncRoot {#Aspid_Collections_Observable_ObservableDictionary_2_SyncRoot}

```csharp
public object SyncRoot { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

### Values {#Aspid_Collections_Observable_ObservableDictionary_2_Values}

Gets an enumerable collection that contains the values in the read-only dictionary.

```csharp
public IEnumerable<TValue> Values { get; }
```

#### Property Value

 [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<TValue\>

### this\[TKey\] {#Aspid_Collections_Observable_ObservableDictionary_2_Item__0_}

Gets or sets the element with the specified key.

```csharp
public TValue this[TKey key] { get; set; }
```

#### Property Value

 TValue

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">key</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [KeyNotFoundException](https://learn.microsoft.com/dotnet/api/system.collections.generic.keynotfoundexception)

The property is retrieved and <code class="paramref">key</code> is not found.

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The property is set and the [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) is read-only.

## Methods

### Add\(TKey, TValue\) {#Aspid_Collections_Observable_ObservableDictionary_2_Add__0__1_}

Adds an element with the provided key and value to the [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2).

```csharp
public void Add(TKey key, TValue value)
```

#### Parameters

`key` TKey

The object to use as the key of the element to add.

`value` TValue

The object to use as the value of the element to add.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">key</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

An element with the same key already exists in the [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2).

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) is read-only.

### Add\(KeyValuePair\<TKey, TValue\>\) {#Aspid_Collections_Observable_ObservableDictionary_2_Add_System_Collections_Generic_KeyValuePair__0__1__}

Adds an item to the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public void Add(KeyValuePair<TKey, TValue> item)
```

#### Parameters

`item` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

The object to add to the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

### Clear\(\) {#Aspid_Collections_Observable_ObservableDictionary_2_Clear}

Removes all items from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public void Clear()
```

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

### Contains\(KeyValuePair\<TKey, TValue\>\) {#Aspid_Collections_Observable_ObservableDictionary_2_Contains_System_Collections_Generic_KeyValuePair__0__1__}

Determines whether the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) contains a specific value.

```csharp
public bool Contains(KeyValuePair<TKey, TValue> item)
```

#### Parameters

`item` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

The object to locate in the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if <code class="paramref">item</code> is found in the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### ContainsKey\(TKey\) {#Aspid_Collections_Observable_ObservableDictionary_2_ContainsKey__0_}

Determines whether the [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) contains an element with the specified key.

```csharp
public bool ContainsKey(TKey key)
```

#### Parameters

`key` TKey

The key to locate in the [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2).

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) contains an element with the key; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">key</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### CopyTo\(KeyValuePair\<TKey, TValue\>\[\], int\) {#Aspid_Collections_Observable_ObservableDictionary_2_CopyTo_System_Collections_Generic_KeyValuePair__0__1____System_Int32_}

Copies the elements of the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) to an [`Array`](https://learn.microsoft.com/dotnet/api/system.array), starting at a particular [`Array`](https://learn.microsoft.com/dotnet/api/system.array) index.

```csharp
public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
```

#### Parameters

`array` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>\[\]

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

### Dispose\(\) {#Aspid_Collections_Observable_ObservableDictionary_2_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public override void Dispose()
```

### GetEnumerator\(\) {#Aspid_Collections_Observable_ObservableDictionary_2_GetEnumerator}

Returns an enumerator that iterates through the collection.

```csharp
public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
```

#### Returns

 [IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator-1)\<[KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>\>

An enumerator that can be used to iterate through the collection.

### OnAdded\(in TKey, in TValue\) {#Aspid_Collections_Observable_ObservableDictionary_2_OnAdded__0___1__}

```csharp
protected virtual void OnAdded(in TKey key, in TValue value)
```

#### Parameters

`key` TKey

`value` TValue

### OnClearing\(\) {#Aspid_Collections_Observable_ObservableDictionary_2_OnClearing}

```csharp
protected virtual void OnClearing()
```

### OnRemoved\(in TKey, in TValue\) {#Aspid_Collections_Observable_ObservableDictionary_2_OnRemoved__0___1__}

```csharp
protected virtual void OnRemoved(in TKey key, in TValue value)
```

#### Parameters

`key` TKey

`value` TValue

### OnReplaced\(in KeyValuePair\<TKey, TValue\>, in KeyValuePair\<TKey, TValue\>\) {#Aspid_Collections_Observable_ObservableDictionary_2_OnReplaced_System_Collections_Generic_KeyValuePair__0__1___System_Collections_Generic_KeyValuePair__0__1___}

```csharp
protected virtual void OnReplaced(in KeyValuePair<TKey, TValue> oldItem, in KeyValuePair<TKey, TValue> newItem)
```

#### Parameters

`oldItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

`newItem` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

### Remove\(TKey\) {#Aspid_Collections_Observable_ObservableDictionary_2_Remove__0_}

Removes the element with the specified key from the [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2).

```csharp
public bool Remove(TKey key)
```

#### Parameters

`key` TKey

The key of the element to remove.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the element is successfully removed; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.  This method also returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> if <code class="paramref">key</code> was not found in the original [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2).

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">key</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) is read-only.

### Remove\(KeyValuePair\<TKey, TValue\>\) {#Aspid_Collections_Observable_ObservableDictionary_2_Remove_System_Collections_Generic_KeyValuePair__0__1__}

Removes the first occurrence of a specific object from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

```csharp
public bool Remove(KeyValuePair<TKey, TValue> item)
```

#### Parameters

`item` [KeyValuePair](https://learn.microsoft.com/dotnet/api/system.collections.generic.keyvaluepair-2)\<TKey, TValue\>

The object to remove from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if <code class="paramref">item</code> was successfully removed from the [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1); otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>. This method also returns <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> if <code class="paramref">item</code> is not found in the original [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1).

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

The [`ICollection<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.icollection-1) is read-only.

### TryGetValue\(TKey, out TValue\) {#Aspid_Collections_Observable_ObservableDictionary_2_TryGetValue__0__1__}

Gets the value associated with the specified key.

```csharp
public bool TryGetValue(TKey key, out TValue value)
```

#### Parameters

`key` TKey

The key whose value to get.

`value` TValue

When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <code class="paramref">value</code> parameter. This parameter is passed uninitialized.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the object that implements [`IDictionary<T1, T2>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) contains an element with the specified key; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

<code class="paramref">key</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

