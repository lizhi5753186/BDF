using System.Threading.Tasks;
using MongoDB.Driver;
using Bdf.Dependency;
using Bdf.MongoDb.Configuration;
using Bdf.Uow;

namespace Bdf.MongoDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MongoDB.
    /// </summary>
    public class MongoDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to MongoDB Database.
        /// </summary>
        public MongoDatabase Database { get; private set; }

        private readonly IBdfMongoDbConfiguration _configuration;
        /// <summary>
        /// Constructor.
        /// </summary>
        public MongoDbUnitOfWork(IBdfMongoDbConfiguration configuration, IUnitOfWorkDefaultOptions defaultOptions)
            : base(defaultOptions)
        {
            _configuration = configuration;
        }

        protected override void BeginUow()
        {
            Database = new MongoClient(_configuration.ConnectionString)
                .GetServer()
                .GetDatabase(_configuration.DatabaseName);
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