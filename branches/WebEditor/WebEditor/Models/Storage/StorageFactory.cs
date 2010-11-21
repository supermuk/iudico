﻿using System;
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
        public static IStorageInterface CreateStorage(StorageType type)
        {
            switch (type)
            {
                case StorageType.Database:
                    throw new NotImplementedException();
                case StorageType.FileSystem:
                    throw new NotImplementedException();
                case StorageType.Mixed:
                    return new MixedStorage();
                default:
                    throw new Exception("Can't create storage of such type");
            }
        }
    }
}