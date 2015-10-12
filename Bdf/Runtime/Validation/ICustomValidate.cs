using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bdf.Runtime.Validation
{
    /// <summary>
    /// Defines interface must be implemented by classes those must be validated with custom rules
    /// </summary>
    public interface ICustomValidate : IValidate
    {
        /// <summary>
        /// Used to Validate the object
        /// </summary>
        /// <param name="results">List of validation errors</param>
        void AddValidationErrors(List<ValidationResult> results);
    }
}