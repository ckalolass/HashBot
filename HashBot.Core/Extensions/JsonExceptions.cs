using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashBot.Core.Extensions
{
    public static class JsonExceptions
    {
        public static T GetValueOrDefault<T>(this JToken jtoken, String key)
        {
            var v = jtoken[key];
            if (v != null)
                return v.Value<T>();
            return default(T);
        }

        public static T GetValueOrDefault<T>(this JToken jtoken, String key1, String key2)
        {
            var v = jtoken[key1];
            if (v != null)
            {
                return GetValueOrDefault<T>(v, key2);
            }
            return default(T);
        }
    }
}
