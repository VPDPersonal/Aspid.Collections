---
title: "Class CreateSyncExtensions"
sidebar_label: "CreateSyncExtensions"
description: "Class CreateSyncExtensions — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class CreateSyncExtensions {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions}

Namespace: [Aspid.Collections.Observable.Synchronizer](Aspid.Collections.Observable.Synchronizer.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public static class CreateSyncExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CreateSyncExtensions](Aspid.Collections.Observable.Synchronizer.CreateSyncExtensions.md)


## Methods

### CreateSync\<TFrom, TTo\>\(IReadOnlyObservableList\<TFrom\>, Func\<TFrom, TTo\>, bool\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_IReadOnlyObservableList___0__System_Func___0___1__System_Boolean_}

```csharp
public static IReadOnlyObservableListSync<TTo> CreateSync<TFrom, TTo>(this IReadOnlyObservableList<TFrom> fromList, Func<TFrom, TTo> converter, bool isDisposable = false)
```

#### Parameters

`fromList` [IReadOnlyObservableList](Aspid.Collections.Observable.IReadOnlyObservableList-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`isDisposable` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [IReadOnlyObservableListSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableListSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TFrom, TTo\>\(IReadOnlyObservableList\<TFrom\>, Func\<TFrom, TTo\>, Action\<TTo\>\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_IReadOnlyObservableList___0__System_Func___0___1__System_Action___1__}

```csharp
public static IReadOnlyObservableListSync<TTo> CreateSync<TFrom, TTo>(this IReadOnlyObservableList<TFrom> fromList, Func<TFrom, TTo> converter, Action<TTo> removed)
```

#### Parameters

`fromList` [IReadOnlyObservableList](Aspid.Collections.Observable.IReadOnlyObservableList-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`removed` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<TTo\>

#### Returns

 [IReadOnlyObservableListSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableListSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TFrom, TTo\>\(ObservableQueue\<TFrom\>, Func\<TFrom, TTo\>, bool\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableQueue___0__System_Func___0___1__System_Boolean_}

```csharp
public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(this ObservableQueue<TFrom> fromQueue, Func<TFrom, TTo> converter, bool isDisposable = false)
```

#### Parameters

`fromQueue` [ObservableQueue](Aspid.Collections.Observable.ObservableQueue-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`isDisposable` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [IReadOnlyObservableCollectionSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableCollectionSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TFrom, TTo\>\(ObservableQueue\<TFrom\>, Func\<TFrom, TTo\>, Action\<TTo\>\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableQueue___0__System_Func___0___1__System_Action___1__}

```csharp
public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(this ObservableQueue<TFrom> fromQueue, Func<TFrom, TTo> converter, Action<TTo> remove)
```

#### Parameters

`fromQueue` [ObservableQueue](Aspid.Collections.Observable.ObservableQueue-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`remove` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<TTo\>

#### Returns

 [IReadOnlyObservableCollectionSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableCollectionSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TFrom, TTo\>\(ObservableStack\<TFrom\>, Func\<TFrom, TTo\>, bool\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableStack___0__System_Func___0___1__System_Boolean_}

```csharp
public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(this ObservableStack<TFrom> fromStack, Func<TFrom, TTo> converter, bool isDisposable = false)
```

#### Parameters

`fromStack` [ObservableStack](Aspid.Collections.Observable.ObservableStack-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`isDisposable` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [IReadOnlyObservableCollectionSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableCollectionSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TFrom, TTo\>\(ObservableStack\<TFrom\>, Func\<TFrom, TTo\>, Action\<TTo\>\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableStack___0__System_Func___0___1__System_Action___1__}

```csharp
public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(this ObservableStack<TFrom> fromStack, Func<TFrom, TTo> converter, Action<TTo> remove)
```

#### Parameters

`fromStack` [ObservableStack](Aspid.Collections.Observable.ObservableStack-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`remove` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<TTo\>

#### Returns

 [IReadOnlyObservableCollectionSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableCollectionSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TFrom, TTo\>\(ObservableHashSet\<TFrom\>, Func\<TFrom, TTo\>, bool\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableHashSet___0__System_Func___0___1__System_Boolean_}

```csharp
public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(this ObservableHashSet<TFrom> fromHashSet, Func<TFrom, TTo> converter, bool isDisposable = false) where TFrom : notnull where TTo : notnull
```

#### Parameters

`fromHashSet` [ObservableHashSet](Aspid.Collections.Observable.ObservableHashSet-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`isDisposable` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [IReadOnlyObservableCollectionSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableCollectionSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TFrom, TTo\>\(ObservableHashSet\<TFrom\>, Func\<TFrom, TTo\>, Action\<TTo\>\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__2_Aspid_Collections_Observable_ObservableHashSet___0__System_Func___0___1__System_Action___1__}

```csharp
public static IReadOnlyObservableCollectionSync<TTo> CreateSync<TFrom, TTo>(this ObservableHashSet<TFrom> fromHashSet, Func<TFrom, TTo> converter, Action<TTo> remove) where TFrom : notnull where TTo : notnull
```

#### Parameters

`fromHashSet` [ObservableHashSet](Aspid.Collections.Observable.ObservableHashSet-1.md)\<TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`remove` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<TTo\>

#### Returns

 [IReadOnlyObservableCollectionSync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableCollectionSync-1.md)\<TTo\>

#### Type Parameters

`TFrom` 

`TTo` 

### CreateSync\<TKey, TFrom, TTo\>\(IReadOnlyObservableDictionary\<TKey, TFrom\>, Func\<TFrom, TTo\>, bool\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__3_Aspid_Collections_Observable_IReadOnlyObservableDictionary___0___1__System_Func___1___2__System_Boolean_}

```csharp
public static IReadOnlyObservableDictionarySync<TKey, TTo> CreateSync<TKey, TFrom, TTo>(this IReadOnlyObservableDictionary<TKey, TFrom> fromDictionary, Func<TFrom, TTo> converter, bool isDisposable = false) where TKey : notnull
```

#### Parameters

`fromDictionary` [IReadOnlyObservableDictionary](Aspid.Collections.Observable.IReadOnlyObservableDictionary-2.md)\<TKey, TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`isDisposable` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [IReadOnlyObservableDictionarySync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableDictionarySync-2.md)\<TKey, TTo\>

#### Type Parameters

`TKey` 

`TFrom` 

`TTo` 

### CreateSync\<TKey, TFrom, TTo\>\(IReadOnlyObservableDictionary\<TKey, TFrom\>, Func\<TFrom, TTo\>, Action\<TTo\>\) {#Aspid_Collections_Observable_Synchronizer_CreateSyncExtensions_CreateSync__3_Aspid_Collections_Observable_IReadOnlyObservableDictionary___0___1__System_Func___1___2__System_Action___2__}

```csharp
public static IReadOnlyObservableDictionarySync<TKey, TTo> CreateSync<TKey, TFrom, TTo>(this IReadOnlyObservableDictionary<TKey, TFrom> fromDictionary, Func<TFrom, TTo> converter, Action<TTo> remove) where TKey : notnull
```

#### Parameters

`fromDictionary` [IReadOnlyObservableDictionary](Aspid.Collections.Observable.IReadOnlyObservableDictionary-2.md)\<TKey, TFrom\>

`converter` [Func](https://learn.microsoft.com/dotnet/api/system.func-2)\<TFrom, TTo\>

`remove` [Action](https://learn.microsoft.com/dotnet/api/system.action-1)\<TTo\>

#### Returns

 [IReadOnlyObservableDictionarySync](Aspid.Collections.Observable.Synchronizer.IReadOnlyObservableDictionarySync-2.md)\<TKey, TTo\>

#### Type Parameters

`TKey` 

`TFrom` 

`TTo` 

