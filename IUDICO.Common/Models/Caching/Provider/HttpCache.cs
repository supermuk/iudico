using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;

namespace IUDICO.Common.Models.Caching.Provider
{
    public class HttpCache: ICacheProvider
    {
        protected string _keyPrefix = "";
        protected Cache _cache
        {
            get { return HttpRuntime.Cache; }
        }

        public void SetKeyPrefix(string keyPrefix)
        {
            _keyPrefix = keyPrefix;
        }

        public T Get<T>(string key, object @lock, Func<T> selector, DateTime absoluteExpiration, params string[] tags)
        {
            object value;

            if ((value = _cache.Get(key)) == null)
            {
                lock (@lock)
                {
                    if ((value = _cache.Get(key)) == null)
                    {
                        value = selector();

                        if (value == null)
                            return (T)value;

                        _cache.Insert(key, value, CreateTagDependency(tags),
                            absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
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
                _cache.Add("_tag:" + tags[i], version, null,
                  DateTime.MaxValue, Cache.NoSlidingExpiration,
                  CacheItemPriority.NotRemovable, null);
            }
            
            return new CacheDependency(null, tags.Select(s => "_tag:" + s).ToArray());
        }

        public void Invalidate(params string[] tags)
        {
            long version = DateTime.UtcNow.Ticks;
            for (int i = 0; i < tags.Length; ++i)
            {
                _cache.Insert("_tag:" + tags[i], version, null,
                  DateTime.MaxValue, Cache.NoSlidingExpiration,
                  CacheItemPriority.NotRemovable, null);
            }
        }

        public void Expire(string key)
        {
            _cache.Remove(key);
        }
    }
}
