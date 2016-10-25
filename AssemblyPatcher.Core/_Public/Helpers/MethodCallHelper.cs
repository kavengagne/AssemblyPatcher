using System;
using System.Diagnostics;
using System.Reflection;


namespace AssemblyPatcher.Core.Helpers
{
    public static class MethodCallHelper
    {
        public static MethodBase GetDebugWriteLineCall()
        {
            return typeof(Debug).GetMethod("WriteLine", new[] { typeof(string), typeof(object[]) });
        }

        public static MethodBase GetConsoleWriteLineCall()
        {
            return typeof(Console).GetMethod("WriteLine", new[] { typeof(string), typeof(object[]) });
        }
    }
}