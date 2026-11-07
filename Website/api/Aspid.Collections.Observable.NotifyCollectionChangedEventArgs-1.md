---
title: "Struct NotifyCollectionChangedEventArgs<T>"
sidebar_label: "NotifyCollectionChangedEventArgs<T>"
description: "Struct NotifyCollectionChangedEventArgs<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Struct NotifyCollectionChangedEventArgs\<T\> {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public readonly struct NotifyCollectionChangedEventArgs<T> : INotifyCollectionChangedEventArgs<T>
```

#### Type Parameters

`T` 

#### Implements

[INotifyCollectionChangedEventArgs\<T\>](Aspid.Collections.Observable.INotifyCollectionChangedEventArgs-1.md)


## Properties

### Action {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Action}

```csharp
public NotifyCollectionChangedAction Action { get; }
```

#### Property Value

 [NotifyCollectionChangedAction](https://learn.microsoft.com/dotnet/api/system.collections.specialized.notifycollectionchangedaction)

### IsSingleItem {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_IsSingleItem}

```csharp
public bool IsSingleItem { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### NewItem {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_NewItem}

```csharp
public T? NewItem { get; }
```

#### Property Value

 T?

### NewItems {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_NewItems}

```csharp
public IReadOnlyList<T>? NewItems { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>?

### NewStartingIndex {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_NewStartingIndex}

```csharp
public int NewStartingIndex { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### OldItem {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_OldItem}

```csharp
public T? OldItem { get; }
```

#### Property Value

 T?

### OldItems {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_OldItems}

```csharp
public IReadOnlyList<T>? OldItems { get; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>?

### OldStartingIndex {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_OldStartingIndex}

```csharp
public int OldStartingIndex { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### Add\(T, int\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Add__0_System_Int32_}

```csharp
public static NotifyCollectionChangedEventArgs<T> Add(T newItem, int newStartingIndex)
```

#### Parameters

`newItem` T

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

### Add\(IReadOnlyList\<T\>, int\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Add_System_Collections_Generic_IReadOnlyList__0__System_Int32_}

```csharp
public static NotifyCollectionChangedEventArgs<T> Add(IReadOnlyList<T> newItems, int newStartingIndex)
```

#### Parameters

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

### Move\(T, int, int\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Move__0_System_Int32_System_Int32_}

```csharp
public static NotifyCollectionChangedEventArgs<T> Move(T changedItem, int oldStartingIndex, int newStartingIndex)
```

#### Parameters

`changedItem` T

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`newStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

### Remove\(T, int\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Remove__0_System_Int32_}

```csharp
public static NotifyCollectionChangedEventArgs<T> Remove(T oldItem, int oldStartingIndex)
```

#### Parameters

`oldItem` T

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

### Remove\(IReadOnlyList\<T\>, int\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Remove_System_Collections_Generic_IReadOnlyList__0__System_Int32_}

```csharp
public static NotifyCollectionChangedEventArgs<T> Remove(IReadOnlyList<T> oldItems, int oldStartingIndex)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`oldStartingIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

### Replace\(T, T, int\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Replace__0__0_System_Int32_}

```csharp
public static NotifyCollectionChangedEventArgs<T> Replace(T oldItem, T newItem, int index)
```

#### Parameters

`oldItem` T

`newItem` T

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

### Replace\(IReadOnlyList\<T\>, IReadOnlyList\<T\>, int\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Replace_System_Collections_Generic_IReadOnlyList__0__System_Collections_Generic_IReadOnlyList__0__System_Int32_}

```csharp
public static NotifyCollectionChangedEventArgs<T> Replace(IReadOnlyList<T> oldItems, IReadOnlyList<T> newItems, int index)
```

#### Parameters

`oldItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`newItems` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T\>

`index` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

### Reset\(\) {#Aspid_Collections_Observable_NotifyCollectionChangedEventArgs_1_Reset}

```csharp
public static NotifyCollectionChangedEventArgs<T> Reset()
```

#### Returns

 [NotifyCollectionChangedEventArgs](Aspid.Collections.Observable.NotifyCollectionChangedEventArgs-1.md)\<T\>

