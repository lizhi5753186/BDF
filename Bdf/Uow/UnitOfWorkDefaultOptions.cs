using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Bdf.Uow
{
    public class UnitOfWorkDefaultOptions: IUnitOfWorkDefaultOptions
    {
        public TransactionScopeOption Scope { get; set; }
        public bool IsTransactional { get; set; }
        public TimeSpan? Timeout { get; set; }
        public IsolationLevel? IsolationLevel { get; set; }

        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters; }
        }
        private readonly List<DataFilterConfiguration> _filters;
        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.FilterName == filterName))
            {
                throw new BdfException("There is already a filter with name: " + filterName);
            }

            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            _filters.RemoveAll(f => f.FilterName == filterName);
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        public UnitOfWorkDefaultOptions()
        {
            _filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
        }
    }
}