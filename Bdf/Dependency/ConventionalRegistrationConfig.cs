using Castle.DynamicProxy;

namespace Bdf.Dependency
{
    /// <summary>
    /// Used to pass configuration while registering classes in conventional way
    /// </summary>
    public class ConventionalRegistrationConfig
    {
        /// <summary>
        /// Install all <see cref="IInterceptor"/> implementations automatically or not.
        /// </summary>
        public bool InstallInstallers { get; set; }

        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}