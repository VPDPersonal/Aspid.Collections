---
title: Фильтрация и сортировка
---

Используйте `CreateFiltered`, когда на разных экранах нужны разные части одного списка. В представлении остаются исходные элементы, а видимые результаты доступны по своим индексам.

## Покажите лучшие результаты

```csharp
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var scores = new ObservableList<int> { 12, 30, 5 };
var descending = Comparer<int>.Create((left, right) => right.CompareTo(left));
using var leaders = scores.CreateFiltered(score => score >= 10, descending);

scores.Add(18);
Console.WriteLine(string.Join(", ", leaders)); // 30, 18, 12

leaders.Filter = score => score >= 20;
Console.WriteLine(string.Join(", ", leaders)); // 30
```

Присваивание `Filter` или `Comparer` сразу перестраивает представление. Задайте `Filter = null`, чтобы включить все элементы, или `Comparer = null`, чтобы вернуть исходный порядок. Событие `CollectionChanged` представления вызывается без аргументов. В его обработчике прочитайте представление заново.

## Обновите представление после изменения условия или элемента

Значение переменной внутри предиката может измениться при том же делегате. В этом случае вызовите `Update()`. Этот же метод понадобится после изменения поля элемента, которое участвует в фильтрации или сортировке, и после изменения обычного `List<T>`, используемого как источник.

```csharp
int threshold = 10;
using var qualified = scores.CreateFiltered(score => score >= threshold);
threshold = 25;
qualified.Update();
```

## Объедините представления в цепочку

Вызовите `CreateFiltered` на готовом представлении, чтобы добавить второе независимое условие, например поиск внутри категории. Передайте `IReadOnlyFilteredList<T>` коду, который только отображает результат. Создавайте, читайте и меняйте цепочку в том же потоке, где изменяется источник. [Освобождайте последним созданное представление первым](./lifetime.md).
