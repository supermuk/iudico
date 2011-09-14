using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    public static class CacheUtility
    {
        public static void InvalidateObject<T>(this Cache c, object id)
            where T : class
        {
            c.Remove(GetKey<T>(id));
        }

        public static T Get<T>(this Cache c, object id)
            where T : class
        {
            T result;
            if (TryGet(c, id, out result))
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException(string.Format(Translations.CacheUtility_Get_Object__0__with_ID__1__not_found_in_the_Cache, typeof(T).Name, id));
            }
        }

        public static bool TryGet<T>(this Cache c, object id, out T res)
            where T: class
        {
            res = (T)HttpRuntime.Cache.Get(GetKey<T>(id));
            return res != null;
        }

        public static void Add<T>(this Cache c, T obj)
            where T : class, IIntKeyedDataObject
        {
            c.Add(obj, obj.ID);
        }

        public static void Add<T>(this Cache c, T obj, object id)
            where T : class
        {
            Add(c, obj, id, CacheItemPriority.Normal, null);
        }

        public static void Add<T>(this Cache c, T obj, object id, CacheItemPriority priority, CacheItemRemovedCallback onRemoved)
            where T : class
        {
            c.Add(GetKey<T>(id), obj, null, DateTime.MaxValue, DEFAULT_EXPIRATION, priority, onRemoved);
        }

        public static void Remove<T>(this Cache c, T obj)
            where T : class
        {
            c.Remove(GetKey<T>(obj));
        }

        public static string GetKey<T>(object id)
            where T : class
        {
            return typeof (T).Name + "_" + id;
            
        }

        private static readonly TimeSpan DEFAULT_EXPIRATION = TimeSpan.FromMinutes(30);
    }
}
