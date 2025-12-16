using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Caching.Redis
{
    public class ChannelKey
    {
        private readonly string _key;
        private ChannelKey(string key)
        {
            _key = key;
        }
        internal string Unwrap()
        {
            if (String.IsNullOrEmpty(_key))
            {
                throw new ArgumentNullException(nameof(ChannelKey));
            }
            return _key;
        }
        public static ChannelKey Wrap(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            return new ChannelKey(key);
        }
    }
}
