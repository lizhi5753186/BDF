using Bdf.Dependency;
using Bdf.Runtime.Caching.Configuration;

namespace Bdf.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICacheManager"/> to manager memorycache
    /// </summary>
    public class BdfMemoryCacheManager : CacheManagerBase
    {
        public BdfMemoryCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<BdfMemoryCache>(DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<BdfMemoryCache>(new { name });
        }
    }
}