
####AssemblyPatcher

__PatcherBase__, __SelectorBase__

- Allows creation of custom patchers. (`MethodPatcher`, `PropertyPatcher`, `FieldPatcher`)
- Use `PatcherBase` as a starting point.
- Create a marker interface to make `AddPatch` type-safe.
- Create a Selector using `SelectorBase`to allow custom member selection.


__IPatch__, __PatchBase__

- Allows creation of custom patches for the Patchers. (`LogMethodNamePatch`, `LogMethodParametersPatch`).
- Use `PatchBase` as a starting point.


__Custom IPatch Example__

1. Declaration
```
public interface IMethodPatch : IPatch { }
public class LogMethodNamePatch : PatchBase, IMethodPatch { }
public class LogMethodParametersPatch : PatchBase, IMethodPatch { }
```

2. Usage
```
var methodPatcher = new MethodPatcher(typeof(Calculator).Module);
methodPatcher.AddPatch(new LogMethodNamePatch());
methodPatcher.AddPatch(new LogMethodParametersPatch());
methodPatcher.AppliesTo(typeof(Calculator)).AppliesTo("Add", "Sub");
methodPatcher.ApplyPatches();
```
