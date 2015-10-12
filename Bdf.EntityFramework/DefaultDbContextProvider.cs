using System.Data.Entity;

namespace Bdf.EntityFramework
{
    /// <summary>
    /// Default DbContext Provider.
    /// </summary>
    /// <typeparam name="TDbContext">Db Context</typeparam>
    public class DefaultDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext DbContext { get; private set; }

        public DefaultDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}