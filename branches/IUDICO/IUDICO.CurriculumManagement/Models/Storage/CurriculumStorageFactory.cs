using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CurriculumManagement.Models.Storage
{
    [Obsolete("Use ICurriculumManagement")]
    public enum CurriculumStorageType
    {
        Database = 1,
        FileSystem,
        Mixed
    }

    [Obsolete("Use ICurriculumManagement")]
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
                    throw new NotImplementedException();
                default:
                    throw new Exception("Can't create storage of such type");
            }
        }
    }
}