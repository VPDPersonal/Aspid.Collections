---
id: match-scoreboard
slug: /match-scoreboard
title: "Match Scoreboard — результаты матча"
sidebar_label: "Результаты матча"
---

# Match Scoreboard — результаты матча

<SampleAvailability />

<SamplePreview sample="match-scoreboard" alt={"Сцена MatchScoreboard в Unity"} />


Готовая сцена `Scenes/MatchScoreboard.unity` показывает игроков по стабильным ID, а не по индексам строк. Импортируйте **04 Match Scoreboard** и запустите Play Mode. Нужен встроенный модуль UI Toolkit (`com.unity.modules.uielements`).

**Light / Dark** переключает тему. **+ Events** раскрывает журнал и дополнительные счётчики, включая `Created` и `Released`.

## Попробуйте

1. В начале Orin имеет 220 очков, Nova — 180, Mira — 140. Запомните номера `View`.
2. Дважды нажмите **Award +25**. Nova наберёт 230 и займёт первое место. У Nova появится новая строка при каждом изменении счёта; номера строк Mira и Orin сохранятся.
3. Нажмите **Reconnect NOVA**. Число игроков не изменится: ключ `NOVA` соответствует существующему `nova`. Счёт Nova увеличится ещё на 10.
4. **Leave first** удалит лидера. Если это Nova, повторное **Reconnect NOVA** добавит его обратно со 125 очками. Остальные игроки останутся на своих ID.
5. **End match** очистит таблицу. **Join player** или **Reconnect NOVA** восстановит непустое состояние.

## Обновление по ключу

```csharp
_players = new ObservableDictionary<string, PlayerScore>(
    StringComparer.OrdinalIgnoreCase);
_rows = _players.CreateSync(CreateRow, isDisposable: true);

var player = _players["nova"];
_players["NOVA"] = player.Add(25); // Replace the existing value.
```

`PlayerScore` неизменяемый: `Add` возвращает новое значение. Присваивание по ключу создаёт `Replace`, после которого проекция освобождает прежний `ScoreRow` и создаёт новый. Редактирование поля обычного объекта внутри словаря само по себе не отправило бы событие коллекции.

В `Scripts/MatchScoreboardSample.cs` порядок строк вычисляется отдельно: очки по убыванию, затем ID без учёта регистра для разрешения ничьей. Порядок перечисления словаря не используется как место игрока. Повторная расстановка элементов не пересоздаёт неизменившиеся строки. Проекция также сохраняет comparer исходного словаря.

## Владение

`ScoreRow` реализует `IDisposable`; удаление, замена и очистка освобождают соответствующие строки. В `OnDisable` сначала освобождается проекция `_rows`, затем `_players`. Лимит в восемь игроков относится только к демонстрационной сцене.
