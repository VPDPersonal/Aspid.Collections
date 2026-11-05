# Aspid.Collections

<picture>
  <source srcset="Documentation~/Images/aspid_collections_readme_banner_dark.webp" />
  <img src="Documentation~/Images/aspid_collections_readme_banner.png" alt="Aspid.Collections" />
</picture>

[![Пакет Unity](https://img.shields.io/badge/Unity-Package-4fa35d?logo=unity&logoColor=white)](#установка)
[![Релиз](https://img.shields.io/github/v/release/VPDPersonal/Aspid.Collections?label=Release&labelColor=254d2c&color=4fa35d)](https://github.com/VPDPersonal/Aspid.Collections/releases)
[![Лицензия MIT](https://img.shields.io/github/license/VPDPersonal/Aspid.Collections?label=License&labelColor=254d2c&color=4fa35d)](LICENSE)

[English](README.md) · **Русский**

Aspid.Collections — наблюдаемые коллекции для Unity и .NET. Реагируйте на изменения инвентаря или таблицы лидеров, преобразуйте модели в модели представления и обновляйте отфильтрованные и отсортированные списки вслед за источником. Библиотека написана на C# и работает без зависимостей от движка Unity.

## Установка

1. Откройте **Window → Package Manager**.
2. Выберите **+ → Install package from git URL…**.
3. Вставьте URL и нажмите **Install**.

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

Ветка `upm` указывает на последний стабильный релиз. Чтобы закрепить версию, укажите её тег `upm/<версия>`, например `#upm/1.1.0`. См. [список релизов](https://github.com/VPDPersonal/Aspid.Collections/releases).

Для предварительных релизов используется ветка `upm-preview`.

<details>
<summary>Совместимость с Unity и проблемы установки</summary>

В манифесте указана минимальная версия Unity 2021.3. Пакет версии 1.1.0 также объявляет зависимость от `com.unity.test-framework@1.6.0`. Если Package Manager не может разрешить её в вашей версии Unity, см. [обсуждение зависимости](https://github.com/VPDPersonal/Aspid.Collections/pull/20).

</details>

## Возможности

Каждый пример можно запустить отдельно как программу C# с инструкциями верхнего уровня.

### Уведомления об изменениях

<table>
<tr>
<td width="60%">

```csharp
using System;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
items.CollectionChanged += change =>
    Console.WriteLine(change.Action);

items.Add("Potion");       // Add
items[0] = "Elixir";       // Replace
items.RemoveAt(0);         // Remove
```

</td>
<td width="40%">

Подпишитесь на `CollectionChanged`, чтобы реагировать на изменения. Для одиночных операций `IsSingleItem` указывает на поля `NewItem` / `OldItem`, для пакетных используйте `NewItems` / `OldItems`. При освобождении список очищается и отправляет `Reset`.

[Пример с уведомлениями →](Samples~/01_BasicChangeNotifications)

</td>
</tr>
</table>

### Синхронизированные представления

<table>
<tr>
<td width="60%">

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

using var scores = new ObservableList<int>();
using var labels = scores.CreateSync(n => $"Score {n}");

scores.Add(10);
scores[0] = 20;
Console.WriteLine(labels[0]); // Score 20
```

</td>
<td width="40%">

`CreateSync` преобразует элементы источника и собирает их в коллекцию для чтения, которая обновляется вслед за ним. Односторонняя синхронизация работает для всех пяти типов коллекций. [Перегрузки](Runtime/Observable/Synchronizer/Extensions/CreateSyncExtensions.cs)

[Пример синхронизации инвентаря →](Samples~/02_InventorySync)

</td>
</tr>
</table>

### Фильтрация и сортировка

<table>
<tr>
<td width="60%">

```csharp
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var scores = new ObservableList<int> { 25, 5, 15 };
using var visible = scores.CreateFiltered(
    n => n >= 10, Comparer<int>.Default);

scores.Add(12);
// 12, 15, 25
Console.WriteLine(string.Join(", ", visible));
```

</td>
<td width="40%">

`CreateFiltered` показывает подходящие элементы в заданном порядке, сохраняя исходную коллекцию. Меняйте `Filter` и `Comparer` во время работы или соединяйте представления в цепочку для нескольких фильтров. Если источник не отправляет уведомления, после его изменения вызовите `Update()`.

[Пример фильтрации инвентаря →](Samples~/03_FilteredInventory)

</td>
</tr>
</table>

### Отдельные обработчики изменений

<table>
<tr>
<td width="60%">

```csharp
using System;
using Aspid.Collections.Observable;

using var items = new ObservableList<string>();
using var events = items.SplitByEvents(
    added: (batch, _) =>
        Console.WriteLine($"Added {batch.Count}"));

items.AddRange("Potion", "Elixir"); // Added 2
```

</td>
<td width="40%">

`SplitByEvents` вызывает отдельные обработчики добавления, удаления, перемещения, замены и сброса. Обработчики изменений элементов получают список даже при изменении одного элемента.

[Пример подписки для аналитики →](Samples~/05_SplitByEvents)

</td>
</tr>
</table>

## Коллекции

| Тип | Для чего подходит |
| --- | --- |
| [`ObservableList<T>`](Runtime/Observable/ObservableList.cs) | Упорядоченный список с индексами, перемещением и пакетными операциями |
| [`ObservableDictionary<TKey, TValue>`](Runtime/Observable/ObservableDictionary.cs) | Доступ по ключу, как в [примере с таблицей лидеров](Samples~/04_DictionaryKeyedTable) |
| [`ObservableHashSet<T>`](Runtime/Observable/ObservableHashSet.cs) | Уникальные элементы и проверка принадлежности множеству |
| [`ObservableQueue<T>`](Runtime/Observable/ObservableQueue.cs) | Обработка в порядке добавления с пакетными операциями |
| [`ObservableStack<T>`](Runtime/Observable/ObservableStack.cs) | Обработка в обратном порядке добавления с пакетными операциями |

Предоставляйте `IReadOnlyObservableList<T>` или `IObservableCollection<T>`, если вызывающему коду нужны чтение и подписка на изменения. Эти интерфейсы ковариантны для ссылочных типов. У интерфейсов словаря оба параметра типа инвариантны.

Вызывайте `Dispose()` у синхронизированных и отфильтрованных представлений и у результата `SplitByEvents`, чтобы снять подписки. Освобождайте зависимые представления раньше их источников. Работайте с отфильтрованным представлением и его источником в одном потоке.

## Ресурсы

| [Примеры Unity](Samples~) | [API библиотеки](Runtime/Observable) | [История изменений](CHANGELOG.md) |
| --- | --- | --- |
| В Package Manager выберите **Aspid.Collections → Samples → Import**. Откройте импортированную сцену и запустите Play Mode. | Типы коллекций, аргументы событий и методы расширения. | Изменения и исправления по релизам. |

## Помощь и обратная связь

В сообщении о проблеме укажите версии Unity и пакета, а также шаги воспроизведения. [Сообщить о проблеме](https://github.com/VPDPersonal/Aspid.Collections/issues)

Aspid.Collections распространяется под [лицензией MIT](LICENSE).
