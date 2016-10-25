using System.Collections.Generic;
using AssemblyPatcher.Core.Base;
using AssemblyPatcher.Core.Helpers;
using AssemblyPatcher.Core.Models;
using AssemblyPatcher.MethodPatcher;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using static AssemblyPatcher.Core.Helpers.InstructionHelper;


namespace AssemblyPatch.Logging.Patches
{
    public class LogMethodResultPatch : PatchBase, IMethodPatch
    {
        public LogMethodResultPatch() : base(InjectionPoint.Postfix) { }
        
        public override bool CanApply(ModuleDefMD module, MethodDef method)
        {
            return method.HasReturnType;
        }

        protected override List<Instruction> ApplyCore(ModuleDefMD module, Importer importer, MethodDef method)
        {
            var instructions = GetInstructions(module, importer, method);
            return instructions;
        }
        
        private static List<Instruction> GetInstructions(ModuleDef module, Importer importer, MethodDef method)
        {
            var writeLine = importer.Import(MethodCallHelper.GetDebugWriteLineCall());
            var resultLocal = new Local(method.ReturnType);
            method.Body.Variables.Add(resultLocal);

            var instructions = new List<Instruction>();
            instructions.Add(Op(OpCodes.Stloc, resultLocal));
            instructions.Add(Op(OpCodes.Ldstr, "Result Type: {0}, Value: {1}"));
            instructions.Add(Op(OpCodes.Ldc_I4, 2));
            instructions.Add(Op(OpCodes.Newarr, module.CorLibTypes.Object.TypeRef));
            instructions.Add(Op(OpCodes.Dup));
            instructions.Add(Op(OpCodes.Ldc_I4, 0));
            instructions.Add(Op(OpCodes.Ldstr, method.ReturnType.FullName));
            instructions.Add(Op(OpCodes.Stelem_Ref));
            instructions.Add(Op(OpCodes.Dup));
            instructions.Add(Op(OpCodes.Ldc_I4, 1));
            instructions.Add(Op(OpCodes.Ldloc, resultLocal));
            instructions.Add(Op(OpCodes.Box, method.ReturnType.ToTypeDefOrRef()));
            instructions.Add(Op(OpCodes.Stelem_Ref));
            instructions.Add(Op(OpCodes.Call, writeLine));
            instructions.Add(Op(OpCodes.Ldloc, resultLocal));

            return instructions;
        }
    }
}
