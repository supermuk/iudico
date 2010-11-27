using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.CourseMgt.Models.Storage
{
    public enum CourseStorageType
    {
        Database = 1,
        FileSystem,
        Mixed
    }

    public class CourseStorageFactory
    {
        public static ICourseStorage CreateStorage(CourseStorageType type)
        {
            switch (type)
            {
                case CourseStorageType.Database:
                    throw new NotImplementedException();
                case CourseStorageType.FileSystem:
                    throw new NotImplementedException();
                case CourseStorageType.Mixed:
                    return new MixedCourseStorage();
                default:
                    throw new Exception("Can't create storage of such type");
            }
        }
    }
}