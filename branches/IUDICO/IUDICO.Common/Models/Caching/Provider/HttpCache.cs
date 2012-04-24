using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;

namespace IUDICO.Common.Models.Caching.Provider
{
    public class HttpCache : ICacheProvider
    {
        protected string keyPrefix = string.Empty;
        protected Cache Cache
        {
            get { return HttpRuntime.Cache; }
        }

        public void SetKeyPrefix(string keyPrefix)
        {
            this.keyPrefix = keyPrefix;
        }

        public T Get<T>(string key, object @lock, Func<T> selector, DateTime absoluteExpiration, params string[] tags)
        {
            object value;

            if ((value = Cache.Get(key)) == null)
            {
                lock (@lock)
                {
                    if ((value = Cache.Get(key)) == null)
                    {
                        value = selector();

                        if (value == null)
                            return (T)value;

                        this.Cache.Insert(
                            key,
                            value,
                            this.CreateTagDependency(tags),
                            absoluteExpiration,
                            Cache.NoSlidingExpiration,
                            CacheItemPriority.Normal,
                            null);
                    }
                }
            }
            return (T)value;
        }

        protected CacheDependency CreateTagDependency(params string[] tags)
        {
            if (tags == null || tags.Length < 1)
                return null;

            long version = DateTime.UtcNow.Ticks;
            for (int i = 0; i < tags.Length; ++i)
            {
                Cache.Add(
                    "_tag:" + tags[i],
                    version,
                    null,
                    DateTime.MaxValue,
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable,
                    null);
            }
            
            return new CacheDependency(null, tags.Select(s => "_tag:" + s).ToArray());
        }

        public void Invalidate(params string[] tags)
        {
            long version = DateTime.UtcNow.Ticks;
            for (int i = 0; i < tags.Length; ++i)
            {
                Cache.Insert(
                    "_tag:" + tags[i],
                    version,
                    null,
                    DateTime.MaxValue,
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable,
                    null);
            }
        }

        public void Expire(string key)
        {
            Cache.Remove(key);
        }
    }
}
