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
    public class ProductCategorizationRepository : MongoDbRepositoryBase<ProductCategorization, Guid>, IProductCategorizationRepository
    {
        public ProductCategorizationRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }

        public IEnumerable<Product> GetProductsForCategory(Category category)
        {
            var categorizations = Collection.AsQueryable().Where(c => c.Category.Id == category.Id).ToList();
            var productCollection = Database.GetCollection<Product>(typeof(Product).Name);
            var productsQuery = productCollection.AsQueryable();
            var totalList = new List<Product>();
            foreach (var categorization in categorizations)
            {
                totalList.AddRange(productsQuery.Where(p => p.Id == categorization.Product.Id).ToList());
            }
            return totalList;
        }

        public Category GetCategoryForProduct(Product product)
        {
            var categorizations = Collection.AsQueryable().FirstOrDefault(c => c.Product.Id == product.Id);
            if (categorizations == null)
                return null;
            var categoryCollection = Database.GetCollection<Category>(typeof(Category).Name);
            return categoryCollection.AsQueryable().FirstOrDefault(c => c.Id == categorizations.Category.Id);
        }
    }
}