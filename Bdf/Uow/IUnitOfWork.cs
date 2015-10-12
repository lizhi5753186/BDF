using System.Threading.Tasks;

namespace Bdf.Uow
{
    /// <summary>
    /// Defines a unit of work.
    /// This interface is internally used by Bdf.
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// 
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Reference to the outer UOW if exists
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// Begins the unit of work with given options.
        /// </summary>
        /// <param name="options">Unit of work options</param>
        void Begin(UnitOfWorkOptions options);
    }
}