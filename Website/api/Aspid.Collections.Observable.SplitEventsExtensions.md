---
title: "Class SplitEventsExtensions"
sidebar_label: "SplitEventsExtensions"
description: "Class SplitEventsExtensions — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Class SplitEventsExtensions {#Aspid_Collections_Observable_SplitEventsExtensions}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public static class SplitEventsExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SplitEventsExtensions](Aspid.Collections.Observable.SplitEventsExtensions.md)


## Methods

### SplitByEvents\<T\>\(IObservableCollection\<T\>, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int\>?, Action\<IReadOnlyList\<T?\>, int, int\>?, Action\<IReadOnlyList\<T?\>, IReadOnlyList\<T?\>, int\>?, Action?\) {#Aspid_Collections_Observable_SplitEventsExtensions_SplitByEvents__1_Aspid_Collections_Observable_IObservableCollection___0__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Int32_System_Int32__System_Action_System_Collections_Generic_IReadOnlyList___0__System_Collections_Generic_IReadOnlyList___0__System_Int32__System_Action_}

```csharp
public static IObservableEvents<T> SplitByEvents<T>(this IObservableCollection<T> observableCollection, Action<IReadOnlyList<T?>, int>? added = null, Action<IReadOnlyList<T?>, int>? removed = null, Action<IReadOnlyList<T?>, int, int>? moved = null, Action<IReadOnlyList<T?>, IReadOnlyList<T?>, int>? replaced = null, Action? reset = null)
```

#### Parameters

`observableCollection` [IObservableCollection](Aspid.Collections.Observable.IObservableCollection-1.md)\<T\>

`added` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

`removed` [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

`moved` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32), [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

`replaced` [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32)\>?

`reset` [Action](https://learn.microsoft.com/dotnet/api/system.action)?

#### Returns

 [IObservableEvents](Aspid.Collections.Observable.IObservableEvents-1.md)\<T\>

#### Type Parameters

`T` 

