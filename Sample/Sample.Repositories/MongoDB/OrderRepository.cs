using System;
using Bdf.MongoDb;
using Bdf.MongoDb.Repositories;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;

namespace Sample.Repositories.MongoDB
{
    public class OrderRepository : MongoDbRepositoryBase<Order, Guid>, IOrderRepository
    {
        public OrderRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }
    }
}