using System.Collections.Generic;
using AssemblyPatcher.Core.Base;
using AssemblyPatcher.Core.Models;
using dnlib.DotNet;


namespace AssemblyPatcher.MethodPatcher
{
    // TODO: KG - Add support for Generic Methods
    public class MethodPatcher : PatcherBase<MethodSelector, IMethodPatch>
    {
        #region Constructors
        public MethodPatcher(ModuleDefMD module, Importer importer) : base(module, importer) { }
        #endregion Constructors


        protected override MethodSelector ApplyPatchesCore(TypeDef classTypeDef, List<Candidate> candidates)
        {
            return new MethodSelector(classTypeDef, candidates);
        }
    }
}
