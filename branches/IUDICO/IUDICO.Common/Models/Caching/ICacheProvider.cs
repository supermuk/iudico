using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Caching
{
    public interface ICacheProvider
    {
        //private static readonly object CacheLockObject = new object();
        /*
        object this[string key]
        {
            get;
            set;
        }

        void SetKeyPrefix(string keyPrefix);

        void Update(string key, object value);
        void Expire(string key);

        void AddTag(string keyTag, string key);
        void DeleteTag(string keyTag, string key);
        void UpdateTag(string keyTag, List<string> tags);
        void ExpireTag(string keyTag);
        List<string> GetTagList(string keyTag);
        */

        void SetKeyPrefix(string keyPrefix);
        void Invalidate(params string[] tags);
        void Expire(string key);
        T Get<T>(string key, object @lock, Func<T> selector, DateTime absoluteExpiration, params string[] tags);
    }
}
