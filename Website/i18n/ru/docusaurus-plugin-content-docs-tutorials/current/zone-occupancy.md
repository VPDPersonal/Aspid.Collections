---
id: zone-occupancy
slug: /zone-occupancy
title: "Zone Occupancy — зона обнаружения"
sidebar_label: "Зона обнаружения"
---

# Zone Occupancy — зона обнаружения

<SampleAvailability />

<SamplePreview sample="zone-occupancy" alt={"Сцена ZoneOccupancy в Unity"} />


Готовая сцена `Scenes/ZoneOccupancy.unity` показывает множество уникальных посетителей и две независимые подписки: отображение зоны и сигнализацию. Импортируйте **05 Zone Occupancy** и запустите Play Mode. Нужен встроенный модуль UI Toolkit (`com.unity.modules.uielements`).

**Light / Dark** переключает тему. **+ Events** раскрывает журнал и дополнительные счётчики.

## Попробуйте

1. В начале внутри Nova и Mira. Сигнализация подключена, но счётчик `Signals` равен нулю: существующие элементы не воспроизводятся как новые события.
2. Нажмите **Enter NOVA again**. `Add` вернёт `false`, количество посетителей и сигналов не изменится. Множество сравнивает имена без учёта регистра.
3. Нажмите **Enter visitor**. Появится следующий отсутствующий посетитель и один сигнал входа.
4. Нажмите **Detach alarm**, затем **Enter visitor** или **Leave first**. Карточки изменятся, счётчик сигнализации останется прежним.
5. Нажмите **Attach alarm**. Старые изменения не будут воспроизведены. Следующее изменение множества снова увеличит счётчик.
6. **Evacuate** отправляет `Reset`, а не отдельный `Remove` для каждого посетителя. При подключённой сигнализации это один сигнал очистки.

## Независимые потребители

```csharp
_markers = _visitors.CreateSync(CreateMarker,
    marker => marker.RemoveFromHierarchy());

_alarm = _visitors.SplitByEvents(
    added: (items, index) => OnEntered(items),
    removed: (items, index) => OnLeft(items),
    reset: OnEvacuated);

_alarm.Dispose(); // The occupancy projection remains connected.
```

`Scripts/ZoneOccupancySample.cs` сохраняет возвращённый `IObservableEvents<string>`: именно этот объект владеет подпиской. Подключение нового обработчика не синхронизирует текущее содержимое; для начального снимка здесь используется `CreateSync`.

В демонстрации `Leave first` удаляет первый перечисленный элемент множества. Порядок `HashSet` не гарантирован: для приоритетов, очереди входа или сортировки нужен отдельный порядок.

## Владение

Сигнализация и проекция принадлежат компоненту, а не друг другу. `OnDisable` отвязывает UI, освобождает сигнализацию, затем проекцию, затем множество. Повторное включение начинает новую демонстрацию без оставшихся обработчиков.
