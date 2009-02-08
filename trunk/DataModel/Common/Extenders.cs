using System.Collections.Generic;

namespace IUDICO.DataModel.Common
{
    public static class Extenders
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> collection, T v)
        {
            foreach (var t in collection)
            {
                yield return t;
            }
            yield return v;
        }
    }
}
