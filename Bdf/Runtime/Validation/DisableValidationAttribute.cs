using System;

namespace Bdf.Runtime.Validation
{
    /// <summary>
    ///Disable auto validation
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableValidationAttribute : Attribute
    {
         
    }
}