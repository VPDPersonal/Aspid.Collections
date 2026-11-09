---
title: "Class ObservableStack<T>"
sidebar_label: "ObservableStack<T>"
description: "Class ObservableStack<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableStack\<T\> {#Aspid_Collections_Observable_ObservableStack_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public class ObservableStack<T> : CollectionChangedEvent<T>, IDisposable, IObservableCollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

#### Type Parameters

`T` 

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionChangedEvent\<T\>](Aspid.Collections.Observable.CollectionChangedEvent-1.md) ← 
[ObservableStack\<T\>](Aspid.Collections.Observable.ObservableStack-1.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IObservableCollection\<T\>](Aspid.Collections.Observable.IObservableCollection-1.md),
[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)


#### Extension Methods

[CreateSyncExtensions.CreateSync\<T, TTo\>\(ObservableStack\<T\>, Func\<T, TTo\>, bool\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableStack___0__System_Func___0___1__System_Boolean_),
[CreateSyncExtensions.CreateSync\<T, TTo\>\(ObservableStack\<T\>, Func\<T, TTo\>, Action\<TTo\>\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableStack___0__System_Func___0___1__System_Action___1__),
[SplitEventsExtensions.SplitByEvents\<T\>\(IObservableCollection\<T\>, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int, int\>?, Action\<IReadOnlyList\<T?\>, IReadOnlyList\<T?\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_)

## Constructors

### ObservableStack\(\) {#Aspid_Collections_Observable_ObservableStack_1__ctor}

```csharp
public ObservableStack()
```

### ObservableStack\(int\) {#Aspid_Collections_Observable_ObservableStack_1__ctor_System_Int32_}

```csharp
public ObservableStack(int capacity)
```

#### Parameters

`capacity` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### ObservableStack\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableStack_1__ctor_System_Collections_Generic_IEnumerable__0__}

```csharp
public ObservableStack(IEnumerable<T> collection)
```

#### Parameters

`collection` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

## Properties

### Count {#Aspid_Collections_Observable_ObservableStack_1_Count}

Gets the number of elements in the collection.

```csharp
public int Count { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### SyncRoot {#Aspid_Collections_Observable_ObservableStack_1_SyncRoot}

```csharp
public object SyncRoot { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

## Methods

### Clear\(\) {#Aspid_Collections_Observable_ObservableStack_1_Clear}

```csharp
public void Clear()
```

### Dispose\(\) {#Aspid_Collections_Observable_ObservableStack_1_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public override void Dispose()
```

### GetEnumerator\(\) {#Aspid_Collections_Observable_ObservableStack_1_GetEnumerator}

Returns an enumerator that iterates through the collection.

```csharp
public IEnumerator<T> GetEnumerator()
```

#### Returns

 [IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator-1)\<T\>

An enumerator that can be used to iterate through the collection.

### OnClearing\(\) {#Aspid_Collections_Observable_ObservableStack_1_OnClearing}

```csharp
protected virtual void OnClearing()
```

### OnPopped\(T\) {#Aspid_Collections_Observable_ObservableStack_1_OnPopped__0_}

```csharp
protected virtual void OnPopped(T item)
```

#### Parameters

`item` T

### OnPoppedRange\(in IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableStack_1_OnPoppedRange_System_Collections_Generic_IReadOnlyList__0___}

```csharp
protected virtual void OnPoppedRange(in IReadOnlyList<T> dest)
```

#### Parameters

`dest` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### OnPushed\(T\) {#Aspid_Collections_Observable_ObservableStack_1_OnPushed__0_}

```csharp
protected virtual void OnPushed(T item)
```

#### Parameters

`item` T

### OnPushedRange\(in IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableStack_1_OnPushedRange_System_Collections_Generic_IReadOnlyList__0___}

```csharp
protected virtual void OnPushedRange(in IReadOnlyList<T> items)
```

#### Parameters

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### Peek\(\) {#Aspid_Collections_Observable_ObservableStack_1_Peek}

```csharp
public T Peek()
```

#### Returns

 T

### Pop\(\) {#Aspid_Collections_Observable_ObservableStack_1_Pop}

```csharp
public T Pop()
```

#### Returns

 T

### PopRange\(T\[\]\) {#Aspid_Collections_Observable_ObservableStack_1_PopRange__0___}

```csharp
public void PopRange(T[] dest)
```

#### Parameters

`dest` T\[\]

### Push\(T\) {#Aspid_Collections_Observable_ObservableStack_1_Push__0_}

```csharp
public void Push(T item)
```

#### Parameters

`item` T

### PushRange\(T\[\]\) {#Aspid_Collections_Observable_ObservableStack_1_PushRange__0___}

```csharp
public void PushRange(T[] items)
```

#### Parameters

`items` T\[\]

### PushRange\(IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableStack_1_PushRange_System_Collections_Generic_IReadOnlyList__0__}

```csharp
public void PushRange(IReadOnlyList<T> items)
```

#### Parameters

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### ToArray\(\) {#Aspid_Collections_Observable_ObservableStack_1_ToArray}

```csharp
public T[] ToArray()
```

#### Returns

 T\[\]

### TrimExcess\(\) {#Aspid_Collections_Observable_ObservableStack_1_TrimExcess}

```csharp
public void TrimExcess()
```

### TryPeek\(out T\) {#Aspid_Collections_Observable_ObservableStack_1_TryPeek__0__}

```csharp
public bool TryPeek(out T result)
```

#### Parameters

`result` T

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### TryPop\(out T\) {#Aspid_Collections_Observable_ObservableStack_1_TryPop__0__}

```csharp
public bool TryPop(out T result)
```

#### Parameters

`result` T

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

