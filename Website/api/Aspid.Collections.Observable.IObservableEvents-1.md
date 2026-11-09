---
title: "Interface IObservableEvents<T>"
sidebar_label: "IObservableEvents<T>"
description: "Interface IObservableEvents<T> — Aspid.Collections API reference"
hide_title: true
pagination_prev: null
pagination_next: null
---
# Interface IObservableEvents\<T\> {#Aspid_Collections_Observable_IObservableEvents_1}

Namespace: [Aspid.Collections.Observable](Aspid.Collections.Observable.md)  
Assembly: Aspid.Collections.Observable.dll  

```csharp
public interface IObservableEvents<out T> : IDisposable
```

#### Type Parameters

`T` 

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

### Added {#Aspid_Collections_Observable_IObservableEvents_1_Added}

```csharp
event Action<IReadOnlyList<out T?>, int> Added
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32)\>

### Moved {#Aspid_Collections_Observable_IObservableEvents_1_Moved}

```csharp
event Action<IReadOnlyList<out T?>, int, int> Moved
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32), [int](https://learn.microsoft.com/dotnet/api/system.int32)\>

### Removed {#Aspid_Collections_Observable_IObservableEvents_1_Removed}

```csharp
event Action<IReadOnlyList<out T?>, int> Removed
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-2)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32)\>

### Replaced {#Aspid_Collections_Observable_IObservableEvents_1_Replaced}

```csharp
event Action<IReadOnlyList<out T?>, IReadOnlyList<out T?>, int> Replaced
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action-3)\<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1)\<T?\>, [int](https://learn.microsoft.com/dotnet/api/system.int32)\>

### Reset {#Aspid_Collections_Observable_IObservableEvents_1_Reset}

```csharp
event Action Reset
```

#### Event Type

 [Action](https://learn.microsoft.com/dotnet/api/system.action)

