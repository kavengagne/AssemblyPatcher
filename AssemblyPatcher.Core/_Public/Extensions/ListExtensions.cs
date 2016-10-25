using System;
using System.Collections.Generic;


namespace AssemblyPatcher.Core.Extensions
{
    public static class ListExtensions
    {
        public static int IndexOf<TSource>(this IList<TSource> instructions, Func<TSource, bool> predicate)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                if (predicate.Invoke(instructions[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int LastIndexOf<TSource>(this IList<TSource> instructions, Func<TSource, bool> predicate)
        {
            for (var i = instructions.Count - 1; i >= 0; i--)
            {
                if (predicate.Invoke(instructions[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void InjectAtIndex<TSource>(this IList<TSource> items, int index, IList<TSource> newItems)
        {
            for (var i = newItems.Count - 1; i >= 0; i--)
            {
                items.Insert(index, newItems[i]);
            }
        }
    }
}