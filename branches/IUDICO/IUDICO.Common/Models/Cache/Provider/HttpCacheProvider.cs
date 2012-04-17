using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IUDICO.Common.Models.Cache.Provider
{
    class HttpCacheProvider: ICacheProvider
    {
        private static readonly object CacheLockObject = new object();
        protected string _keyPrefix = "";

        public void SetKeyPrefix(string keyPrefix)
        {
            _keyPrefix = keyPrefix;
        }

        public object this[string key]
        {
            get
            {
                return HttpRuntime.Cache[_keyPrefix + "-" + key];
            }

            set
            {
                lock (CacheLockObject)
                {
                    if (this[_keyPrefix + "-" + key] == null)
                        HttpRuntime.Cache.Insert(_keyPrefix + "-" + key, value, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
                }
            }
        }

        public void Update(string key, object value)
        {
            lock (CacheLockObject)
            {
                HttpRuntime.Cache.Insert(_keyPrefix + "-" + key, value, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
        }

        public void Expire(string key)
        {
            lock (CacheLockObject)
            {
                HttpRuntime.Cache.Remove(_keyPrefix + "-" + key);
            }
        }

        public List<string> GetTagList(string keyTag)
        {
            return (List<string>)HttpRuntime.Cache["_tags-" + _keyPrefix + "-" + keyTag] ?? new List<string>();
        }

        protected void UpdateTagList(string keyTag, List<string> tagList)
        {
            HttpRuntime.Cache.Insert("_tags-" + _keyPrefix + "-" + keyTag, tagList, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
        }

        public void AddTag(string keyTag, string key)
        {
            lock (CacheLockObject)
            {
                var tags = GetTagList(keyTag);
                tags.Add(key);

                UpdateTagList(keyTag, tags);
            }
        }

        public void DeleteTag(string keyTag, string key)
        {
            lock (CacheLockObject)
            {
                var tags = GetTagList(keyTag);
                tags.Remove(key);

                UpdateTagList(keyTag, tags);
            }
        }

        public void UpdateTag(string keyTag, List<string> tags)
        {
            lock (CacheLockObject)
            {
                UpdateTagList(keyTag, tags);
            }
        }

        public void ExpireTag(string keyTag)
        {
            lock (CacheLockObject)
            {
                var tags = GetTagList(keyTag);

                foreach (var key in tags)
                {
                    Expire(key);
                }

                //UpdateTagList(keyTag, null);
            }
        }
    }
}
