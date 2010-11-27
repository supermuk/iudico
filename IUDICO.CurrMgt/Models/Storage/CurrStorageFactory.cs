using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurrMgt.Models.Storage
{
    public enum CurrStorageType
    {
        Database = 1,
        FileSystem,
        Mixed
    }

    public class CurrStorageFactory
    {
        public static ICurrStorage CreateStorage(CurrStorageType type)
        {
            switch (type)
            {
                case CurrStorageType.Database:
                    throw new NotImplementedException();
                case CurrStorageType.FileSystem:
                    throw new NotImplementedException();
                case CurrStorageType.Mixed:
                    return new MixedCurrStorage();
                default:
                    throw new Exception("Can't create storage of such type");
            }
        }
    }
}