using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashBot.Core.Extensions
{
    public static class IsolatedStorageExtensions
    {
        public static void SaveSetting(this IsolatedStorageSettings settings, string key, object data)
        {
            settings[key] = data;
            settings.Save();
        }

        public static T LoadSetting<T>(this IsolatedStorageSettings settings, string key)
        {
            if (settings.Contains(key))
                return (T)settings[key];
            return default(T);
        }
    }
}
