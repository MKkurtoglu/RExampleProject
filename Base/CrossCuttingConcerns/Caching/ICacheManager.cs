namespace Base.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);
        void Remove(string key);
        bool isAdd(string key);
        void RemoveByPattern(string pattern);
    }
}
