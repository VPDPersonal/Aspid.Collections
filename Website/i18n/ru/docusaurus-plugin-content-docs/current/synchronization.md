# Синхронизированные представления

## Один объект назначения на элемент источника

Подключите `Aspid.Collections.Observable.Synchronizer` и вызовите `CreateSync`. Он сразу преобразует текущее содержимое и отслеживает будущие изменения. Возвращаемый тип доступен только для чтения и поддерживает освобождение.

```csharp
using System;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Synchronizer;

using var names = new ObservableList<string> { "Nova", "Mira" };
using var rows = names.CreateSync(name => new MemberRow(name), isDisposable: true);
names.Move(0, 1); // Keep the same MemberRow objects.
names.RemoveAt(0); // Dispose only the removed row.

sealed class MemberRow : IDisposable
{
    public string Name { get; }
    public MemberRow(string name) => Name = name;
    public void Dispose() => Console.WriteLine($"Released {Name}");
}
```

## Выберите способ освобождения

| Перегрузка | Удалённые объекты назначения |
| --- | --- |
| `CreateSync(converter)` | Автоматического освобождения элементов нет |
| `CreateSync(converter, isDisposable: true)` | Вызывает IDisposable у поддерживающих его элементов |
| `CreateSync(converter, removed)` | Вызывает ваш обработчик удаления |

Освобождение действует при удалении, замене, очистке и Dispose обёртки. List.Move сохраняет объекты назначения. Если замена List/Dictionary оставляет ту же ссылку назначения, этот объект не освобождается. Обёртка не освобождает источник, а Dispose источника сам по себе не вызывает Dispose его моделей.

## Пять форм источника

List возвращает `IReadOnlyObservableListSync<TTo>`, Dictionary — `IReadOnlyObservableDictionarySync<TKey, TTo>`, Queue/Stack/HashSet — `IReadOnlyObservableCollectionSync<TTo>`. Очередь и стек сохраняют FIFO/LIFO-порядок, включая уже существующее содержимое стека.

List, Queue и Stack передают поддерживаемые пакетные уведомления. Sync словаря и множества может принимать пакеты от совместимого собственного источника, но отправляет изменения назначения по одному элементу. У самих Dictionary/HashSet нет публичного пакетного API изменения.

## Ограничения преобразования и comparer

- Преобразование HashSet должно выдавать уникальные значения назначения по `EqualityComparer<TTo>.Default`. Коллизия вызывает исключение. Comparer источника определяет исходные элементы во внутренней карте, а не равенство типа назначения.
- Sync словаря сохраняет comparer, если источник — `ObservableDictionary<TKey, TFrom>` или его подкласс. Другая реализация интерфейса словаря только для чтения использует comparer ключей по умолчанию.
- Преобразование и освобождение должны быть предсказуемыми и не выбрасывать исключений. Изменение не откатывается при исключении; часть потребителей уже могла его обработать.

См. [Squad Roster](/tutorials/squad-roster), [Crafting Queue](/tutorials/crafting-queue) и [Match Scoreboard](/tutorials/match-scoreboard).
