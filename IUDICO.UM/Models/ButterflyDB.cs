using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.UM.Models
{
    public class ButterflyDB : ButterflyDataContext
    {
        protected static readonly ButterflyDB instance;

        protected ButterflyDB()
            : base()
        {
        }

        static ButterflyDB()
        {
            instance = new ButterflyDB();
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