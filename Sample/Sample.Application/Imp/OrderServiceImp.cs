using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Bdf.Application.Services;
using Bdf.Domain.Services;
using Bdf.Events.Bus;
using Bdf.Sample.Domain;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;
using Bdf.Sample.Domain.Services;
using Sample.Application.Dtos.Order;
using Sample.Repositories.MongoDB;

namespace Sample.Application.Imp
{
    public class OrderServiceImp : ApplicationService, IOrderService
    {
        #region Private Fileds
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDomainService _domainService;
        private readonly IEventBus _eventBus;
        #endregion

        #region Ctor
        public OrderServiceImp(IUserRepository userRepository,
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository,
            IShoppingCartItemRepository shoppingCartItemRepository,
            IDomainService domainService,
            IOrderRepository orderRepository,
            IEventBus eventBus)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _domainService = domainService;
            _orderRepository = orderRepository;
            _eventBus = eventBus;
        }

        #endregion 

        #region IOrderService Members
        public int GetShoppingCartItemCount(Guid userId)
        {
            var user = _userRepository.Get(userId);
            var shoppingCart = _shoppingCartRepository.Single(s => s.User.Id == user.Id);
            if (shoppingCart == null)
                throw new InvalidOperationException("No valid ShoppingCart instance.");
            var shoppingCartItems =
                _shoppingCartItemRepository.GetAllList(s => s.ShoppingCart.Id == shoppingCart.Id);
            return shoppingCartItems.Sum(s => s.Quantity);
        }

        public void AddProductToCart(Guid customerId, Guid productId, int quantity)
        {
            var user = _userRepository.Get(customerId);

            var shoppingCart = _shoppingCartRepository.Single(s => s.User.Id == user.Id);
            if (shoppingCart == null)
                throw new DomainException("User {0} don't exist shoppingcart.", customerId);

            var product = _productRepository.Get(productId);
            var shoppingCartItem = _shoppingCartItemRepository.FindItem(shoppingCart, product);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    Product = product,
                    ShoppingCart = shoppingCart,
                    Quantity = quantity
                };

                _shoppingCartItemRepository.Insert(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.UpdateQuantity(shoppingCartItem.Quantity + quantity);
                _shoppingCartItemRepository.Update(shoppingCartItem);
            }
        }

        public GetShoppingCartOutput GetShoppingCart(Guid customerId)
        {
            var user = _userRepository.Get(customerId);

            var shoppingCart = _shoppingCartRepository.Single(s => s.User.Id == user.Id);
            if (shoppingCart == null)
                throw new DomainException("User {0} don't exist shoppingcart.", customerId);

            var shoppingCartItems =
                _shoppingCartItemRepository.GetAllList(s => s.ShoppingCart.Id == shoppingCart.Id);

            var shoppingCartDto = Mapper.Map<ShoppingCart, ShoppingCartDto>(shoppingCart);
            shoppingCartDto.Items = new List<ShoppingCartItemDto>();
            if (shoppingCartItems != null && shoppingCartItems.Any())
            {
                shoppingCartItems
                    .ToList()
                    .ForEach(s => shoppingCartDto.Items.Add(Mapper.Map<ShoppingCartItem, ShoppingCartItemDto>(s)));
                shoppingCartDto.Subtotal = shoppingCartDto.Items.Sum(p => p.ItemAmount);
            }

            return new GetShoppingCartOutput()
            {
                ShoppingCart = shoppingCartDto
            };
        }

        public void UpdateShoppingCartItem(Guid shoppingCartItemId, int quantity)
        {
            var shoppingCartItem = _shoppingCartItemRepository.Get(shoppingCartItemId);
            shoppingCartItem.UpdateQuantity(quantity);
            _shoppingCartItemRepository.Update(shoppingCartItem);
        }

        public void DeleteShoppingCartItem(Guid shoppingCartItemId)
        {
            var shoppingCartItem = _shoppingCartItemRepository.Get(shoppingCartItemId);
            _shoppingCartItemRepository.Delete(shoppingCartItem);
        }

        public OrderDto Checkout(Guid customerId)
        {
            var user = _userRepository.Get(customerId);
            var shoppingCart = _shoppingCartRepository.Single(s => s.User.Id == user.Id);
            var order = _domainService.CreateOrder(user, shoppingCart);

            return Mapper.Map<Order, OrderDto>(order);
        }

        public OrderDto GetOrder(Guid orderId)
        {
            var order = _orderRepository.Single(o => o.Id == orderId);
            return Mapper.Map<Order, OrderDto>(order);
        }

        public GetOrdersOutput GetOrdersForUser(Guid userId)
        {
            var orders = _orderRepository.GetAllList(o => o.User.Id == userId).AsQueryable().OrderByDescending(o => o.CreationTime);
            var orderDtos = new List<OrderDto>();
            orders
                .ToList()
                .ForEach(o => orderDtos.Add(Mapper.Map<Order, OrderDto>(o)));
            return new GetOrdersOutput
            {
                Orders = orderDtos
            };
        }

        public GetOrdersOutput GetAllOrders()
        {
            var orders = _orderRepository.GetAll().OrderByDescending(o => o.CreationTime);
            var orderDtos = new List<OrderDto>();
            orders
                .ToList()
                .ForEach(o => orderDtos.Add(Mapper.Map<Order, OrderDto>(o)));
            return new GetOrdersOutput()
            {
                Orders = orderDtos
            };
        }

        public void Confirm(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);
            order.Confirm();
            _orderRepository.Update(order);
        }

        public void Dispatch(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);
            order.Dispatch();
            _orderRepository.Update(order);
        }
        #endregion 
    }
}