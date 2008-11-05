using System;
using System.Reflection;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Common
{
    public static class Utils
    {
        public static T GetAtr<T>([NotNull] this ICustomAttributeProvider target)
            where T: Attribute
        {
            return (T) target.Atr<T>();
        }
    }
}
