using System.Reflection;

namespace Bdf.Dependency
{
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        public Assembly Assembly { get; private set; }


        public IIocManager IocManager { get; private set; }
        

        public ConventionalRegistrationConfig Config  { get; private set; }

        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}