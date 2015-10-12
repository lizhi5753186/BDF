using System;
using System.Collections.Generic;
using System.Transactions;

namespace Bdf.Uow
{
    /// <summary>
    /// Used to get/set default options for a unit of work
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// Should unit of works be transactional
        /// Default: true
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for unit of works.
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets isolation level of transaction.
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets list of all data filter configuration
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// Registers a data filter to uint of work system.
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="isEnabledByDefault"></param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// Overrides a data filter definition
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="isEnabledByDefault"></param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}