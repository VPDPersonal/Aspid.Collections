---
title: "Interface INotifyCollectionChangedEventArgs<T>"
sidebar_label: "INotifyCollectionChangedEventArgs<T>"
description: "Interface INotifyCollectionChangedEventArgs<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface INotifyCollectionChangedEventArgs\<T\> {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public interface INotifyCollectionChangedEventArgs<out T>
```

#### Type Parameters

`T` 

## Properties

### Action {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_Action}

```csharp
NotifyCollectionChangedAction Action { get; }
```

#### Property Value

 [NotifyCollectionChangedAction](https://learn.microsoft.com/dotnet/api/system.collections.specialized.notifycollectionchangedaction)

### IsSingleItem {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_IsSingleItem}

```csharp
bool IsSingleItem { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### NewItem {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_NewItem}

```csharp
T? NewItem { get; }
```

#### Property Value

 T?

### NewItems {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_NewItems}

```csharp
IReadOnlyList<out T>? NewItems { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>?

### NewStartingIndex {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_NewStartingIndex}

```csharp
int NewStartingIndex { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### OldItem {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_OldItem}

```csharp
T? OldItem { get; }
```

#### Property Value

 T?

### OldItems {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_OldItems}

```csharp
IReadOnlyList<out T>? OldItems { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>?

### OldStartingIndex {#Aspid_Collections_Observable_INotifyCollectionChangedEventArgs_1_OldStartingIndex}

```csharp
int OldStartingIndex { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

