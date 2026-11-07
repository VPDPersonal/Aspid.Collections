---
title: "Class CollectionChangedEvent<T>"
sidebar_label: "CollectionChangedEvent<T>"
description: "Class CollectionChangedEvent<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CollectionChangedEvent\<T\> {#Aspid_Collections_Observable_CollectionChangedEvent_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public abstract class CollectionChangedEvent<T> : IDisposable
```

#### Type Parameters

`T` 

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectionChangedEvent\<T\>](Aspid.Collections.Observable.CollectionChangedEvent-1.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)


## Methods

### Dispose\(\) {#Aspid_Collections_Observable_CollectionChangedEvent_1_Dispose}

Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

```csharp
public virtual void Dispose()
```

### Invoke\(INotifyCollectionChangedEventArgs\<T\>\) {#Aspid_Collections_Observable_CollectionChangedEvent_1_Invoke_Aspid_Collections_Observable_INotifyCollectionChangedEventArgs__0__}

```csharp
protected void Invoke(INotifyCollectionChangedEventArgs<T> e)
```

#### Parameters

`e` [INotifyCollectionChangedEventArgs](Aspid.Collections.Observable.INotifyCollectionChangedEventArgs-1.md)\<T\>

### CollectionChanged {#Aspid_Collections_Observable_CollectionChangedEvent_1_CollectionChanged}

```csharp
public event NotifyCollectionChangedEventHandler<T>? CollectionChanged
```

#### Event Type

 [NotifyCollectionChangedEventHandler](Aspid.Collections.Observable.NotifyCollectionChangedEventHandler-1.md)\<T\>?

