using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.TS.Models.Shared
{
    public class Package
    {
        #region Public Properties

        public long GID { get; set; }

        public String Location { get; set; }

        public String Title { get; set; }

        #endregion

        #region Constructors

        public Package(long gid, string location, string title)
        {
            this.GID = gid;
            this.Location = location;
            this.Title = title;
        }

        #endregion
    }
}