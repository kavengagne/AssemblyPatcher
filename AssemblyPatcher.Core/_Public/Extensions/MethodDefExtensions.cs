using System.Linq;
using AssemblyPatcher.Assets;
using AssemblyPatcher.Core.Base;
using dnlib.DotNet;


namespace AssemblyPatcher.Core.Extensions
{
    public static class MethodDefExtensions
    {
        public static bool HasPatch(this MethodDef method, IPatch patch)
        {
            var patchName = method.CustomAttributes
                                  .Where(a => a.TypeFullName == typeof(PatchAttribute).FullName)
                                  .Where(a => a.HasConstructorArguments)
                                  .Select(a => a.ConstructorArguments[0].Value.ToString()).ToList();

            return patchName.Any(name => name == patch.GetType().Name);
        }
    }
}