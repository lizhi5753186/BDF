using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Bdf.Extensions;
using Bdf.Runtime.Session;

namespace Bdf.Uow
{
    /// <summary>
    /// Base for all Unit of Work classes.
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public string Id { get; private set; }

        public IUnitOfWork Outer { get; set; }

        public event EventHandler Completed;

        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        public event EventHandler Disposed;

        public UnitOfWorkOptions Options { get; set; }

        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters.ToImmutableList(); }
        }
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// Get a value indicates that this unit of work is diposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Reference to current ABP session.
        /// </summary>
        public IBdfSession BdfSession { private get; set; }

        /// <summary>
        /// Is <see cref="Begin"/> method called before?
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        /// Is <see cref="Complete"/> method called before?
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        /// Is this unit of work successfully completed.
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// A reference to the exception if this unit of work failed.
        /// </summary>
        private Exception _exception;

        protected UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions)
        {
            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();
            BdfSession = NullBdfSession.Instance;
        }

        public void Begin(UnitOfWorkOptions options)
        {
            if(options ==null)
                throw new ArgumentNullException("options");

            PreventMultipleBegin();
            Options = options;
            SetFilters(options.FilterOverrides);
            
            BeginUow();
        }

        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        public abstract void SaveChanges();

        public abstract Task SaveChangesAsync();

        public IDisposable DisableFilter(params string[] filterNames)
        {
            var disabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(filterName, false);
                }
            }

            disabledFilters.ForEach(ApplyDisableFilter);

            return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));
        }

        public IDisposable EnableFilter(params string[] filterNames)
        {
            var enabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(filterName, true);
                }
            }

            enabledFilters.ForEach(ApplyEnableFilter);

            return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        }

        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            var filterIndex = GetFilterIndex(filterName);

            var newfilter = new DataFilterConfiguration(_filters[filterIndex]);

            //Store old value
            object oldValue = null;
            var hasOldValue = newfilter.FilterParameters.ContainsKey(filterName);
            if (hasOldValue)
            {
                oldValue = newfilter.FilterParameters[filterName];
            }

            newfilter.FilterParameters[parameterName] = value;

            _filters[filterIndex] = newfilter;

            ApplyFilterParameterValue(filterName, parameterName, value);

            return new DisposeAction(() =>
            {
                //Restore old value
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });
        }


        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

        /// <summary>
        /// Should be implemented by derived classes to start UOW.
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// </summary>
        protected abstract void DisposeUow();

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to disable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">Filter name</param>
        protected virtual void ApplyDisableFilter(string filterName)
        {
            throw new NotImplementedException("DisableFilter is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to enable a filter.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">Filter name</param>
        protected virtual void ApplyEnableFilter(string filterName)
        {
            throw new NotImplementedException("EnableFilter is not implemented for " + GetType().FullName);
        }

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Call to trigger <see cref="Failed"/> event.
        /// </summary>
        /// <param name="exception">Exception that case failure</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }


        /// <summary>
        /// Concrete Unit of work classes should implement this
        /// method in order to set a parameter's value.
        /// Should not call base method since it throws <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">Filter name</param>
        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            throw new NotImplementedException("SetFilterParameterValue is not implemented for " + GetType().FullName);
        }

        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new BdfException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new BdfException("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }

        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (var i = 0; i < _filters.Count; i++)
            {
                var filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }
        }

        private DataFilterConfiguration GetFilter(string filterName)
        {
            var filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
            if (filter == null)
            {
                throw new BdfException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filter;
        }

        private int GetFilterIndex(string filterName)
        {
            var filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
            if (filterIndex < 0)
            {
                throw new BdfException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filterIndex;
        }
    }
}