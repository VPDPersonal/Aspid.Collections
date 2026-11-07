---
title: "Class ObservableQueue<T>"
sidebar_label: "ObservableQueue<T>"
description: "Class ObservableQueue<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class ObservableQueue\<T\> {#Aspid_Collections_Observable_ObservableQueue_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public class ObservableQueue<T> : CollectionChangedEvent<T>, IDisposable, IObservableCollection<T>, IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
```

#### Type Parameters

`T` 

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionChangedEvent\<T\>](Aspid.Collections.Observable.CollectionChangedEvent-1.md) ← 
[ObservableQueue\<T\>](Aspid.Collections.Observable.ObservableQueue-1.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable),
[IObservableCollection\<T\>](Aspid.Collections.Observable.IObservableCollection-1.md),
[IReadOnlyCollection\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlycollection-1),
[IEnumerable\<T\>](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1),
[IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable)


#### Extension Methods

[CreateSyncExtensions.CreateSync\<T, TTo\>\(ObservableQueue\<T\>, Func\<T, TTo\>, bool\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableQueue___0__System_Func___0___1__System_Boolean_),
[CreateSyncExtensions.CreateSync\<T, TTo\>\(ObservableQueue\<T\>, Func\<T, TTo\>, Action\<TTo\>\)](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableQueue___0__System_Func___0___1__System_Action___1__),
[SplitEventsExtensions.SplitByEvents\<T\>\(IObservableCollection\<T\>, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int, int\>?, Action\<IReadOnlyList\<T?\>, IReadOnlyList\<T?\>, int\>?, Action?\)](Aspid.Collections.Observable.SplitEventsExtensions.md#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_)

## Constructors

### ObservableQueue\(\) {#Aspid_Collections_Observable_ObservableQueue_1__ctor}

```csharp
public ObservableQueue()
```

### ObservableQueue\(int\) {#Aspid_Collections_Observable_ObservableQueue_1__ctor_System_Int32_}

```csharp
public ObservableQueue(int capacity)
```

#### Parameters

`capacity` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### ObservableQueue\(IEnumerable\<T\>\) {#Aspid_Collections_Observable_ObservableQueue_1__ctor_System_Collections_Generic_IEnumerable__0__}

```csharp
public ObservableQueue(IEnumerable<T> collection)
```

#### Parameters

`collection` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1)\<T\>

### ObservableQueue\(Queue\<T\>\) {#Aspid_Collections_Observable_ObservableQueue_1__ctor_System_Collections_Generic_Queue__0__}

```csharp
public ObservableQueue(Queue<T> queue)
```

#### Parameters

`queue` [Queue](https://learn.microsoft.com/dotnet/api/system.collections.generic.queue-1)\<T\>

## Properties

### Count {#Aspid_Collections_Observable_ObservableQueue_1_Count}

Gets the number of elements in the collection.

```csharp
public int Count { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### SyncRoot {#Aspid_Collections_Observable_ObservableQueue_1_SyncRoot}

```csharp
public object SyncRoot { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

## Methods

### Clear\(\) {#Aspid_Collections_Observable_ObservableQueue_1_Clear}

```csharp
public void Clear()
```

### Dequeue\(\) {#Aspid_Collections_Observable_ObservableQueue_1_Dequeue}

```csharp
public T Dequeue()
```

#### Returns

 T

### DequeueRange\(in T\[\]\) {#Aspid_Collections_Observable_ObservableQueue_1_DequeueRange__0____}

```csharp
public void DequeueRange(in T[] dest)
```

#### Parameters

`dest` T\[\]

### Dispose\(\) {#Aspid_Collections_Observable_ObservableQueue_1_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public override void Dispose()
```

### Enqueue\(T\) {#Aspid_Collections_Observable_ObservableQueue_1_Enqueue__0_}

```csharp
public void Enqueue(T item)
```

#### Parameters

`item` T

### EnqueueRange\(T\[\]\) {#Aspid_Collections_Observable_ObservableQueue_1_EnqueueRange__0___}

```csharp
public void EnqueueRange(T[] items)
```

#### Parameters

`items` T\[\]

### EnqueueRange\(IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableQueue_1_EnqueueRange_System_Collections_Generic_IReadOnlyList__0__}

```csharp
public void EnqueueRange(IReadOnlyList<T> items)
```

#### Parameters

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### GetEnumerator\(\) {#Aspid_Collections_Observable_ObservableQueue_1_GetEnumerator}

Returns an enumerator that iterates through the collection.

```csharp
public IEnumerator<T> GetEnumerator()
```

#### Returns

 [IEnumerator](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerator-1)\<T\>

An enumerator that can be used to iterate through the collection.

### OnClearing\(\) {#Aspid_Collections_Observable_ObservableQueue_1_OnClearing}

```csharp
protected virtual void OnClearing()
```

### OnDequeued\(T\) {#Aspid_Collections_Observable_ObservableQueue_1_OnDequeued__0_}

```csharp
protected virtual void OnDequeued(T item)
```

#### Parameters

`item` T

### OnDequeuedRange\(in IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableQueue_1_OnDequeuedRange_System_Collections_Generic_IReadOnlyList__0___}

```csharp
protected virtual void OnDequeuedRange(in IReadOnlyList<T> dest)
```

#### Parameters

`dest` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### OnEnqueued\(T\) {#Aspid_Collections_Observable_ObservableQueue_1_OnEnqueued__0_}

```csharp
protected virtual void OnEnqueued(T item)
```

#### Parameters

`item` T

### OnEnqueuedRange\(in IReadOnlyList\<T\>\) {#Aspid_Collections_Observable_ObservableQueue_1_OnEnqueuedRange_System_Collections_Generic_IReadOnlyList__0___}

```csharp
protected virtual void OnEnqueuedRange(in IReadOnlyList<T> items)
```

#### Parameters

`items` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

### Peek\(\) {#Aspid_Collections_Observable_ObservableQueue_1_Peek}

```csharp
public T Peek()
```

#### Returns

 T

### ToArray\(\) {#Aspid_Collections_Observable_ObservableQueue_1_ToArray}

```csharp
public T[] ToArray()
```

#### Returns

 T\[\]

### TrimExcess\(\) {#Aspid_Collections_Observable_ObservableQueue_1_TrimExcess}

```csharp
public void TrimExcess()
```

### TryDequeue\(out T\) {#Aspid_Collections_Observable_ObservableQueue_1_TryDequeue__0__}

```csharp
public bool TryDequeue(out T result)
```

#### Parameters

`result` T

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### TryPeek\(out T\) {#Aspid_Collections_Observable_ObservableQueue_1_TryPeek__0__}

```csharp
public bool TryPeek(out T result)
```

#### Parameters

`result` T

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

