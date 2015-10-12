using System;
using System.Linq;
using Castle.Core.Logging;
using Bdf.Collections.Extensions;
using Bdf.Dependency;
using Bdf.Runtime.Validation;

namespace Bdf.Logging
{
    /// <summary>
    /// This class can be used to write logs from somewhere where it's a little hard to get a reference to the <see cref="ILogger"/>.
    /// Normally, get <see cref="ILogger"/> using property injection.
    /// </summary>
    internal class LogHelper
    {
        /// <summary>
        /// A reference to the logger.
        /// </summary>
        public static ILogger Logger { get; private set; }

        /// <summary>
        /// Static Constructor.
        /// </summary>
        static LogHelper()
        {
            Logger = IocManager.Instance.IsRegistered(typeof(ILogger))
                ? IocManager.Instance.Resolve<ILogger>()
                : NullLogger.Instance;
        }

        /// <summary>
        /// Log Exception.
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        /// <summary>
        ///  Log Exception
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="ex">Exception</param>
        public static void LogException(ILogger logger, Exception ex)
        {
            logger.Error(ex.ToString(), ex);
            LogValidationErrors(ex);
        }

        /// <summary>
        /// Log validation error
        /// </summary>
        /// <param name="exception">Exception</param>
        private static void LogValidationErrors(Exception exception)
        {
            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is BdfValidationException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (!(exception is BdfValidationException))
            {
                return;
            }

            var validationException = exception as BdfValidationException;
            if (validationException.ValidationErrors.IsNullOrEmpty())
            {
                return;
            }

            Logger.Warn("There are " + validationException.ValidationErrors.Count + " validation errors:");
            foreach (var validationResult in validationException.ValidationErrors)
            {
                var memberNames = "";
                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    memberNames = " (" + string.Join(", ", validationResult.MemberNames) + ")";
                }

                Logger.Warn(validationResult.ErrorMessage + memberNames);
            }
        }
    }
}