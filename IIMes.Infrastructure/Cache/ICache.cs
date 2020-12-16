namespace IIMes.Infrastructure.Cache
{
    public interface ICache
    {
        void Set(string key, object value);

        T Get<T>(string key);

        void Del(string key);
    }
}