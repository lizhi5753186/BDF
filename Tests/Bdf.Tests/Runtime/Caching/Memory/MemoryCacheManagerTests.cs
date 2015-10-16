using System;
using Bdf.Runtime.Caching;
using Bdf.Runtime.Caching.Configuration;
using Bdf.Runtime.Caching.Memory;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Runtime.Caching.Memory
{
    public class MemoryCacheManagerTests : TestBaseWithLocalIocManager
    {
        private readonly ITypedCache<string, MyCacheItem> _cache;

        public MemoryCacheManagerTests()
        {
            LocalIocManager.Register<ICachingConfiguration, CachingConfiguration>();
            LocalIocManager.Register<ICacheManager, BdfMemoryCacheManager>();
            var cacheManager = LocalIocManager.Resolve<ICacheManager>();

            var defaultSlidingExpireTime = TimeSpan.FromHours(24);
            LocalIocManager.Resolve<ICachingConfiguration>().ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = defaultSlidingExpireTime;
            });

            _cache = cacheManager.GetCache<string, MyCacheItem>("MyCacheItems");
            _cache.DefaultSlidingExpireTime.ShouldBe(defaultSlidingExpireTime);
        }

        [Fact]
        public void Simple_Get_Set_Test()
        {
            _cache.GetOrDefault("A").ShouldBe(null);

            _cache.Set("A", new MyCacheItem { Value = 42 });

            _cache.GetOrDefault("A").ShouldNotBe(null);
            _cache.GetOrDefault("A").Value.ShouldBe(42);

            _cache.Get("B", () => new MyCacheItem { Value = 43 }).Value.ShouldBe(43);
            _cache.Get("B", () => new MyCacheItem { Value = 44 }).Value.ShouldBe(43); //Does not call factory, so value is not changed
        }

        [Serializable]
        public class MyCacheItem
        {
            public int Value { get; set; }
        }
    }
   
}