using dnlib.DotNet;


namespace AssemblyPatcher.Core.Base
{
    public interface IPatch
    {
        bool CanApply(ModuleDefMD module, MethodDef method);
        bool Apply(ModuleDefMD module, Importer importer, MethodDef method);
    }
}