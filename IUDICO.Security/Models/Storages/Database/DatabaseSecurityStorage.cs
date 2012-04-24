using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models.Storages.Database
{
    public class DatabaseSecurityStorage : ISecurityStorage
    {
        private readonly Func<ISecurityDataContext> CreateIDataContext;

        public DatabaseSecurityStorage()
        {
            this.CreateIDataContext = () =>
                {
                    return new DBDataContext();
                };
        }

        public DatabaseSecurityStorage(Func<ISecurityDataContext> createIDataContext)
        {
            this.CreateIDataContext = createIDataContext;
        }

        #region ISecurityStorage
        public void CreateUserActivity(UserActivity userActivity)
        {
            using (var db = this.NewContext())
            {
                db.UserActivities.InsertOnSubmit(userActivity);
                db.SubmitChanges();
            }
        }

        public IEnumerable<UserActivity> GetUserActivities()
        {
            return this.NewContext().UserActivities;
        }

        #endregion

        protected ISecurityDataContext NewContext()
        {
            return this.CreateIDataContext();
        }
    }
}