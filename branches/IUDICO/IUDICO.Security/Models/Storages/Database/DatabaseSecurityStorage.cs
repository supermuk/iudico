using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models.Storages.Database
{
    public class DatabaseSecurityStorage : ISecurityStorage
    {
        private readonly Func<ISecurityDataContext> _CreateIDataContext;

        public DatabaseSecurityStorage()
        {
            _CreateIDataContext = () =>
                {
                    return new DBDataContext();
                };
        }

        public DatabaseSecurityStorage(Func<ISecurityDataContext> createIDataContext)
        {
            _CreateIDataContext = createIDataContext;
        }

        #region ISecurityStorage
        public void CreateUserActivity(UserActivity userActivity)
        {
            using (var db = NewContext())
            {
                db.UserActivities.InsertOnSubmit(userActivity);
                db.SubmitChanges();
            }
        }

        public IEnumerable<UserActivity> GetUserActivities()
        {
            return NewContext().UserActivities;
        }

        #endregion

        protected ISecurityDataContext NewContext()
        {
            return _CreateIDataContext();
        }
    }
}