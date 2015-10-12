using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Logging;
using Bdf.Collections.Extensions;

namespace Bdf.Reflection
{
    /// <summary>
    /// Type Finder Implements
    /// </summary>
    public class TypeFinder : ITypeFinder
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Assembly Finder
        /// </summary>
        public IAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public TypeFinder()
        {
            AssemblyFinder = CurrentDomainAssemblyFinder.Instance;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// Find type filter with predicate
        /// </summary>
        /// <param name="predicate">filter condition</param>
        /// <returns>Type list</returns>
        public Type[] Find(Func<Type, bool> predicate)
        {
            return GetAllTypes().Where(predicate).ToArray();
        }

        /// <summary>
        /// Find all types
        /// </summary>
        /// <returns></returns>
        public Type[] FindAll()
        {
            return GetAllTypes().ToArray();
        }

        private List<Type> GetAllTypes()
        {
            var allTypes = new List<Type>();

            foreach (var assembly in AssemblyFinder.GetAllAssemblies().Distinct())
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }

                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            return allTypes;
        }
    }
}