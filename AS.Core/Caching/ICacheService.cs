using Core.Caching.Redis;

namespace Core.Caching
{
    public interface ICacheService
    {
        CacheKey CreateCacheKey(string key);
        ChannelKey CreateChannelKey(string key);
        Task<Dictionary<TKey, TValue>> GetHashsetAsync<TKey, TValue>(CacheKey key);
        Task<T> GetHashsetFieldAsync<T, TField>(CacheKey key, TField field) where T : class;
        Task<bool> SetHashsetFieldAsync<T, TField>(CacheKey key, TField field, T obj) where T : class;
        Task<bool> SetHashsetAsync<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict);
        Task<bool> HashsetDeleteAsync<TField>(CacheKey key, TField field);
        Task<long> HashsetDeleteAsync<TField>(CacheKey key, List<TField> fields);
        Task<bool> SetExpiryForKeyAsync(CacheKey key, TimeSpan expiry);
        Task<T> GetAsync<T>(CacheKey key, TimeSpan? expiry = null) where T : class;
        Task<bool> SetAsync<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class;
        Task<long> DeleteKeysByPrefixAsync(CacheKey prefix);
        Task<bool> DeleteKeyAsync(CacheKey key);
        Task<long> PublishAsync<T>(ChannelKey key, T obj);
        Task<bool> SubscribeAsync<T>(ChannelKey key, Action<ChannelKey, T> handler);

        Dictionary<TKey, TValue> GetHashset<TKey, TValue>(CacheKey key);
        T GetHashsetField<T, TField>(CacheKey key, TField field) where T : class;
        bool SetHashsetField<T, TField>(CacheKey key, TField field, T obj) where T : class;
        bool SetHashset<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict);
        bool HashsetDelete<TField>(CacheKey key, TField field);
        long HashsetDelete<TField>(CacheKey key, List<TField> fields);
        bool SetExpiryForKey(CacheKey key, TimeSpan expiry);
        T Get<T>(CacheKey key, TimeSpan? expiry = null) where T : class;
        bool Set<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class;
        long DeleteKeysByPrefix(CacheKey prefix);
        bool DeleteKey(CacheKey key);
    }
}
