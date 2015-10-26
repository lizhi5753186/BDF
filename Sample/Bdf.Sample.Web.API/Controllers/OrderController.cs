using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sample.Application;
using Sample.Application.Dtos.Order;

namespace Bdf.Sample.Web.API.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderServiceImp;

        public OrderController(IOrderService orderService)
        {
            _orderServiceImp = orderService;
        }

        [Route("ProductToCart")]
        public void AddProductToCart(Guid customerId, Guid productId, int quantity)
        {
            _orderServiceImp.AddProductToCart(customerId, productId, quantity);
        }

        [Route("ShoppingCart")]
        public GetShoppingCartOutput GetShoppingCart(Guid customerId)
        {
            return _orderServiceImp.GetShoppingCart(customerId);
        }

        [Route("ShoppingCartItemCount")]
        public int GetShoppingCartItemCount(Guid userId)
        {
            return _orderServiceImp.GetShoppingCartItemCount(userId);
        }

        [Route("UpdateShoppingCartItem")]
        public void UpdateShoppingCartItem(Guid shoppingCartItemId, int quantity)
        {
            _orderServiceImp.UpdateShoppingCartItem(shoppingCartItemId, quantity);
        }

        [Route("DeleteShoppingCartItem")]
        public void DeleteShoppingCartItem(Guid shoppingCartItemId)
        {
            _orderServiceImp.DeleteShoppingCartItem(shoppingCartItemId);
        }

        [Route("Checkout")]
        public OrderDto Checkout(Guid customerId)
        {
            return _orderServiceImp.Checkout(customerId);
        }

        [Route("Order")]
        public OrderDto GetOrder(Guid orderId)
        {
            return _orderServiceImp.GetOrder(orderId);
        }

        [Route("OrdersForUser")]
        public GetOrdersOutput GetOrdersForUser(Guid userId)
        {
            return _orderServiceImp.GetOrdersForUser(userId);
        }

        [Route("Orders")]
        public GetOrdersOutput GetAllOrders()
        {
            return _orderServiceImp.GetAllOrders();
        }

        [Route("Confirm")]
        public void Confirm(Guid orderId)
        {
            _orderServiceImp.Confirm(orderId);
        }

        [Route("Dispatch")]
        public void Dispatch(Guid orderId)
        {
            _orderServiceImp.Dispatch(orderId);
        }
    }
}
