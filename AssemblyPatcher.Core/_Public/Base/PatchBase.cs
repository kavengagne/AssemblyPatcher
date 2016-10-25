using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyPatcher.Assets;
using AssemblyPatcher.Core.Extensions;
using AssemblyPatcher.Core.Models;
using dnlib.DotNet;
using dnlib.DotNet.Emit;


namespace AssemblyPatcher.Core.Base
{
    public abstract class PatchBase : IPatch
    {
        private readonly InjectionPoint _injectionPoint;
        private readonly string _patchName;


        protected PatchBase(InjectionPoint injectionPoint)
        {
            _patchName = GetType().Name;
            _injectionPoint = injectionPoint;
        }


        public virtual bool CanApply(ModuleDefMD module, MethodDef method)
        {
            return true;
        }

        protected abstract List<Instruction> ApplyCore(ModuleDefMD module, Importer importer, MethodDef method);

        public bool Apply(ModuleDefMD module, Importer importer, MethodDef method)
        {
            if (CanApply(module, method) && method.Body.HasInstructions)
            {
                var instructions = ApplyCore(module, importer, method);
                var opcodesRange = InjectInstructionBlock(method, instructions, _injectionPoint);
                if (opcodesRange != null)
                {
                    AddPatchAttributeToMethod(module, method, opcodesRange);
                    return true;
                }
            }
            return false;
        }

        private void AddPatchAttributeToMethod(ModuleDef module, IHasCustomAttribute target, Tuple<int, int> opcodesRange)
        {
            var patchAttribute = module.Types.FirstOrDefault(t => t.FullName == typeof(PatchAttribute).FullName);
            if (patchAttribute != null)
            {
                target.CustomAttributes.Add(new CustomAttribute(patchAttribute.FindMethod(".ctor"), new List<CAArgument>
                {
                    new CAArgument(module.CorLibTypes.String, _patchName),
                    new CAArgument(module.CorLibTypes.Int32, opcodesRange.Item1),
                    new CAArgument(module.CorLibTypes.Int32, opcodesRange.Item2)
                }));
            }
        }

        // TODO: KG - Add peverify call after modification
        // TODO: KG - Make InjectionPoints extensible
        private static Tuple<int, int> InjectInstructionBlock(MethodDef method, List<Instruction> instructions, InjectionPoint injectionPoint)
        {
            Tuple<int, int> range = null;
            if (injectionPoint == InjectionPoint.Prefix)
            {
                range = InjectPrefixBlock(method, instructions);
            }
            if (injectionPoint == InjectionPoint.Postfix)
            {
                range = InjectPostfixBlock(method, instructions);
            }
            method.Body.Instructions.UpdateInstructionOffsets();
            return range;
        }

        private static Tuple<int, int> InjectPrefixBlock(MethodDef method, List<Instruction> instructions)
        {
            method.Body.Instructions.InjectAtIndex(0, instructions);
            return new Tuple<int, int>(0, instructions.Count);
        }

        private static Tuple<int, int> InjectPostfixBlock(MethodDef method, IList<Instruction> instructions)
        {
            var methodInstructions = method.Body.Instructions;
            var insertIndex = methodInstructions.Count - 1;
            methodInstructions.InjectAtIndex(insertIndex, instructions);
            return new Tuple<int, int>(insertIndex, instructions.Count);
        }
    }
}
