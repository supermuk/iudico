using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Security.Models.Storages.Cache
{
    public class CachedSecurityStorage: ISecurityStorage
    {
        private readonly ISecurityStorage _storage;
        private readonly ICacheProvider _cacheProvider;
        private readonly object lockObject = new object();

        public CachedSecurityStorage(ISecurityStorage storage, ICacheProvider cacheProvider)
        {
            _storage = storage;
            _cacheProvider = cacheProvider;
        }

        public void CreateUserActivity(UserActivity userActivity)
        {
            _storage.CreateUserActivity(userActivity);

            _cacheProvider.Expire("useractivities");
        }

        public IEnumerable<UserActivity> GetUserActivities()
        {
            return _cacheProvider.Get<IEnumerable<UserActivity>>("useractivities", @lockObject, () => _storage.GetUserActivities(), DateTime.Now.AddDays(1));
        }
    }
}