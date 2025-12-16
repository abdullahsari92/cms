using Newtonsoft.Json;
using ServiceStack;
using StackExchange.Redis;


namespace Core.Caching.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _conn;
        public RedisCacheService(IConnectionMultiplexer conn)
        {
            _conn = conn;
        }
        public CacheKey CreateCacheKey(string key)
        {
            return CacheKey.Wrap(key);
        }
        public ChannelKey CreateChannelKey(string key)
        {
            return ChannelKey.Wrap(key);
        }

        public async Task<T> GetHashsetFieldAsync<T, TField>(CacheKey key, TField field) where T : class
        {
            if (!_conn.IsConnected)
            {
                return default;
            }
            var db = _conn.GetDatabase();
            var result = await db.HashGetAsync(Unwrap(key), RedisValue.Unbox(field));
            if (result.IsNullOrEmpty)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<T> GetAsync<T>(CacheKey key, TimeSpan? expiry = null) where T : class
        {
            if (!_conn.IsConnected)
            {
                return default;
            }
            var db = _conn.GetDatabase();

            RedisValue result;
            if (expiry.HasValue)
            {
                result = await db.StringGetSetExpiryAsync(Unwrap(key), expiry);
            }
            else
            {
                result = await db.StringGetAsync(Unwrap(key));
            }

            if (result.IsNullOrEmpty)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<bool> SetExpiryForKeyAsync(CacheKey key, TimeSpan expiry)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();
            return await db.KeyExpireAsync(Unwrap(key), expiry);
        }

        public async Task<bool> SetAsync<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            var serialized = JsonConvert.SerializeObject(obj);

            return await db.StringSetAsync(Unwrap(key), serialized, expiry: expiry);
        }

        public async Task<Dictionary<TKey, TValue>> GetHashsetAsync<TKey, TValue>(CacheKey key)
        {
            if (!_conn.IsConnected)
            {
                return new Dictionary<TKey, TValue>();
            }
            var db = _conn.GetDatabase();
            HashEntry[] data = await db.HashGetAllAsync(Unwrap(key));

            return data.ToDictionary(entry => entry.Name.ConvertTo<TKey>(), entry => JsonConvert.DeserializeObject<TValue>(entry.Value));
        }

        public async Task<bool> SetHashsetFieldAsync<T, TField>(CacheKey key, TField field, T obj) where T : class
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            var serialized = JsonConvert.SerializeObject(obj);

            await db.HashSetAsync(Unwrap(key), RedisValue.Unbox(field), serialized);
            // Don't use HashSetAsync's return value because both results indicate success and that's what we care.
            return true;
        }

        public async Task<bool> SetHashsetAsync<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            await db.HashSetAsync(Unwrap(key), dict.Select(pair => new HashEntry(RedisValue.Unbox(pair.Key), JsonConvert.SerializeObject(pair.Value))).ToArray());
            return true;
        }

        public async Task<bool> HashsetDeleteAsync<TField>(CacheKey key, TField field)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            await db.HashDeleteAsync(Unwrap(key), RedisValue.Unbox(field));
            return true;
        }

        public async Task<long> HashsetDeleteAsync<TField>(CacheKey key, List<TField> fields)
        {
            if (!_conn.IsConnected)
            {
                return -1;
            }
            var db = _conn.GetDatabase();

            return await db.HashDeleteAsync(Unwrap(key), fields.Select(f => RedisValue.Unbox(f)).ToArray());
        }

        public async Task<long> DeleteKeysByPrefixAsync(CacheKey prefix)
        {
            if (!_conn.IsConnected)
            {
                return -1;
            }
            var db = _conn.GetDatabase();
            var endpoints = _conn.GetEndPoints();
            var keys = _conn.GetServer(endpoints[0]).Keys(database: db.Database, pattern: Unwrap(prefix) + "*").ToArray();

            var deletedCount = await db.KeyDeleteAsync(keys);
            return deletedCount;
        }

        public async Task<bool> DeleteKeyAsync(CacheKey key)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            return await db.KeyDeleteAsync(Unwrap(key));
        }

        public async Task<long> PublishAsync<T>(ChannelKey key, T obj)
        {
            if (!_conn.IsConnected)
            {
                return -1;
            }

            var serialized = JsonConvert.SerializeObject(obj);
            return await _conn.GetSubscriber().PublishAsync(Unwrap(key), serialized);
        }

        public async Task<bool> SubscribeAsync<T>(ChannelKey key, Action<ChannelKey, T> handler)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }

            await _conn.GetSubscriber().SubscribeAsync(Unwrap(key), (redisChannel, value) =>
            {
                handler(key, JsonConvert.DeserializeObject<T>(value));
            });
            return true;
        }

        private static string Unwrap(CacheKey key)
        {
            return key.Unwrap();
        }

        private static string Unwrap(ChannelKey key)
        {
            return key.Unwrap();
        }

        public Dictionary<TKey, TValue> GetHashset<TKey, TValue>(CacheKey key)
        {
            if (!_conn.IsConnected)
            {
                return new Dictionary<TKey, TValue>();
            }
            var db = _conn.GetDatabase();
            HashEntry[] data = db.HashGetAll(Unwrap(key));

            return data.ToDictionary(entry => entry.Name.ConvertTo<TKey>(), entry => JsonConvert.DeserializeObject<TValue>(entry.Value));
        }

        public T GetHashsetField<T, TField>(CacheKey key, TField field) where T : class
        {
            if (!_conn.IsConnected)
            {
                return default;
            }
            var db = _conn.GetDatabase();
            var result = db.HashGet(Unwrap(key), RedisValue.Unbox(field));
            if (result.IsNullOrEmpty)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        public bool SetHashsetField<T, TField>(CacheKey key, TField field, T obj) where T : class
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            var serialized = JsonConvert.SerializeObject(obj);

            db.HashSet(Unwrap(key), RedisValue.Unbox(field), serialized);
            // Don't use HashSetAsync's return value because both results indicate success and that's what we care.
            return true;
        }

        public bool SetHashset<TKey, TValue>(CacheKey key, Dictionary<TKey, TValue> dict)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            db.HashSet(Unwrap(key), dict.Select(pair => new HashEntry(RedisValue.Unbox(pair.Key), JsonConvert.SerializeObject(pair.Value))).ToArray());
            return true;
        }

        public bool HashsetDelete<TField>(CacheKey key, TField field)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            db.HashDelete(Unwrap(key), RedisValue.Unbox(field));
            return true;
        }

        public long HashsetDelete<TField>(CacheKey key, List<TField> fields)
        {
            if (!_conn.IsConnected)
            {
                return -1;
            }
            var db = _conn.GetDatabase();

            return db.HashDelete(Unwrap(key), fields.Select(f => RedisValue.Unbox(f)).ToArray());
        }

        public bool SetExpiryForKey(CacheKey key, TimeSpan expiry)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();
            return db.KeyExpire(Unwrap(key), expiry);
        }

        public T Get<T>(CacheKey key, TimeSpan? expiry = null) where T : class
        {
            if (!_conn.IsConnected)
            {
                return default;
            }
            var db = _conn.GetDatabase();

            RedisValue result;
            if (expiry.HasValue)
            {
                result = db.StringGetSetExpiry(Unwrap(key), expiry);
            }
            else
            {
                result = db.StringGet(Unwrap(key));
            }

            if (result.IsNullOrEmpty)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(result);
        }

        public bool Set<T>(CacheKey key, T obj, TimeSpan? expiry = null) where T : class
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            var serialized = JsonConvert.SerializeObject(obj);

            return db.StringSet(Unwrap(key), serialized, expiry: expiry);
        }

        public long DeleteKeysByPrefix(CacheKey prefix)
        {
            if (!_conn.IsConnected)
            {
                return -1;
            }
            var db = _conn.GetDatabase();
            var endpoints = _conn.GetEndPoints();
            var keys = _conn.GetServer(endpoints[0]).Keys(database: db.Database, pattern: Unwrap(prefix) + "*").ToArray();

            var deletedCount = db.KeyDelete(keys);
            return deletedCount;
        }

        public bool DeleteKey(CacheKey key)
        {
            if (!_conn.IsConnected)
            {
                return false;
            }
            var db = _conn.GetDatabase();

            return db.KeyDelete(Unwrap(key));
        }
    }
}
