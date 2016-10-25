using dnlib.DotNet.Emit;


namespace AssemblyPatcher.Core.Helpers
{
    public static class InstructionHelper
    {
        public static Instruction Op(OpCode opcode, object operand = null)
        {
            if (operand == null)
            {
                return new Instruction(opcode);
            }
            return new Instruction(opcode, operand);
        }
    }
}