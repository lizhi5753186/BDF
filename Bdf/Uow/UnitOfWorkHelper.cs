using System;
using System.Reflection;
using Bdf.Application.Services;
using Bdf.Domain.Repositories;

namespace Bdf.Uow
{
    /// <summary>
    /// A helper class to simplify unit of work process
    /// </summary>
    internal static class UnitOfWorkHelper
    {
        /// <summary>
        /// Check type whether is IRepository or IApplicationService instance
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns></returns>
        public static bool IsConventionalUowClass(Type type)
        {
            return typeof(IRepository).IsAssignableFrom(type) || typeof(IApplicationService).IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns true if given method has UnitOfWorkAttribute attribute.
        /// </summary>
        /// <param name="methodInfo">Method info to check</param>
        public static bool HasUnitOfWorkAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        /// <summary>
        /// Returns UnitOfWorkAttribute it exists.
        /// </summary>
        /// <param name="methodInfo">Method info to check</param>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false);
            if (attrs.Length <= 0)
            {
                return null;
            }

            return (UnitOfWorkAttribute)attrs[0];
        }
    }
}