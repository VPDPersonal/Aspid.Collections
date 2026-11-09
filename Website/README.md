# Сайт Aspid.Collections

Сайт документации на Docusaurus с интерфейсом Aspid.FastTools: поиск по документации и API, русский и английский языки, светлая и тёмная темы, выбор акцента, анимированный логотип и галерея примеров.

## Локальный запуск

Нужен Node.js 22. Из корня репозитория:

```bash
npm --prefix Website ci
npm --prefix Website run start
```

Английская версия запускается по адресу, который напечатает Docusaurus. Для русской версии:

```bash
npm --prefix Website run start:ru
```

Для проверки обоих языков одновременно соберите статический сайт:

```bash
npm --prefix Website run build
npm --prefix Website run serve -- --port 4173 --no-open
```

Базовый путь — `/Aspid.Collections/`, русский язык — `/Aspid.Collections/ru/`.

## Источники содержимого

- `docs/` — английские руководства; `i18n/ru/docusaurus-plugin-content-docs/current/` — русские переводы.
- `tutorials/` — английские упражнения. Русские упражнения и изображения копируются из `Samples~/` пакета командой `sync-content`; правьте оригинал в пакете.
- `api/` — сохранённый результат DocFX. Генератор читает только engine-free runtime C#, а не Unity-проект или его Library.
- `CHANGELOG.md` в корне репозитория — источник истории изменений; страницы истории создаются перед сборкой.
- README репозитория остаются самостоятельными файлами: сайт их не перезаписывает.

## Проверки и генерация API

```bash
npm --prefix Website run sync-content
npm --prefix Website run check-content
npm --prefix Website test
npm --prefix Website run build
```

Для обновления API нужен .NET SDK 10:

```bash
npm --prefix Website run api
```

Версия DocFX закреплена в `.config/dotnet-tools.json`. Преобразование Markdown, поиск, шрифты, соответствие примеров пакету и парность кодовых блоков переводов проверяются тестами. Сборка отклоняет сломанные внутренние ссылки и якоря.

## GitHub Pages

Workflow `.github/workflows/docs.yml` собирает оба языка в PR. После попадания изменений в `main` он публикует `Website/build` через GitHub Pages. В настройках репозитория **Pages → Source** должен быть выбран **GitHub Actions**.

Плановый адрес — `https://vpdpersonal.github.io/Aspid.Collections/`. Добавление workflow само по себе не публикует текущую рабочую копию. Сайт не требует сервера, базы данных или ключей доступа.
