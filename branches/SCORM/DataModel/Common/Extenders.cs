using System.Collections.Generic;

namespace IUDICO.DataModel.Common
{
    /// <summary>
    /// Deprecated
    /// </summary>
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

        public static IEnumerable<T> NonNull<T>(this IEnumerable<T> collection)
            where T: class
        {
            foreach (var t in collection)
            {
                if (t != null)
                    yield return t;
            }
        }
    }
}
