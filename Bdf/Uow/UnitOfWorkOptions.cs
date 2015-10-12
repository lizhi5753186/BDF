using System;
using System.Transactions;
using System.Collections.Generic;

namespace Bdf.Uow
{
    /// <summary>
    /// Unit of work Options
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// Scope option
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transaction
        /// Uses default value if not supplied
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW as milliseconds
        /// Used default value if not supplied
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// Uses default value if not supplied
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }

        /// <summary>
        /// Can be used to enable/disable some filters
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; private set; }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkOptions"/> object
        /// </summary>
        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }

        /// <summary>
        /// Fill Unit of work options with default options
        /// </summary>
        /// <param name="defaultOptions"></param>
        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }

            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }

            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }
        }
    }
}