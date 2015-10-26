using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bdf.Domain.Services;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;
using Bdf.Uow;

namespace Bdf.Sample.Domain.Services
{
    public class SampleDomainService : DomainService
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCategorizationRepository _productCategorizationRepository;
        private readonly IUserRoleRepository _userRoleRepository;

         #region Ctor

        public SampleDomainService(
            IOrderRepository orderRepository, 
            IShoppingCartItemRepository shoppingCartItemRepository, 
            IProductCategorizationRepository productCategorizationRepository,
            IUserRoleRepository userRoleRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productCategorizationRepository = productCategorizationRepository;
            _userRoleRepository = userRoleRepository;
        }

        #endregion 
       
         [UnitOfWork]
        public Order CreateOrder(User user, ShoppingCart shoppingCart)
        {
            var order = new Order();
            var shoppingCartItems =
                _shoppingCartItemRepository.GetAllList(s => s.ShoppingCart.Id == shoppingCart.Id);
            if (shoppingCartItems == null || !shoppingCartItems.Any())
                throw new InvalidOperationException("Shopping Cart have not any item");

            order.OrderItems = new List<OrderItem>();
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderItem = shoppingCartItem.ConvertToOrderItem();
                orderItem.Order = order;
                order.OrderItems.Add(orderItem);
                _shoppingCartItemRepository.Delete(shoppingCartItem);
            }
            order.User = user;
            order.Status = OrderStatus.Paid;
            _orderRepository.Insert(order);
            return order;
        }

        [UnitOfWork]
        public ProductCategorization Categorize(Product product, Category category)
        {
            if(product == null)
                throw new ArgumentNullException("product");
            if(category == null)
                throw new ArgumentNullException("category");

            var productCategorization =
                _productCategorizationRepository.FirstOrDefault(c => c.Product.Id == product.Id);
            if (productCategorization == null)
            {
                productCategorization = ProductCategorization.CreateCategorization(product, category);
                _productCategorizationRepository.Insert(productCategorization);
            }
            else
            {
                productCategorization.Category.Id = category.Id;
                _productCategorizationRepository.Update(productCategorization);
            }

            return productCategorization;
        }

         [UnitOfWork]
        public void Uncategorize(Product product, Category category = null)
        {
            Expression<Func<ProductCategorization, bool>> specExpress = null
                ;
            if (category == null)
                specExpress = p => p.Product.Id == product.Id;
            else
                specExpress = p => p.Product.Id == product.Id && p.Category.Id == category.Id;
            var productCategorization = _productCategorizationRepository.FirstOrDefault(specExpress);
            if (productCategorization == null) return;

            _productCategorizationRepository.Delete(productCategorization);
        }

        [UnitOfWork]
        public UserRole AssignRole(User user, Role role)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (role == null)
                throw new ArgumentNullException("role");
            var userRole = _userRoleRepository.FirstOrDefault(ur => ur.UserId == user.Id);
            if (userRole == null)
            {
                userRole = UserRole.CreateUserRole(user, role);
                _userRoleRepository.Insert(userRole);
            }
            else
            {
                userRole.RoleId = role.Id;
                _userRoleRepository.Update(userRole);
            }

            return userRole;
        }

         [UnitOfWork]
        public void UnassignRole(User user, Role role = null)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            Expression<Func<UserRole, bool>> specExpression = null;
            if (role == null)
                specExpression = ur => ur.UserId == user.Id;
            else
                specExpression = ur => ur.UserId == user.Id && ur.RoleId == role.Id;

            UserRole userRole = _userRoleRepository.FirstOrDefault(specExpression);

            if (userRole == null) return;

            _userRoleRepository.Delete(userRole);
        }
    }
}