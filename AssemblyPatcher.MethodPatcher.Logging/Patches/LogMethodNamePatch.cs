using System.Collections.Generic;
using System.Reflection;
using AssemblyPatcher.Core.Base;
using AssemblyPatcher.Core.Helpers;
using AssemblyPatcher.Core.Models;
using AssemblyPatcher.MethodPatcher;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using static AssemblyPatcher.Core.Helpers.InstructionHelper;


namespace AssemblyPatch.Logging.Patches
{
    // TODO: KG - Add a way to recognize if the patch has already been applied to the method
    // TODO: KG - Support Patch Removal (also Add CanRemove virtual method
    // TODO: KG - Add start, end index to PatchAttribute for patch removal
    public class LogMethodNamePatch : PatchBase, IMethodPatch
    {
        public LogMethodNamePatch() : base(InjectionPoint.Prefix) { }
        
        protected override List<Instruction> ApplyCore(ModuleDefMD module, Importer importer, MethodDef method)
        {
            var instructions = GetInstructions(module, importer);
            return instructions;
        }
        
        private static List<Instruction> GetInstructions(ModuleDef module, Importer importer)
        {
            var writeLine = importer.Import(MethodCallHelper.GetDebugWriteLineCall());
            var getCurrentMethod = importer.Import(GetCurrentMethodCall());
            var getNameMethod = importer.Import(typeof(MemberInfo).GetMethod("get_Name"));

            var instructions = new List<Instruction>();
            instructions.Add(Op(OpCodes.Ldstr, "MethodName: {0}"));
            instructions.Add(Op(OpCodes.Ldc_I4, 1));
            instructions.Add(Op(OpCodes.Newarr, module.CorLibTypes.Object.TypeRef));
            instructions.Add(Op(OpCodes.Dup));
            instructions.Add(Op(OpCodes.Ldc_I4, 0));
            instructions.Add(Op(OpCodes.Call, getCurrentMethod));
            instructions.Add(Op(OpCodes.Callvirt, getNameMethod));
            instructions.Add(Op(OpCodes.Stelem_Ref));
            instructions.Add(Op(OpCodes.Call, writeLine));

            return instructions;
        }

        private static MethodBase GetCurrentMethodCall()
        {
            var getCurrentMethod = typeof(MethodBase).GetMethod("GetCurrentMethod");
            return getCurrentMethod;
        }
    }
}
