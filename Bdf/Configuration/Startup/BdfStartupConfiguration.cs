using Bdf.Dependency;
using Bdf.Runtime.Caching.Configuration;
using Bdf.Uow;

namespace Bdf.Configuration.Startup
{
    public class BdfStartupConfiguration : DictionayBasedConfig, IBdfStartupConfiguration
    {
        public IIocManager IocManager { get; private set; }

        public IEventBusConfiguration EventBus { get; private set; }

        
        public ICachingConfiguration Caching { get; private set; }
       
        public string DefaultNameOrConnectionString { get;set; }

        public Bdf.Uow.IUnitOfWorkDefaultOptions UnitOfWork { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocManager"></param>
        public BdfStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public void Initialize()
        {
            UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            EventBus = IocManager.Resolve<IEventBusConfiguration>();
            Caching = IocManager.Resolve<ICachingConfiguration>();
        }
    }
}