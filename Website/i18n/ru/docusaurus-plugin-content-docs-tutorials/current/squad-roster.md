---
id: squad-roster
slug: /squad-roster
title: "Squad Roster — состав отряда"
sidebar_label: "Состав отряда"
---

# Squad Roster — состав отряда

<SampleAvailability />

<SamplePreview sample="squad-roster" alt={"Сцена SquadRoster в Unity"} />


Готовая сцена `Scenes/SquadRoster.unity` показывает связь данных отряда с карточками. Импортируйте **01 Squad Roster** в Package Manager и запустите Play Mode. Нужен встроенный модуль UI Toolkit (`com.unity.modules.uielements`).

**Light / Dark** переключает тему. **+ Events** раскрывает журнал и дополнительные счётчики, включая `Created` и `Released`.

## Попробуйте

1. В начале: 3 участника, `CREATED = 3`, `RELEASED = 0`.
2. Нажмите **Recruit pair**. Появятся две карточки, журнал покажет один `Add / batch of 2`, а не два одиночных события.
3. Нажмите **Rotate**. Первый участник окажется последним. Номера `View`, `Created` и `Released` не изменятся: `Move` сохраняет существующие представления.
4. Нажмите **Replace first**. Число участников останется прежним, `Created` и `Released` увеличатся на один.
5. Нажмите **Dismiss all**. Появится пустое состояние, все созданные карточки будут освобождены. **Recruit** начнёт новый отряд.

## Код, который стоит перенести в свой проект

```csharp
// The wrapper converts existing members and subscribes to later source changes.
_cards = _members.CreateSync(CreateCard, isDisposable: true);

_members.AddRange(first, second); // One batch notification.
_members.Move(0, _members.Count - 1); // Existing card objects survive.
_members[0] = replacement; // The previous projected card is disposed.
```

В `Scripts/SquadRosterSample.cs` метод `DescribeChange` разбирает собственные аргументы событий Collections: сначала проверяет `Reset`, затем `IsSingleItem`, затем читает одиночные или пакетные поля. В `Reset` нет списка удалённых элементов; проекция сама освобождает принадлежащие ей карточки.

`Scripts/Presentation/CrewCard.cs` реализует `IDisposable`: удаляет элемент из иерархии и увеличивает счётчик освобождений. `Render` только заново расставляет те же элементы в порядке источника. Поэтому перестановку можно отличить от пересоздания UI.

## Владение

Компонент владеет `_cards` и `_members`. В `OnDisable` он отвязывает UI и события, освобождает `_cards`, затем `_members`. При удалении участника освобождается только его карточка; исходная модель `CrewMember` не содержит ресурсов движка.

Лимит в восемь участников относится к демонстрации, а не к возможностям `ObservableList`.
