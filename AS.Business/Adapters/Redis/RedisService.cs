using Core.Caching.Redis;
using Core.Caching;
using Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace Business.Adapters.Redis
{
    internal class RedisService : IRedisService
    {
        private readonly IConfiguration _config;
        private readonly ICacheService _cacheService;

        private readonly string _prefix;

        public RedisService(IConfiguration config, ICacheService cacheService)
        {
            _config = config;
            _cacheService = cacheService;
            _prefix = _config.GetSection("Redis:Prefix").Value ?? String.Empty;
        }

        /// <summary>
        /// BuildCacheKey builds a key with respect to env.
        /// <para>When interacting with Redis, a CacheKey should only be obtained via BuildCacheKey or BuildDomainSpecificCacheKey.</para>
        /// For example, "test-products" will be the result of this method when the environment is "test" and, "products" when it's "prod". 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>A key to be used as a paramater to RedisService methods.</returns>
        public CacheKey BuildCacheKey(string key)
        {
            if (!string.IsNullOrEmpty(_prefix))
            {
                return _cacheService.CreateCacheKey($"{_prefix}-{key}");
            }

            return _cacheService.CreateCacheKey(key);
        }

        public CacheKey BuildCacheKey(string key, uint domainId)
        {
            if (domainId == 0)
            {
                throw new Exception("domain is unknown");
            }

            if (!string.IsNullOrEmpty(_prefix))
            {
                return _cacheService.CreateCacheKey($"{_prefix}-{key}-domain-{domainId}");
            }

            return _cacheService.CreateCacheKey($"{key}-domain-{domainId}");
        }

        public ChannelKey BuildChannelKey(string key)
        {
            if (!string.IsNullOrEmpty(_prefix))
            {
                return _cacheService.CreateChannelKey($"{_prefix}-{key}");
            }

            return _cacheService.CreateChannelKey(key);
        }

        public ChannelKey BuildDomainSpecificChannelKey(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteKeyAsync(CacheKey key)
        {
            return await _cacheService.DeleteKeyAsync(key);
        }

        public async Task<long> DeleteKeysByPrefixAsync(CacheKey prefix)
        {
            return await _cacheService.DeleteKeysByPrefixAsync(prefix);
        }

        public async Task<T> GetAsync<T>(CacheKey key, TimeSpan? expiry = null) where T : class
        {
            return await _cacheService.GetAsync<T>(key, expiry);
        }

        public async Task<Dictionary<TKey, TValue>> GetHashsetAsync<TKey, TValue>(CacheKey key) where TKey : notnull
        {
            return await _cacheService.GetHashsetAsync<TKey, TValue>(key);
        }

        public async Task<T> GetHashsetFieldAsync<T, TField>(CacheKey key, TField field) where T : class
        {
            return await _cacheService.GetHashsetFieldAsync<T, TField>(key, field);
        }

        public async Task<bool> HashsetDeleteAsync<TField>(CacheKey key, TField field)
        {
            return await _cacheService.HashsetDeleteAsync<TField>(key, field);
        }

        public async Task<long> HashsetDeleteAsync<TField>(CacheKey key, List<TField> fields)
        {
            return await _cacheService.HashsetDeleteAsync<TField>(key, fields);
        }

        public async Task<bool> SetAsync<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class
        {
            return await _cacheService.SetAsync<T>(key, obj, expiry);
        }

        public async Task<bool> SetExpiryForKeyAsync(CacheKey key, TimeSpan expiry)
        {
            return await _cacheService.SetExpiryForKeyAsync(key, expiry);
        }

        public async Task<bool> SetHashsetAsync<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict) where TKey : notnull
        {
            return await _cacheService.SetHashsetAsync<TKey, TValue>(key, dict);
        }

        public async Task<bool> SetHashsetFieldAsync<T, TField>(CacheKey key, TField field, T obj) where T : class
        {
            return await _cacheService.SetHashsetFieldAsync<T, TField>(key, field, obj);
        }

        public async Task<long> PublishAsync<T>(ChannelKey channelKey, T obj) where T : class
        {
            return await _cacheService.PublishAsync<T>(channelKey, obj);
        }

        public async Task<bool> SubscribeAsync<T>(ChannelKey channelKey, Action<ChannelKey, T> handler) where T : class
        {
            return await _cacheService.SubscribeAsync<T>(channelKey, handler);
        }

        public Dictionary<TKey, TValue> GetHashset<TKey, TValue>(CacheKey key) where TKey : notnull
        {
            return _cacheService.GetHashset<TKey, TValue>(key);
        }

        public T GetHashsetField<T, TField>(CacheKey key, TField field) where T : class
        {
            return _cacheService.GetHashsetField<T, TField>(key, field);
        }

        public bool SetHashsetField<T, TField>(CacheKey key, TField field, T obj) where T : class
        {
            return _cacheService.SetHashsetField<T, TField>(key, field, obj);
        }

        public bool SetHashset<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict) where TKey : notnull
        {
            return _cacheService.SetHashset<TKey, TValue>(key, dict);
        }

        public bool HashsetDelete<TField>(CacheKey key, TField field)
        {
            return _cacheService.HashsetDelete<TField>(key, field);
        }

        public long HashsetDelete<TField>(CacheKey key, List<TField> fields)
        {
            return _cacheService.HashsetDelete<TField>(key, fields);
        }

        public bool SetExpiryForKey(CacheKey key, TimeSpan expiry)
        {
            return _cacheService.SetExpiryForKey(key, expiry);
        }

        public T Get<T>(CacheKey key, TimeSpan? expiry = null) where T : class
        {
            return _cacheService.Get<T>(key, expiry);
        }

        public bool Set<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class
        {
            return _cacheService.Set<T>(key, obj, expiry);
        }

        public long DeleteKeysByPrefix(CacheKey prefix)
        {
            return _cacheService.DeleteKeysByPrefix(prefix);
        }

        public bool DeleteKey(CacheKey key)
        {
            return _cacheService.DeleteKey(key);
        }
    }
}
