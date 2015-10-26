using System;
using Bdf.MongoDb;
using Bdf.MongoDb.Repositories;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;

namespace Sample.Repositories.MongoDB
{
    public class ShoppingCartItemRepository : MongoDbRepositoryBase<ShoppingCartItem, Guid>, IShoppingCartItemRepository
    {
        public ShoppingCartItemRepository(IMongoDatabaseProvider databaseProvider) 
            : base(databaseProvider)
        {
        }

        public ShoppingCartItem FindItem(ShoppingCart shoppingCart, Product product)
        {
            return Single(sci => sci.ShoppingCart.Id == shoppingCart.Id &&
               sci.Product.Id == product.Id);
        }
    }
}