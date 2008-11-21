using System;
using System.Reflection;
using System.Text;
using LEX.CONTROLS;
using System.Collections.Generic;

namespace IUDICO.DataModel.Common
{
    public static class Utils
    {
        public static bool HasAtr<T>([NotNull] this ICustomAttributeProvider target)
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
            var s = new StringBuilder();
            foreach (var i in data)
            {
                if (s.Length > 0)
                {
                    s.Append(",");
                }
                s.Append(i);
            }
            return s.ToString();
        }
    }
}
