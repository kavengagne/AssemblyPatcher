using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AssemblyPatcher.Core.Base;
using dnlib.DotNet;


namespace AssemblyPatcher.Core
{
    public class AssemblyEditor
    {
        private readonly ModuleDefMD _module;
        private Importer _importer;
        private readonly List<Type> _patchers;
        private readonly ModuleContext _context;


        #region Constructors
        public AssemblyEditor(string modulePath) : this()
        {
            _module = ModuleDefMD.Load(modulePath);
            AfterConstructor();
        }

        public AssemblyEditor(Module module) : this()
        {
            _module = ModuleDefMD.Load(module);
            AfterConstructor();
        }

        public AssemblyEditor(Stream moduleStream) : this()
        {
            _module = ModuleDefMD.Load(moduleStream);
            AfterConstructor();
        }

        private AssemblyEditor()
        {
            var assemblyResolver = new AssemblyResolver();
            _context = new ModuleContext(assemblyResolver);
            assemblyResolver.DefaultModuleContext = _context;
            _patchers = GetPatchers();
        }

        private void AfterConstructor()
        {
            _module.Context = _context;
            _module.Context.AssemblyResolver.AddToCache(_module);
            _importer = new Importer(_module);
        }
        #endregion Constructors
        

        private static List<Type> GetPatchers()
        {
            var patchers = new List<Type>();
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var loadedAssembly in loadedAssemblies)
            {
                var types = loadedAssembly.ExportedTypes;
                patchers.AddRange(types.Where(t => t.BaseType?.Name == typeof(PatcherBase<,>).Name));
            }
            return patchers;
        }

        public TPatcher GetPatcher<TPatcher>()
            where TPatcher : class
        {
            var patcherType = _patchers.FirstOrDefault(p => p.Name == typeof(TPatcher).Name);
            if (patcherType != null)
            {
                return (TPatcher)Activator.CreateInstance(patcherType, _module, _importer);
            }
            return null;
        }

        public object GetPatcher(Type patcherType)
        {
            patcherType = _patchers.FirstOrDefault(p => p.Name == patcherType.Name);
            if (patcherType != null)
            {
                return Activator.CreateInstance(patcherType, _module, _importer);
            }
            return null;
        }

        public void Write(string filePath)
        {
            _module.Write(filePath);
        }
    }
}