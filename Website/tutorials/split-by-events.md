---
title: "Analytics callbacks"
---

Scene · `AnalyticsTap.unity`

Filter the Console by `analytics`. Compare the first two add messages: the callback gets one item and then two. Replace, move, remove and reset go to separate callbacks. In `OnDestroy`, the component disposes the subscription handle. [AnalyticsTapSample.cs](https://github.com/VPDPersonal/Aspid.Collections/blob/main/Aspid.Collections/Packages/tech.aspid.collections/Samples~/05_SplitByEvents/AnalyticsTapSample.cs)
