using Nito.AsyncEx;
using System;
using System.Threading.Tasks;

namespace Bdf.Runtime.Caching
{
    /// <summary>
    /// Base class for caches
    /// </summary>
    public abstract class CacheBase : ICache
    {
        public string Name { get; private set; }

        public TimeSpan DefaultSlidingExpireTime { get; set; }

        protected readonly object SyncObj = new object();

        private readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        protected CacheBase(string name)
        {
            Name = name;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
        }

        public object Get(string key, Func<string, object> factory)
        {
            var cacheKey = key;
            var item = GetOrDefault(key);
            if (item != null) return item;
            lock (SyncObj)
            {
                item = GetOrDefault(key);
                if (item != null) return item;
                item = factory(key);
                if (item == null)
                {
                    throw new BdfException("Can not insert null values to the cache!");
                }

                Set(cacheKey, item);
            }

            return item;
        }

        public virtual async Task<object> GetAsync(string key, Func<string, Task<object>> factory)
        {
            var cacheKey = key;
            var item = await GetOrDefaultAsync(key);
            if (item != null) return item;
            using (await _asyncLock.LockAsync())
            {
                item = await GetOrDefaultAsync(key);
                if (item != null) return item;

                item = await factory(key);
                if (item == null)
                {
                    throw new BdfException("Can not insert null values to the cache!");
                }

                await SetAsync(cacheKey, item);
            }

            return item;
        }

        public abstract object GetOrDefault(string key);

        public virtual Task<object> GetOrDefaultAsync(string key)
        {
            return Task.FromResult(GetOrDefault(key));
        }

        public abstract void Set(string key, object value, TimeSpan? slidingExpireTime = null);

        public virtual Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            Set(key, value, slidingExpireTime);
            return Task.FromResult(0);
        }

        public abstract void Remove(string key);
        
        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        public abstract void Clear();
        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        public virtual void Dispose()
        {
        }
    }
}