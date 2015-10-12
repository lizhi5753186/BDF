namespace Bdf.Runtime.Validation
{
    /// <summary>
    /// Used to normalize inputs before method execution.
    /// </summary>
    public interface IShouldNormalize
    {
        /// <summary>
        /// This method is called lastly before method execution (after validation if exists).
        /// </summary>
        void Normalize();
    }
}