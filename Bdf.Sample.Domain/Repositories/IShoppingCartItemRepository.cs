using System;
using Bdf.Domain.Repositories;
using Bdf.Sample.Domain.Model;

namespace Bdf.Sample.Domain.Repositories
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem, Guid>
    {
        ShoppingCartItem FindItem(ShoppingCart shoppingCart, Product product);
    }
}