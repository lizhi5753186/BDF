using System;
using System.Collections.Generic;
using System.Linq;
using Bdf.MongoDb;
using Bdf.MongoDb.Repositories;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;
using MongoDB.Driver.Linq;

namespace Sample.Repositories.MongoDB
{
    public class ProductRepository : MongoDbRepositoryBase<Product, Guid>, IProductRepository
    {
        public ProductRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }

        public IEnumerable<Product> GetNewProducts(int count = 0)
        {
            List<Product> products = null;
            products = count == 0 ? 
                Collection.AsQueryable().Where(p => p.IsNew).ToList() 
                : Collection.AsQueryable().Where(p => p.IsNew).Take(count).ToList();
            return products;
        }
    }
}