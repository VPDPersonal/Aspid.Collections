---
title: "Обработчики для аналитики"
---

Сцена · `AnalyticsTap.unity`

Введите `analytics` в фильтре Console. Сравните первые две записи о добавлении. Один и тот же обработчик получает сначала один элемент, затем два. Замена, перемещение, удаление и сброс вызывают отдельные обработчики. В `OnDestroy` компонент освобождает подписку. [AnalyticsTapSample.cs](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Samples~/05_SplitByEvents/AnalyticsTapSample.cs)
