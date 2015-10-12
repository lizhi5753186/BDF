
using Castle.Core.Logging;
using Bdf.Uow;

namespace Bdf
{
    /// <summary>
    /// This class is used as a basic class of service.
    /// </summary>
    public abstract class BdfServiceBase
    {
        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
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

        /// <summary>
        /// Reference to the logger to write logs.
        /// </summary>
        public ILogger Logger { protected get; set; }

         /// <summary>
        /// Constructor.
        /// </summary>
        protected BdfServiceBase()
        {
            Logger = NullLogger.Instance;
        }
    }
}
