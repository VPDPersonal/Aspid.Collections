---
title: ObservableDictionary
---

В `ObservableDictionary<TKey, TValue>` запись определяется ключом. В событии передаётся пара `KeyValuePair<TKey, TValue>`.

## Обновление таблицы лидеров

```csharp
using System;
using Aspid.Collections.Observable;

using var scores = new ObservableDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
scores["Ada"] = 10;
scores["Ada"] = 18;
bool found = scores.TryGetValue("ada", out int score);
```

Две записи вызовут `Add` и `Replace`. Поиск найдёт значение 18. Чтобы обновить строку таблицы, возьмите ключ из элемента события. Индексы изменений равны −1.

## Операции

| Метод | Поведение |
| --- | --- |
| `Add(key, value)` | Вставляет новый ключ. Дубликат вызывает `ArgumentException`. |
| `this[key]` | Чтение отсутствующего ключа вызывает исключение. Присваивание добавляет или заменяет запись. |
| `ContainsKey / TryGetValue` | Проверяют наличие ключа или получают значение за один поиск. |
| `Remove(key)` | Возвращает false для отсутствующего ключа. При удалении отправляет прежнюю пару. |
| `Remove(pair)` | Удаляет пару при совпадении ключа и значения. |
| `Keys / Values / Comparer` | Возвращают последовательности ключей и значений, а также правило сравнения ключей. |

[Исходный код](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableDictionary.cs)
