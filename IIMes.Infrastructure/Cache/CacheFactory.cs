namespace IIMes.Infrastructure.Cache
{
    public static class CacheFactory
    {
        private static ICache _cache;

        public static ICache GetCache()
        {
            // string useRedisSession = ConfigurationManager.AppSettings.Get("UseRedisSession");
            if (_cache == null)
            {
                _cache = MemCache.GetInstance();
                // if (string.Compare(useRedisSession, "true", StringComparison.OrdinalIgnoreCase) == 0)
                // {
                //     _cache = RedisCache.GetInstance();
                // }
                // else
                // {
                //     _cache = MemCache.GetInstance();
                // }
            }

            return _cache;
        }
    }
}
