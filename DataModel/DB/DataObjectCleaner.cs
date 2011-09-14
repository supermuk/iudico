using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.DB
{
    public class DataObjectCleaner : IDisposable
    {
        public void Dispose()
        {
            var errors = new List<Exception>();
            foreach (var o in _DOs)
            {
                try
                {
                    var m = DatabaseModel.DELETE_METHOD.MakeGenericMethod(new[] {o.GetType()});
                    m.Invoke(ServerModel.DB, new object[] {o.ID});
                }
                catch(Exception e)
                {
                    errors.Add(e);
                }
            }
            if (errors.Count > 0)
            {
                throw new DMError(errors.Select(e => e.Message).ConcatSeparator(Environment.NewLine));
            }
        }

        public int Insert<TDataObject>(TDataObject obj)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            var res = ServerModel.DB.Insert(obj);
            RegisterForDelete(obj);
            return res;
        }

        public void Insert<TDataObject>(IList<TDataObject> objs)
            where TDataObject : class, IIntKeyedDataObject, new()
        {
            ServerModel.DB.Insert(objs);
        }

        protected void RegisterForDelete(IIntKeyedDataObject obj)
        {
            _DOs.Add(obj);
        }

        private readonly List<IIntKeyedDataObject> _DOs = new List<IIntKeyedDataObject>();
    }
}
