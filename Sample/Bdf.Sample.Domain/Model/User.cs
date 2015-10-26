using System;
using System.Collections.Generic;
using Bdf.Domain.Entities;
using Bdf.Domain.Entities.Auditing;

namespace Bdf.Sample.Domain.Model
{
    public class User : Entity<Guid>, IHasCreationTime, ISoftDelete 
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

       
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime LastLogonDate { get; set; }

        public string Contact { get; set; }

        //用户的联系地址
        public Address ContactAddress { get; set; }

        //用户的发货地址
        public Address DeliveryAddress { get; set; }

        public IEnumerable<Order> Orders
        {
            get
            {
                IEnumerable<Order> result = null;
                //DomainEvent.Handle<GetUserOrdersEvent>(new GetUserOrdersEvent(this),
                //    (e, ret, exc) =>
                //    {
                //        result = e.Orders;
                //    });
                return result;
            }
        }

        public override string ToString()
        {
            return this.UserName;
        }

        #region Public Methods

        public void Disable()
        {
            this.IsDeleted = true;
        }

        public void Enable()
        {
            this.IsDeleted = false;
        }

        public ShoppingCart CreateShoppingCart()
        {
            var shoppingCart = new ShoppingCart { User = this };
            return shoppingCart;
        }
        #endregion 
    }
}