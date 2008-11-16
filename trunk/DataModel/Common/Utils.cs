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
