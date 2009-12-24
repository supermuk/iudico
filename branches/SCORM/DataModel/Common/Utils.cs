using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public interface ICloneble<T>
        where T: class
    {
        T Clone();
    }

    public static class Utils
    {
        public static bool HasAtr<T>([NotNull] this ICustomAttributeProvider target)
            where T: Attribute
        {
            return target.GetCustomAttributes(typeof (T), true).Length == 1;
        }

        [NotNull]
        public static T GetAtr<T>([NotNull] this ICustomAttributeProvider target)
            where T: Attribute
        {
            return (T) target.Atr<T>();
        }

        public static bool TryGetAtr<T>(this ICustomAttributeProvider target, out T res)
            where T: Attribute
        {
            var ats = target.GetCustomAttributes(typeof (T), true);
            if (ats.Length > 1)
            {
                throw new InvalidOperationException("Too many attributes");
            }
            if (ats.Length == 0)
            {
                res = null;
                return false;
            }
            else
            {
                res = (T) ats[0];
                return true;
            }
        }

        public static string ConcatComma<T>([NotNull] this IEnumerable<T> data)
        {
            return data.ConcatSeparator(",");
        }

        public static string ConcatSeparator<T>([NotNull] this IEnumerable<T> data, [NotNull] string separator)
        {
            var s = new StringBuilder();
            foreach (var i in data)
            {
                if (s.Length > 0)
                {
                    s.Append(separator);
                }
                s.Append(i);
            }
            return s.ToString();                        
        }

        public static void RemoveDuplicates<T>([NotNull] this List<T> list)
            where T : IComparable<T>
        {
            list.Sort();
            for (int i = list.Count - 1; i > 0; --i)
            {
                if (list[i].CompareTo(list[i - 1]) == 0)
                {
                    list.RemoveAt(i);
                }
            }
        }
    }
}
