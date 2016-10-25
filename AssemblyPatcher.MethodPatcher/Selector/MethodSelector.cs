using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyPatcher.Core.Base;
using AssemblyPatcher.Core.Models;
using dnlib.DotNet;


namespace AssemblyPatcher.MethodPatcher
{
    public class MethodSelector : SelectorBase
    {
        internal MethodSelector(TypeDef classTypeDef, List<Candidate> candidates) : base(classTypeDef, candidates) { }


        public void AppliesTo(params string[] methodNames)
        {
            AppliesTo(IsNameMatching(methodNames));
        }

        public void AppliesTo(params MethodInfo[] methodInfos)
        {
            AppliesTo(IsNameMatching(methodInfos));
        }


        private void AppliesTo(Func<MethodDef, bool> predicate)
        {
            var methods = ClassTypeDef.Methods.Where(predicate);
            foreach (var methodDef in methods)
            {
                Candidates.Add(new Candidate(methodDef));
            }
        }

        private static Func<MethodDef, bool> IsNameMatching(string[] methodNames)
        {
            return m => methodNames.Any(name => name == m.Name);
        }

        private static Func<MethodDef, bool> IsNameMatching(MethodInfo[] methodInfos)
        {
            return m => methodInfos.Any(inf => inf.Name == m.Name);
        }
    }
}