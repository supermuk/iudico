using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Interfaces;
using IUDICO.Common.Models;
using System.Data.Linq;

namespace IUDICO.Security.Models
{
    public partial class DBDataContext : ISecurityDataContext
    {
        partial void OnCreated()
        {
            DataLoadOptions opts = new DataLoadOptions();
            opts.LoadWith<Room>(r => r.Computers);
            
            LoadOptions = opts;
        }

        IMockableTable<Room> ISecurityDataContext.Rooms
        {
            get { return new MockableTable<Room>(Rooms); }
        }

        IMockableTable<Computer> ISecurityDataContext.Computers
        {
            get { return new MockableTable<Computer>(Computers); }
        }

        IMockableTable<UserActivity> ISecurityDataContext.UserActivities
        {
            get { return new MockableTable<UserActivity>(UserActivities); }
        }
    }
}