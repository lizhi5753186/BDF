using System;
using System.Runtime.Caching;

namespace Bdf.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICache"/> to work as <see cref="MemoryCache"/>
    /// </summary>
    public class BdfMemoryCache : CacheBase
    {
        private readonly MemoryCache _memoryCache;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Unique name of the cache</param>
        public BdfMemoryCache(string name)
            :base(name)
        {
            _memoryCache = new MemoryCache(Name);
        }

        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }

        public override void Set(string key, object value, System.TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                throw new BdfException("Can not insert null values to the cache!");
            }

            _memoryCache.Set(
                key,
                value,
                new CacheItemPolicy
                {
                    SlidingExpiration = slidingExpireTime ?? DefaultSlidingExpireTime
                });
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);

        }

        public override void Clear()
        {
            _memoryCache.Dispose();
        }

        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}