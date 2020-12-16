using System.Collections.Generic;
using System.Runtime.Caching;
using NLog;

namespace IIMes.Infrastructure.Cache
{
    public sealed class MemCache : ICache
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        private static readonly object SyncRoot = new object();
        private static MemCache _instance;

        public static MemCache GetInstance()
        {
            lock (SyncRoot)
            {
                if (_instance == null)
                {
                    _instance = new MemCache();
                }

                return _instance;
            }
        }

        private MemCache()
        {
            System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
            Logger.Info("MemCache Initialized" + t);
        }

        public void Set(string key, object value)
        {
            if (value == null)
            {
                throw new System.Exception("A null object value is being tried to add into Cache.");
            }

            lock (SyncRoot)
            {
                _cache[key] = value;
            }
        }

        public T Get<T>(string key)
        {
            lock (SyncRoot)
            {
                if (_cache.ContainsKey(key))
                {
                    object obj = _cache[key];
                    return (T)obj;
                }
                else
                {
                    return default;
                }
            }
        }

        public void Del(string key)
        {
            if (key == null)
            {
                return;
            }

            lock (SyncRoot)
            {
                if (_cache.ContainsKey(key))
                {
                    _cache.Remove(key);
                }
            }
        }
    }
}
