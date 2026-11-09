# Быстрый старт

## Один источник, два потребителя

Создайте коллекцию, затем синхронизированную проекцию и отфильтрованное представление. При создании они включат уже существующие элементы источника.

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using Aspid.Collections.Observable.Synchronizer;

using var scores = new ObservableList<int> { 25, 5, 15 };
using var labels = scores.CreateSync(score => $"Score {score}");
using var visible = scores.CreateFiltered(score => score >= 10);

scores.Add(30);
Console.WriteLine(labels[3]); // Score 30
Console.WriteLine(visible.Count); // 3

scores.Move(3, 0);
Console.WriteLine(labels[0]); // Score 30
```

`using var` освобождает объекты в обратном порядке объявления: фильтр, синхронизированную проекцию, затем источник. Источник хранит данные, а потребители определяют способ их отображения.

## Следующий шаг

1. [Выберите коллекцию](collections.md) по требованиям к порядку, уникальности или постоянным ключам.
2. [Разберите события](events.md), если нужны точные элементы и индексы.
3. [Создайте синхронизированное представление](synchronization.md), если каждому элементу источника соответствует объект назначения.
4. [Добавьте фильтрацию и сортировку](filtering.md) для поиска и представлений только для чтения.

## Попробуйте в Unity

Импортируйте [Squad Roster](/tutorials/squad-roster). Добавьте пару участников, переставьте их, затем замените одного. Номера представлений и счётчики покажут, какие карточки созданы, сохранены и освобождены.

Для изменения свойства модели используйте [Item Browser](/tutorials/item-browser): измените цену и явно обновите цепочку фильтров. Коллекции наблюдают структурные операции, а не произвольные свойства элементов.
