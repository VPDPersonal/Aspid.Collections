---
title: "Installation"
---

Aspid.Collections runs on Unity **2021.3.0f1 or later**. The runtime uses pure C# and can also be referenced by a .NET project.

## Install in Unity

1. Open **Window → Package Manager**.
2. Select **+ → Install package from git URL…** (called **Add package from git URL…** in some Editor versions).
3. Paste the URL below and confirm. Once Unity finishes compiling, find **Aspid.Collections** under **In Project**.

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

If your scripts belong to a custom assembly definition, add **Aspid.Collections.Observable** under **Assembly Definition References**. In C#, import `Aspid.Collections.Observable` to access the collection types.

## Choose a version

| Git suffix | Use |
| --- | --- |
| `#upm` | Track stable releases. Use **Update** in Package Manager to adopt a newer release. |
| `#upm/1.1.1` | Pin release 1.1.1. Replace the number with a published version to pin another release. |
| `#upm-preview` | Follow the preview branch when available. |

Use the same repository URL and replace only the suffix. [Published releases](https://github.com/VPDPersonal/Aspid.Collections/releases)

## Use the source from .NET

Clone the repository and run the following from a .NET 10 project directory. Replace the path with the location of your clone. The referenced project compiles the runtime sources without Unity.

```sh
dotnet add reference /path/to/Aspid.Collections/DotnetTests/Runtime/Aspid.Collections.Observable.csproj
```

To verify the checkout, run the engine-free test suite from the repository root.

```sh
dotnet test DotnetTests/Aspid.Collections.Tests.csproj --configuration Release
```
