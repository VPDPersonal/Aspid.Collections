---
title: "Установка"
---

Aspid.Collections поддерживает Unity **2021.3.0f1 и новее**. Библиотека написана на C#, её можно подключить и к проекту .NET.

## Установка в Unity

1. Откройте **Window → Package Manager**.
2. Нажмите **+ → Install package from git URL…** (в некоторых версиях редактора пункт называется **Add package from git URL…**).
3. Вставьте адрес ниже и подтвердите установку. Когда Unity закончит компиляцию, откройте **In Project** и найдите **Aspid.Collections**.

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

Если у ваших скриптов свой файл определения сборки, добавьте **Aspid.Collections.Observable** в **Assembly Definition References**. Для доступа к коллекциям в коде подключите пространство имён `Aspid.Collections.Observable`.

## Выбор версии

| Суффикс URL | Когда использовать |
| --- | --- |
| `#upm` | Для стабильных релизов. Чтобы перейти на новую версию, нажмите **Update** в Package Manager. |
| `#upm/1.1.1` | Чтобы закрепить версию 1.1.1. Для другого релиза подставьте его номер. |
| `#upm-preview` | Для предварительных релизов, когда доступна ветка preview. |

Оставьте прежний адрес репозитория и замените суффикс. [Опубликованные версии](https://github.com/VPDPersonal/Aspid.Collections/releases)

## Подключение исходников в .NET

Клонируйте репозиторий. В папке вашего проекта .NET 10 выполните команду ниже, указав путь к своей копии. Подключённый проект собирает исходники библиотеки без Unity.

```sh
dotnet add reference /path/to/Aspid.Collections/DotnetTests/Runtime/Aspid.Collections.Observable.csproj
```

Чтобы проверить библиотеку, запустите тесты из корня репозитория.

```sh
dotnet test DotnetTests/Aspid.Collections.Tests.csproj --configuration Release
```
