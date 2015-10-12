using System.Threading.Tasks;
using Bdf.Dependency;
using Bdf.Uow;

namespace Bdf.MemoryDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MemoryDb.
    /// </summary>
    public class MemoryDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to Memory Database.
        /// </summary>
        public MemoryDatabase Database { get; private set; }

       
        private readonly MemoryDatabase _memoryDatabase;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MemoryDbUnitOfWork(MemoryDatabase memoryDatabase, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _memoryDatabase = memoryDatabase;
        }

        protected override void BeginUow()
        {
            Database = _memoryDatabase;
        }

        public override void SaveChanges()
        {

        }

        public override async Task SaveChangesAsync()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected override async Task CompleteUowAsync()
        {

        }

        protected override void DisposeUow()
        {

        }
    }
}