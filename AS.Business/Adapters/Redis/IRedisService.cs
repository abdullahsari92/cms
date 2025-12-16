using Core.Caching.Redis;

namespace Business.Adapters.Redis
{
    public interface IRedisService
    {
        /// <summary>
        /// BuildCacheKey builds a key with respect to env.
        /// <para>When interacting with Redis, a CacheKey should only be obtained via BuildCacheKey or BuildDomainSpecificCacheKey.</para>
        /// For example, "test-products" will be the result of this method when the environment is "test" and, "products" when it's "prod". 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A key to be used as a paramater to RedisService methods.</returns>
        CacheKey BuildCacheKey(string key);

        CacheKey BuildCacheKey(string key, uint domainId);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <returns>A dictionary of key <typeparamref name="TKey"/> value <typeparamref name="TValue"/>. Returns an empty dictionary if the hash set doesn't exist.</returns>
        Task<Dictionary<TKey, TValue>> GetHashsetAsync<TKey, TValue>(CacheKey key) where TKey : notnull;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>The hash field value in the form of <typeparamref name="T"/>. If it doesn't exist returns the default value for the type <typeparamref name="T"/>. Therefore null reference is a possible outcome and should be checked.</returns>
        Task<T> GetHashsetFieldAsync<T, TField>(CacheKey key, TField field) where T : class;
        Task<bool> SetHashsetFieldAsync<T, TField>(CacheKey key, TField field, T obj) where T : class;
        Task<bool> SetHashsetAsync<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict) where TKey : notnull;
        Task<bool> HashsetDeleteAsync<TField>(CacheKey key, TField field);
        Task<long> HashsetDeleteAsync<TField>(CacheKey key, List<TField> fields);
        Task<bool> SetExpiryForKeyAsync(CacheKey key, TimeSpan expiry);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(CacheKey key, TimeSpan? expiry = null) where T : class;
        Task<bool> SetAsync<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class;
        Task<long> DeleteKeysByPrefixAsync(CacheKey prefix);
        Task<bool> DeleteKeyAsync(CacheKey key);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <returns>A dictionary of key <typeparamref name="TKey"/> value <typeparamref name="TValue"/>. Returns an empty dictionary if the hash set doesn't exist.</returns>
        Dictionary<TKey, TValue> GetHashset<TKey, TValue>(CacheKey key) where TKey : notnull;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TField"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns>The hash field value in the form of <typeparamref name="T"/>. If it doesn't exist returns the default value for the type <typeparamref name="T"/>. Therefore null reference is a possible outcome and should be checked.</returns>
        T GetHashsetField<T, TField>(CacheKey key, TField field) where T : class;
        bool SetHashsetField<T, TField>(CacheKey key, TField field, T obj) where T : class;
        bool SetHashset<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict) where TKey : notnull;
        bool HashsetDelete<TField>(CacheKey key, TField field);
        long HashsetDelete<TField>(CacheKey key, List<TField> fields);
        bool SetExpiryForKey(CacheKey key, TimeSpan expiry);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        T Get<T>(CacheKey key, TimeSpan? expiry = null) where T : class;
        bool Set<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class;
        long DeleteKeysByPrefix(CacheKey prefix);
        bool DeleteKey(CacheKey key);

        /// <summary>
        /// BuildChannelKey builds a key with respect to env.
        /// <para>When interacting with Redis, a ChannelKey should only be obtained via BuildChannelKey or BuildDomainSpecificChannelKey.</para>
        /// For example, "test-transfer-jobs" will be the result of this method when the environment is "test" and, "products" when it's "prod". 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A key to be used as a paramater to RedisService pub/sub methods.</returns>
        ChannelKey BuildChannelKey(string key);

        /// <summary>
        /// BuildDomainSpecificChannelKey builds a domain specific key with respect to env.
        /// <para>When interacting with Redis, a ChannelKey should only be obtained via BuildChannelKey or BuildDomainSpecificChannelKey.</para>
        /// <example> For example, "test-transfer-jobs-domain-4" will be the result of this method when the domain id is 4 </example> 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A key to be used as a paramater to RedisService pub/sub methods.</returns>
        [Obsolete("Not yet implemented")]
        ChannelKey BuildDomainSpecificChannelKey(string key);

        Task<long> PublishAsync<T>(ChannelKey channelKey, T obj) where T : class;

        Task<bool> SubscribeAsync<T>(ChannelKey channel, Action<ChannelKey, T> handler) where T : class;
    }
}
