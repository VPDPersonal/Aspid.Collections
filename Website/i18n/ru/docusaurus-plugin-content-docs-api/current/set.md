---
title: ObservableHashSet
---

Используйте `ObservableHashSet<T>` для уникальных элементов, например идентификаторов открытых способностей. Правило сравнения определяет, какие значения представляют один элемент.

## Открытые способности

```csharp
using System;
using Aspid.Collections.Observable;

using var abilities = new ObservableHashSet<string>(StringComparer.OrdinalIgnoreCase);
bool unlocked = abilities.Add("Dash");
bool duplicate = abilities.Add("dash");
bool removed = abilities.Remove("DASH");
```

Значения `unlocked` и `removed` равны true. Повторное добавление вернёт false. Подписчики получат одно добавление и одно удаление, оба со строкой `"Dash"` в исходном регистре.

## Операции

| Метод | Поведение |
| --- | --- |
| `Add / Remove` | Возвращают true и отправляют событие при изменении состава множества. Индексы равны −1. |
| `Contains / Overlaps` | Проверяют наличие одного элемента или пересечение с другой последовательностью. |
| `IsSubsetOf / IsSupersetOf` | Проверяют включение множеств. Варианты с Proper дополнительно требуют различия составов. |
| `SetEquals` | Сравнивает состав по правилу множества независимо от порядка. |
| `Clear` | Удаляет все элементы и отправляет `Reset`. |

[Исходный код](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Runtime/Observable/ObservableHashSet.cs)
