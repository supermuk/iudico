using System;
using System.Collections.Generic;

namespace IUDICO.DataModel.Common
{
    public class ComparableList<T> : List<T>, IComparable<ComparableList<T>>
        where T : IComparable<T>
    {
        public ComparableList()
        {
        }

        public ComparableList(int capacity)
            : base(capacity)
        {   
        }

        public ComparableList(IEnumerable<T> collection) 
            : base(collection)
        {
        }

        #region Implementation of IComparable<T>

        public int CompareTo(ComparableList<T> other)
        {
            if (other == null)
                return -1;
            var r = Count.CompareTo(other.Count);
            if (r != 0)
                return r;

            for (int i = 0; i < Count; i++)
            {
                r = this[i].CompareTo(other[i]);
                if (r != 0)
                    return r;
            }
            return 0;    
        }

        #endregion
    }
}
