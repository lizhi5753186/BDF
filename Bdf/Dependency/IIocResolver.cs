using System;

namespace Bdf.Dependency
{
    /// <summary>
    /// Used to resolve dependencies
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// Gets an object from IOC container
        /// Return object must Released <see cref="Release"/> after usage.
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <returns>The oject instance</returns>
        T Resolve<T>();

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// Gets an object from IOC containers
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <returns>The object instance</returns>
        object Resolve(Type type);

        /// <summary>
        /// Gets an object from IOC container.
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The object instance</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// Release a resolved obhect
        /// </summary>
        /// <param name="obj">Object to be released</param>
        void Release(object obj);

        /// <summary>
        /// Check whether gived type is registered before
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns></returns>
        bool IsRegistered(Type type);

        /// <summary>
        /// Check whether given type is registered before
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        /// <returns></returns>
        bool IsRegistered<T>();
    }
}