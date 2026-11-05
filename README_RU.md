# Aspid.Collections

<picture>
  <source srcset="Documentation~/Images/aspid_collections_readme_banner_dark.webp" />
  <img src="Documentation~/Images/aspid_collections_readme_banner.png" alt="Aspid.Collections" />
</picture>

[![Unity 2021.3.0f1+](Documentation~/Images/status-badge-unity.svg)](#установка)
[![Релиз 1.1.0](Documentation~/Images/status-badge-release.svg)](https://github.com/VPDPersonal/Aspid.Collections/releases/tag/v1.1.0)
[![Лицензия MIT](Documentation~/Images/status-badge-license.svg)](LICENSE)

[English](README.md) · **Русский**

Aspid.Collections — наблюдаемые коллекции для Unity и .NET. Реагируйте на изменения инвентаря или таблицы лидеров, преобразуйте модели в модели представления и обновляйте отфильтрованные и отсортированные списки вслед за источником. Библиотека написана на C# и работает без зависимостей от движка Unity.

## Установка

<table>
<tr>
<td width="56%"><img src="Documentation~/Images/install.webp" alt="Установка Aspid.Collections по Git URL в Package Manager" width="100%"></td>
<td width="44%">
<ol>
<li>Откройте <b>Window → Package Manager</b></li>
<li>Выберите <b>+ → Install package from git URL…</b></li>
<li>Вставьте URL и нажмите <b>Install</b></li>
</ol>
</td>
</tr>
</table>

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

URL указывает на последнюю версию. Кнопка **Update** в Package Manager установит следующую. Чтобы закрепить версию или сменить канал, см. [варианты установки](#варианты-установки).

Нужна Unity 2021.3.0f1 или новее.

<details>
<summary>Решение проблем</summary>

**Сразу после установки появляются ошибки компиляции.** Проверьте версию Unity в **Help → About Unity** (**Unity → About Unity** на macOS).

**В Console появляется `Assembly with name 'Aspid.Collections.Observable' already exists`.** В проекте есть вторая копия пакета, например его папка в `Assets`. Удалите копию.

**Package Manager не может разрешить `com.unity.test-framework@1.6.0`.** Эта зависимость указана в опубликованном релизе `#upm` (1.1.0). Из `main` её убрали в [PR #20](https://github.com/VPDPersonal/Aspid.Collections/pull/20).

</details>

## Возможности

### Уведомления об изменениях

<table>
<tr>
<td width="60%">

```csharp
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

<table width="100%">
<thead>
<tr><th>Тип</th><th width="100%">Для чего подходит</th></tr>
</thead>
<tbody>
<tr><td><a href="Runtime/Observable/ObservableList.cs"><code>ObservableList&lt;T&gt;</code></a></td><td>Упорядоченный список с индексами, перемещением и пакетными операциями</td></tr>
<tr><td><a href="Runtime/Observable/ObservableDictionary.cs"><code>ObservableDictionary&lt;TKey, TValue&gt;</code></a></td><td>Доступ по ключу, как в <a href="Samples~/04_DictionaryKeyedTable">примере с таблицей лидеров</a></td></tr>
<tr><td><a href="Runtime/Observable/ObservableHashSet.cs"><code>ObservableHashSet&lt;T&gt;</code></a></td><td>Уникальные элементы и проверка принадлежности множеству</td></tr>
<tr><td><a href="Runtime/Observable/ObservableQueue.cs"><code>ObservableQueue&lt;T&gt;</code></a></td><td>Обработка в порядке добавления с пакетными операциями</td></tr>
<tr><td><a href="Runtime/Observable/ObservableStack.cs"><code>ObservableStack&lt;T&gt;</code></a></td><td>Обработка в обратном порядке добавления с пакетными операциями</td></tr>
</tbody>
</table>

Предоставляйте `IReadOnlyObservableList<T>` или `IObservableCollection<T>`, если вызывающему коду нужны чтение и подписка на изменения. Эти интерфейсы ковариантны для ссылочных типов. У интерфейсов словаря оба параметра типа инвариантны.

Вызывайте `Dispose()` у синхронизированных и отфильтрованных представлений и у результата `SplitByEvents`, чтобы снять подписки. Освобождайте зависимые представления раньше их источников. Работайте с отфильтрованным представлением и его источником в одном потоке.

## Ресурсы

| [Примеры Unity](Samples~) | [API библиотеки](Runtime/Observable) | [История изменений](CHANGELOG.md) |
| --- | --- | --- |
| В Package Manager выберите **Aspid.Collections → Samples → Import**. Откройте импортированную сцену и запустите Play Mode. | Типы коллекций, аргументы событий и методы расширения. | Изменения и исправления по релизам. |

### Варианты установки

Чтобы закрепить версию, укажите её тег `upm/<версия>`, например `#upm/1.1.0`. См. [список релизов](https://github.com/VPDPersonal/Aspid.Collections/releases).

Предварительные релизы публикуются в ветку `upm-preview`.

## Помощь и поддержка

<p>Нашли ошибку или хотите задать вопрос? Укажите версию Unity, версию пакета и шаги воспроизведения.</p>
<p><a href="https://github.com/VPDPersonal/Aspid.Collections/issues"><b>Открыть задачу</b></a> · <a href="https://github.com/VPDPersonal/Aspid.Collections">Поставить звезду на GitHub</a></p>
