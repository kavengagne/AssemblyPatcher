using System.Collections.Generic;
using System.Text;
using AssemblyPatcher.Core.Base;
using AssemblyPatcher.Core.Helpers;
using AssemblyPatcher.Core.Models;
using AssemblyPatcher.MethodPatcher;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using static AssemblyPatcher.Core.Helpers.InstructionHelper;


namespace AssemblyPatch.Logging.Patches
{
    public class LogMethodParametersPatch : PatchBase, IMethodPatch
    {
        public LogMethodParametersPatch() : base(InjectionPoint.Prefix) { }
        
        public override bool CanApply(ModuleDefMD module, MethodDef method)
        {
            return method.HasParamDefs;
        }

        protected override List<Instruction> ApplyCore(ModuleDefMD module, Importer importer, MethodDef method)
        {
            var instructions = GetInstructions(module, importer, method);
            return instructions;
        }
        
        private static List<Instruction> GetInstructions(ModuleDef module, Importer importer, MethodDef method)
        {
            var writeLine = importer.Import(MethodCallHelper.GetDebugWriteLineCall());

            var instructions = new List<Instruction>();

            instructions.Add(Op(OpCodes.Ldstr, GetParametersFormatString(method)));
            instructions.Add(Op(OpCodes.Ldc_I4, method.ParamDefs.Count));
            instructions.Add(Op(OpCodes.Newarr, module.CorLibTypes.Object.TypeRef));

            for (var index = 0; index < method.ParamDefs.Count; index++)
            {
                var parameter = method.Parameters[index + (method.IsStatic ? 0 : 1)];
                instructions.Add(Op(OpCodes.Dup));
                instructions.Add(Op(OpCodes.Ldc_I4, index));
                instructions.Add(Op(OpCodes.Ldarg, parameter));
                if (parameter.Type.IsValueType)
                {
                    instructions.Add(Op(OpCodes.Box, parameter.Type.ToTypeDefOrRef()));
                }
                instructions.Add(Op(OpCodes.Stelem_Ref));
            }
            instructions.Add(Op(OpCodes.Call, writeLine));

            return instructions;
        }

        private static string GetParametersFormatString(MethodDef method)
        {
            var format = new StringBuilder("Parameters:");
            for (var index = 0; index < method.ParamDefs.Count; index++)
            {
                format.AppendFormat("\n\t{0} = {{{1}}}", method.Parameters[index + (method.IsStatic ? 0 : 1)].Name, index);
            }
            return format.ToString();
        }
    }
}