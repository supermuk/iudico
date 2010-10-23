using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace WebEditor.Models
{
    public class ButterflyDB: ButterflyDataContext
    {
        protected ButterflyDB() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ButterflyConnectionString"].ConnectionString)
        {
        }

        private sealed class SingletonActivator
        {
            private static readonly ButterflyDB instance = new ButterflyDB();
            public static ButterflyDB Instance
            {
                get
                {
                    //return new ButterflyDB();
                    return instance;
                }
            }
        }
        public static ButterflyDB Instance
        {
            get
            {
                return SingletonActivator.Instance;
            }
        }

        public int AddCourse(Course course)
        {
            course.Created = DateTime.Now;
            course.Updated = DateTime.Now;
            base.Courses.InsertOnSubmit(course);
            base.SubmitChanges();
            return course.Id;
        }
    }
}