using Bdf.Domain.Entities;
using Bdf.Domain.Repositories;

namespace Bdf.MemoryDb.Repositories
{
    public class MemoryRepository<TEntity> : MemoryRepository<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public MemoryRepository(IMemoryDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }
}