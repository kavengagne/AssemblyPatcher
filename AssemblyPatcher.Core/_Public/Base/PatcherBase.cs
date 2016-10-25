using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyPatcher.Assets;
using AssemblyPatcher.Core.Extensions;
using AssemblyPatcher.Core.Helpers;
using AssemblyPatcher.Core.Models;
using dnlib.DotNet;

// TODO: KG - Add logging to configurable file
namespace AssemblyPatcher.Core.Base
{
    public abstract class PatcherBase<TSelector, TPatch>
        where TPatch : IPatch
    {
        internal readonly ModuleDefMD Module;
        internal readonly Importer Importer;
        internal readonly List<TPatch> Patches = new List<TPatch>();
        internal readonly List<Candidate> Candidates = new List<Candidate>();


        #region Constructors
        protected PatcherBase(ModuleDefMD module, Importer importer)
        {
            Module = module;
            Importer = importer;
        }
        #endregion Constructors


        protected abstract TSelector ApplyPatchesCore(TypeDef classTypeDef, List<Candidate> candidates);


        public void AddPatch(TPatch patch)
        {
            Patches.Add(patch);
        }
        
        public TSelector AppliesTo(string className)
        {
            var classTypeDef = TypeHelper.GetTypeByFullName(Module, className);
            return ApplyPatchesCore(classTypeDef, Candidates);
        }

        public TSelector AppliesTo(Type classType)
        {
            var classTypeDef = TypeHelper.GetTypeByType(Module, classType);
            return ApplyPatchesCore(classTypeDef, Candidates);
        }

        public bool ApplyPatches()
        {
            bool patchesApplied = false;
            InjectAssets();
            foreach (var candidate in Candidates)
            {
                for (var index = Patches.Count - 1; index >= 0; index--)
                {
                    var patch = Patches[index];
                    if (!candidate.Method.HasPatch(patch))
                    {
                        var applied = patch.Apply(Module, Importer, candidate.Method);
                        patchesApplied |= applied;
                    }
                }
            }
            return patchesApplied;
        }

        // TODO: KG - Make this method search the types dynamically
        private void InjectAssets()
        {
            var assetType = typeof(PatchAttribute);
            if (!AssetTypeExists(assetType))
            {
                InjectAssetType(assetType);
            }
        }

        private bool AssetTypeExists(Type assetType)
        {
            var assetTypeExists = Module.Types.Any(t => t.FullName == assetType.FullName);
            return assetTypeExists;
        }

        private void InjectAssetType(Type assetType)
        {
            var assetsModule = ModuleDefMD.Load(assetType.Module);
            var assetTypeDef = assetsModule.Types.FirstOrDefault(t => t.FullName == assetType.FullName);
            if (assetTypeDef != null)
            {
                assetsModule.Types.Remove(assetTypeDef);
                Module.Types.Add(assetTypeDef);
            }
        }
    }
}