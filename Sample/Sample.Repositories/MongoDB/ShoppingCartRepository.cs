using System;
using Bdf.MongoDb;
using Bdf.MongoDb.Repositories;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;

namespace Sample.Repositories.MongoDB
{
    public class ShoppingCartRepository : MongoDbRepositoryBase<ShoppingCart, Guid>, IShoppingCartRepository
    {
        public ShoppingCartRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }
    }
}