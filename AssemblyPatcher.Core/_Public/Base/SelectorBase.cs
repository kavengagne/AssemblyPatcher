using System.Collections.Generic;
using AssemblyPatcher.Core.Models;
using dnlib.DotNet;


namespace AssemblyPatcher.Core.Base
{
    public abstract class SelectorBase
    {
        protected TypeDef ClassTypeDef;
        protected List<Candidate> Candidates;

        protected SelectorBase(TypeDef classTypeDef, List<Candidate> candidates)
        {
            ClassTypeDef = classTypeDef;
            Candidates = candidates;
        }
    }
}