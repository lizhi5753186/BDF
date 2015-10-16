using System.Reflection;

namespace Bdf.Dependency
{
    public class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public Assembly Assembly { get; private set; }


        public IIocManager IocManager { get; private set; }
        

        public ConventionalRegistrationConfig Config  { get; private set; }

        public ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}