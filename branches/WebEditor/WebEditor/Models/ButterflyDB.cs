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
        protected static readonly ButterflyDB instance = new ButterflyDB();

        protected ButterflyDB() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ButterflyConnectionString"].ConnectionString)
        {
        }

        public static ButterflyDB Instance
        {
            get
            {
                return instance;
            }
        }
    }   
}