using System;
using Bdf.Application.Services;
using Sample.Application.Dtos.Order;

namespace Sample.Application
{
    public interface IOrderService : IApplicationService
    {
        int GetShoppingCartItemCount(Guid userId);

        void AddProductToCart(Guid customerId, Guid productId, int quantity);

        GetShoppingCartOutput GetShoppingCart(Guid customerId);

        void UpdateShoppingCartItem(Guid shoppingCartItemId, int quantity);

        void DeleteShoppingCartItem(Guid shoppingCartItemId);

        OrderDto Checkout(Guid customerId);

        OrderDto GetOrder(Guid orderId);

        GetOrdersOutput GetOrdersForUser(Guid userId);

        GetOrdersOutput GetAllOrders();

        void Confirm(Guid orderId);

        void Dispatch(Guid orderId);
    }
}