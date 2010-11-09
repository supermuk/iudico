using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEditor.Models.Storage
{
    public enum StorageType
    {
        Database = 1,
        FileSystem,
        Mixed
    }

    public class StorageFactory
    {
        public IStorageInterface CreateStorage(StorageType type)
        {
            switch (type)
            {
                case StorageType.Database:
                    throw new NotImplementedException();
                    break;
                case StorageType.FileSystem:
                    throw new NotImplementedException();
                    break;
                case StorageType.Mixed:
                    return new MixedStorage();
                    break;
                default:
                    throw new Exception("Can't create storage of such type");
                    break;
            }
        }
    }
}