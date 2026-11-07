# Performance and verification

## Use batches where the source supports them

List AddRange/InsertRange/RemoveRange and Queue/Stack range operations emit a batch rather than repeated single-item events. List/Queue/Stack sync forwards the supported batches. Empty batches are ignored. Dictionary and HashSet do not expose public range mutation methods.

Batch operations reduce dispatch work; they do not imply zero allocation. `SplitByEvents` turns a single-item payload into a one-element array, and the core event API accepts an event-args interface. Copying retained batch data has its own cost.

## Measure your actual chain

FilteredList can update direct-source edits incrementally, but explicit Update, reset and downstream filtered chains can rebuild the index map. Comparers, converters, UI creation and cleanup callbacks are application work. Measure them with the size and access pattern of your real data.

Do not infer benchmark numbers from the interactive website illustration. It runs in JavaScript and explains behavior, not .NET timing.

## Run the repository tests

```bash
dotnet test DotnetTests/Aspid.Collections.Tests.csproj --configuration Release
```

For Unity, open the development project in `Aspid.Collections/` and run PlayMode suites in Test Runner. The project version is 6000.6.5f1; the package minimum remains 2021.3.0f1.

Performance suites live under `Tests/Runtime/Observable/Performance/`. They require Unity Test Framework and `com.unity.test-framework.performance`; without the performance package their asmdef reference will not resolve when tests compile. `ASPID_COLLECTIONS_PERFORMANCE_TESTING` is a code-level version define, not an assembly-level fallback. Core-only consumers do not need to import tests.

## Keep API documentation current

The API section is generated from the runtime's public/protected C# surface with DocFX. Run `npm --prefix Website run api` after changing that surface, then build both locales. The generation project has no Unity dependencies.
