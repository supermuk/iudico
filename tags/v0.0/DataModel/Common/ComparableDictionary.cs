using System;
using System.Collections.Generic;

namespace IUDICO.DataModel.Common
{
    public class ComparableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IComparable<ComparableDictionary<TKey, TValue>>
    {
        #region Implementation of IComparable<ComparableDictionary<TKey,TValue>>

        public int CompareTo(ComparableDictionary<TKey, TValue> other)
        {
            // TODO: Implement it. All dictionaries are different for now
            return -1;
        }

        #endregion
    }
}
