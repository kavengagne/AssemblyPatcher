using System;


namespace AssemblyPatcher.Assets
{
    public class PatchAttribute : Attribute
    {
        public string PatchName { get; set; }
        public int FromIndex { get; set; }
        public int InstructionsCount { get; set; }

        public PatchAttribute(string patchName, int fromIndex, int instructionsCount)
        {
            PatchName = patchName;
            FromIndex = fromIndex;
            InstructionsCount = instructionsCount;
        }
    }
}