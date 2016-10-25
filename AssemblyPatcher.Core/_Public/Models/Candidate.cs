using dnlib.DotNet;


namespace AssemblyPatcher.Core.Models
{
    public class Candidate
    {
        public MethodDef Method { get; set; }
        
        public Candidate(MethodDef method)
        {
            Method = method;
        }
    }
}