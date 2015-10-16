using System.Linq;
using System.Reflection;
using Bdf.Dependency;
using Bdf.Interceptions;
using Castle.Core;
using Castle.MicroKernel;

namespace Bdf.Uow
{
    /// <summary>
    /// This class is used to register interceptor for needed classes of Unit of Work mechanism.
    /// </summary>
    public static class UnitOfWorkRegistrar
    {
        /// <summary>
        /// Initializes the registerer.
        /// </summary>sssss
        /// <param name="iocManager">IOC manager</param>
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += ComponentRegistered;
        }

        private static void ComponentRegistered(string key, IHandler handler)
        {
            if (UnitOfWorkHelper.IsConventionalUowClass(handler.ComponentModel.Implementation))
            {
                //Intercept all methods of all repositories.
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            else if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
            {
                //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
    }
}