using System.Configuration;
using Castle.MicroKernel.Registration;
using Bdf.Configuration.Startup;
using Bdf.Dependency;

namespace Bdf.EntityFramework.Dependency
{
    /// <summary>
    /// Registers classes derived from AbpDbContext with configurations.
    /// </summary>
    public class EntityFrameworkConventionalRegisterer : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<BdfDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                    .Configure(c => c.DynamicParameters(
                        (kernel, dynamicParams) =>
                        {
                            var connectionString = GetNameOrConnectionStringOrNull(context.IocManager);
                            if (!string.IsNullOrWhiteSpace(connectionString))
                            {
                                dynamicParams["nameOrConnectionString"] = connectionString;
                            }
                        })));
        }

        private static string GetNameOrConnectionStringOrNull(IIocResolver iocResolver)
        {
            if (iocResolver.IsRegistered<IBdfStartupConfiguration>())
            {
                var defaultConnectionString = iocResolver.Resolve<IBdfStartupConfiguration>().DefaultNameOrConnectionString;
                if (!string.IsNullOrWhiteSpace(defaultConnectionString))
                {
                    return defaultConnectionString;
                }
            }

            return ConfigurationManager.ConnectionStrings["Default"] != null ? "Default" : null;
        }
    }
}