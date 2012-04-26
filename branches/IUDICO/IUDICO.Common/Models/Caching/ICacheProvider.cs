using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IUDICO.Common.Models.Caching
{
    public interface ICacheProvider
    {
        void SetKeyPrefix(string keyPrefix);
        void Invalidate(params string[] tags);
        void Expire(string key);
        T Get<T>(string key, object @lock, Func<T> selector, DateTime absoluteExpiration, params string[] tags);
    }
}
