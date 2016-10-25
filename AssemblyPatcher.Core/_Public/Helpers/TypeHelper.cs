using System;
using System.Linq;
using dnlib.DotNet;


namespace AssemblyPatcher.Core.Helpers
{
    public static class TypeHelper
    {
        public static TypeDef GetTypeByFullName(ModuleDef module, string fullName)
        {
            var types = module.Types;
            return types.FirstOrDefault(def => def.FullName == fullName);
        }

        public static TypeDef GetTypeByType(ModuleDef module, Type classType)
        {
            var types = module.Types;
            return types.FirstOrDefault(def => def.FullName == classType.FullName);
        }
    }
}