# Installation

## Unity Package Manager

Open **Window → Package Manager**, choose **+ → Install package from git URL…**, and paste:

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm
```

`upm` follows stable releases. Use **Update** in Package Manager when a newer release is available. Pin an immutable release when a project needs reproducible dependencies:

```text
https://github.com/VPDPersonal/Aspid.Collections.git#upm/1.1.1
```

Prereleases use the `upm-preview` branch when available. Immutable version tags are named `upm/<version>`, including prerelease versions. The package ID is `tech.aspid.collections`; the minimum Unity version is **2021.3.0f1**.

## Import a sample

Select **Aspid.Collections → Samples → Import**, open `Scenes/<sample name>.unity` and enter Play mode. Samples require the built-in **UI Toolkit** module (`com.unity.modules.uielements` 1.0.0). They include their own UI, fonts and assembly definition; import any one independently. The core runtime does not require that module.

The source repository embeds the package under `Aspid.Collections/Packages/tech.aspid.collections/`. UPM release refs place the package at their root: use `#upm`, without a `?path=` parameter.

## Use outside Unity

The runtime is pure C#, with no engine references. Include the files under `Runtime/Observable/` in your .NET build, preserving their namespaces. The repository's `DotnetTests/Runtime/Aspid.Collections.Observable.csproj` is an engine-free example. This is not a published NuGet installation command.

## Troubleshooting

- **Duplicate assembly name:** remove a second copy under `Assets` before installing the UPM package.
- **Cannot resolve Test Framework 1.6.0:** version 1.1.0 declared that unpublished dependency. Upgrade to 1.1.1 or the current stable channel.
- **UIElements namespace is missing after importing a sample:** restore the built-in UI Toolkit module in your project's Package Manager. Core-only consumers need no UI module.

Continue with the [quick start](quick-start.md).
