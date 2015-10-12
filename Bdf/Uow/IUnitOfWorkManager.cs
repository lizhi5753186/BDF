using System.Transactions;

namespace Bdf.Uow
{
    /// <summary>
    /// Unit of work manager.
    /// Used to begin and control a unit of work
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// Gets current active unit of work (or null if not exists).
        /// </summary>
        IActiveUnitOfWork Current { get; }

        /// <summary>
        /// Begins a new unit of work.
        /// </summary>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin();

        /// <summary>
        /// Begins a new unit of work
        /// </summary>
        /// <param name="scope">Transaction Scope</param>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope);

        /// <summary>
        /// Begins a new unit of work
        /// </summary>
        /// <param name="options">Unit of work options</param>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options);
    }
}