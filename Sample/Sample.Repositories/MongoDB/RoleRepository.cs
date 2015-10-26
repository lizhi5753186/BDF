using System;
using Bdf.MongoDb;
using Bdf.MongoDb.Repositories;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;

namespace Sample.Repositories.MongoDB
{
    public class RoleRepository : MongoDbRepositoryBase<Role, Guid>, IRoleRepository
    {
        public RoleRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }
    }
}