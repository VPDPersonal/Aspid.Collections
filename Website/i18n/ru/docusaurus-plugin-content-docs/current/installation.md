# Установка

## Unity Package Manager

Откройте **Window → Package Manager**, выберите **+ → Install package from git URL…** и вставьте:

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

`upm` указывает на стабильные релизы. Кнопка **Update** устанавливает новый релиз, когда он доступен. Для воспроизводимых зависимостей закрепите неизменяемую версию:

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm/1.1.1
```

Предварительные релизы используют ветку `upm-preview`, когда она доступна. Неизменяемые теги имеют вид `upm/<version>`, включая предварительные версии. ID пакета — `tech.aspid.collections`, минимальная версия Unity — **2021.3.0f1**.

## Импорт примера

Выберите **Aspid.Collections → Samples → Import**, откройте `Scenes/<имя примера>.unity` и запустите Play Mode. Примерам нужен встроенный модуль **UI Toolkit** (`com.unity.modules.uielements` 1.0.0). Каждый содержит собственные UI, шрифты и asmdef и импортируется отдельно. Самому runtime этот модуль не нужен.

В исходном репозитории пакет находится в `Aspid.Collections/Packages/tech.aspid.collections/`. В релизных UPM-ссылках он расположен в корне: используйте `#upm`, без параметра `?path=`.

## Использование вне Unity

Runtime написан на C# без ссылок на движок. Включите файлы из `Runtime/Observable/` в сборку .NET, сохранив пространства имён. `DotnetTests/Runtime/Aspid.Collections.Observable.csproj` в репозитории показывает такую сборку. Команды установки из NuGet у проекта нет.

## Решение проблем

- **Дублируется имя сборки:** удалите вторую копию из `Assets` перед установкой UPM-пакета.
- **Не разрешается Test Framework 1.6.0:** версия 1.1.0 объявляла эту неопубликованную зависимость. Перейдите на 1.1.1 или текущий стабильный канал.
- **Нет пространства имён UIElements после импорта примера:** восстановите встроенный модуль UI Toolkit через Package Manager проекта. Потребителям только ядра он не требуется.

Далее — [быстрый старт](quick-start.md).
