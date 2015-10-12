using System;

namespace Bdf.Reflection
{
    /// <summary>
    /// This interface is used to find all type by using Relection
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// Find types with predicate
        /// </summary>
        /// <param name="predicate">Filter condition</param>
        /// <returns>filter types</returns>
        Type[] Find(Func<Type, bool> predicate);


        /// <summary>
        /// Find all types
        /// </summary>
        /// <returns>types</returns>
        Type[] FindAll(); 
    }
}