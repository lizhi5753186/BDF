﻿using Castle.Core;
using Castle.MicroKernel;
using Bdf.Application.Services;
using Bdf.Dependency;
using Bdf.Interceptions;

namespace Bdf.Runtime.Validation
{
    /// <summary>
    /// Used to Register Validation Interceptor.
    /// </summary>
    internal static class ValidationInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ValidationInterceptor)));
            }
        }
    }
}