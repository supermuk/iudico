using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurrMgt.Models.Storage
{
    public enum CurriculumStorageType
    {
        Database = 1,
        FileSystem,
        Mixed
    }

    public class CurriculumStorageFactory
    {
        public static ICurriculumStorage CreateStorage(CurriculumStorageType type)
        {
            switch (type)
            {
                case CurriculumStorageType.Database:
                    throw new NotImplementedException();
                case CurriculumStorageType.FileSystem:
                    throw new NotImplementedException();
                case CurriculumStorageType.Mixed:
                    return new MixedCurriculumStorage();
                default:
                    throw new Exception("Can't create storage of such type");
            }
        }
    }
}