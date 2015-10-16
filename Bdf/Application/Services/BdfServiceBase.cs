using Bdf.Uow;

namespace Bdf.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for application services
    /// It has some useful object property-injected and has some basic methods
    /// most of services may need to.
    /// </summary>
    public abstract class BdfServiceBase
    {
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new BdfException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }

        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }
    }
}