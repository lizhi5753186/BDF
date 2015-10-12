using System;
using System.Runtime.Serialization;

namespace Bdf
{
    /// <summary>
    /// This exception is thrown if a problem on Bdf initialization progress.
    /// </summary>
    [Serializable]
    public class BdfInitializationException : BdfException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BdfInitializationException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public BdfInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BdfInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}