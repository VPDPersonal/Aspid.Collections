# Фильтрация и сортировка

## Представление списка только для чтения

`CreateFiltered` принимает `IReadOnlyList<T>` и возвращает `FilteredList<T>`. Передайте предикат, comparer или оба. Он наблюдает список с уведомлениями или другой фильтр; обычный список только для чтения требует явного обновления.

```csharp
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

using var scores = new ObservableList<int> { 25, 5, 15 };
var minimum = 10;
using var matching = scores.CreateFiltered(value => value >= minimum);
using var sorted = matching.CreateFiltered(Comparer<int>.Default);

scores.Add(12); // The chain updates automatically.
minimum = 20;
matching.Update(); // Refresh the captured predicate and every downstream view.
Console.WriteLine(string.Join(", ", sorted)); // 25
```

Порядок источника не меняется. Присваивание `Filter` или `Comparer` автоматически вызывает `Update()`. При равных по comparer значениях порядок определяется индексом источника.

## Обновляйте нужное звено

Изменения свойств элементов и захваченных переменных не отправляют событий коллекции. Вызовите `Update()` на затронутом верхнем представлении, чтобы остальные звенья получили обновление. Поэтому [Item Browser](/tutorials/item-browser) обновляет поисковый фильтр после скидки на предмет.

`CollectionChanged` фильтра — `Action` без элементов и индексов. Такой фильтр нельзя напрямую передать в `CreateSync`: перегрузке списка нужен `IReadOnlyObservableList<T>`. Перестраивайте соответствующую UI-проекцию по общему уведомлению фильтра либо используйте отдельный адаптер с явным владением.

## Инкрементальная обработка и перестроение

При прямом наблюдении списка инкрементально обрабатываются Add/Remove и одиночные Replace/Move. Reset и пакетные Replace/Move вызывают полный `Update()`. Следующий фильтр цепочки получает общий Action и перестраивается. Не рассчитывайте на инкрементальную работу каждого звена.

## Жизненный цикл и потоки

Освобождайте цепочку от последнего фильтра к источнику. `FilteredList.Dispose()` снимает подписки и слушателей; он не очищает источник и не обещает пустую карту индексов. Считайте освобождённое представление непригодным к дальнейшей работе. Сам `IReadOnlyFilteredList<T>` не поддерживает IDisposable, поэтому сохраняйте конкретного владельца.

Создавайте, изменяйте и перечисляйте фильтры на одном потоке. Внутренних блокировок карты индексов у них нет.
