using System;
using System.Collections;
using System.Collections.Generic;

namespace Zilla.Xtructs.Ext
{
    public static class IEnumerableExt
    {
        public static IEnumerable<T> Evens<T>(this IEnumerable<T> @this)
        {
            bool evenFlag = true;

            foreach(T item in @this)
            {
                if (evenFlag) yield return item;

                evenFlag = !evenFlag;
            }
        }

        public static IEnumerable<T> Odds<T>(this IEnumerable<T> @this)
        {
            bool oddFlag = true;

            foreach (T item in @this)
            {
                if (oddFlag) yield return item;

                oddFlag = !oddFlag;
            }
        }

        public static bool TryFindIndex<T>(this List<T> list, Predicate<T> match, out int index)
        {
            index = list.FindIndex(match);
            return index == -1;
        }

        public static void AddSorted<T>(this List<T> list, T item)
            where T : IComparable<T>
        {
            int i = 0;
            foreach(var t in list)
            {
                if (item.CompareTo(t) >= 0) break;
                i++;
            }

            list.Insert(i, item);
        }
    }
}
