using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Caching.Redis
{
    /// <summary>
    /// String wrapper to limit how one can interact with the cache.
    /// </summary>
    public class CacheKey
    {
        private readonly string _key;
        private CacheKey(string key)
        {
            _key = key;
        }
        internal string Unwrap()
        {
            if (String.IsNullOrEmpty(_key))
            {
                throw new ArgumentNullException(nameof(CacheKey));
            }
            return _key;
        }
        public static CacheKey Wrap(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return new CacheKey(key);
        }
    }
}
