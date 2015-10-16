using System;

namespace Bdf.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by Bdf infrastructure.
    /// </summary>
    public class BdfHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
         public BdfHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}