using Bdf.Dependency;
using Bdf.Runtime.Caching;
using Bdf.Runtime.Caching.Configuration;

namespace Bdf.RedisCache.RedisImpl
{
    public class BdfRedisCacheManager : CacheManagerBase
    {
        public BdfRedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<BdfRedisCache>(DependencyLifeStyle.Transient);
        }
        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<BdfRedisCache>(new { name });
        }
    }
}