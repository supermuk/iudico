using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models.Storages
{
    public interface ISecurityStorage
    {
        void CreateUserActivity(UserActivity userActivity);
        IEnumerable<UserActivity> GetUserActivities();
    }
}
