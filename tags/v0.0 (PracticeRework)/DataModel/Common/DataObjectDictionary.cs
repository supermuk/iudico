using System.Collections;
using System.Collections.Generic;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    public class DataObjectDictionary<TDataObject> : Dictionary<int, TDataObject>
        where TDataObject : IIntKeyedDataObject
    {
        public DataObjectDictionary(IEnumerable<TDataObject> objs)
        {
            foreach (var o in objs)
            {
                Add(o.ID, o);
            }
        }

        public DataObjectDictionary(IEnumerable objs)
        {
            foreach (IIntKeyedDataObject o in objs)
            {
                Add(o.ID, (TDataObject)o);
            }
        }
    }

    public class DataObjectDictionary : DataObjectDictionary<IIntKeyedDataObject>
    {
        public DataObjectDictionary(IEnumerable<IIntKeyedDataObject> objs)
            : base(objs)
        {
        }

        public DataObjectDictionary(IEnumerable objs)
            : base(objs)
        {
        }
    }
}
