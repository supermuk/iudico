using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CourseManagment.Models.Storage
{
    public enum CourseStorageType
    {
        Database = 1,
        FileSystem,
        Mixed
    }

    [Obsolete("Use Windsor instead")]
    public class CourseStorageFactory
    {
        public static ICourseStorage CreateStorage(CourseStorageType type)
        {
            throw new NotSupportedException("This factory is obsolete");
            switch (type)
            {
                case CourseStorageType.Database:
                    throw new NotImplementedException();
                case CourseStorageType.FileSystem:
                    throw new NotImplementedException();
                //case CourseStorageType.Mixed:
                    //return new MixedCourseStorage();
                default:
                    throw new Exception("Can't create storage of such type");
            }
        }
    }
}