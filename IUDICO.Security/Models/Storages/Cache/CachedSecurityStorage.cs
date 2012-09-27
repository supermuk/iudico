using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models.Storages.Cache
{
    public class CachedSecurityStorage : ISecurityStorage
    {
        private readonly ISecurityStorage storage;
        private readonly ICacheProvider cacheProvider;
        private readonly object lockObject = new object();

        public CachedSecurityStorage(ISecurityStorage storage, ICacheProvider cacheProvider)
        {
            this.storage = storage;
            this.cacheProvider = cacheProvider;
        }

        public void CreateUserActivity(UserActivity userActivity)
        {
            this.storage.CreateUserActivity(userActivity);

            this.cacheProvider.Expire("useractivities");
        }

        public IEnumerable<UserActivity> GetUserActivities()
        {
            return this.cacheProvider.Get<IEnumerable<UserActivity>>("useractivities", @lockObject, () => this.storage.GetUserActivities().ToList(), DateTime.Now.AddDays(1));
        }
    }
}