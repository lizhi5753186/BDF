using System;
using Bdf.Domain.Entities;

namespace Bdf.Sample.Domain.Model
{
    public class ShoppingCartItem : Entity<Guid>
    {
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public decimal ItemAmount
        {
            get
            {
                return this.Product.UnitPrice * this.Quantity;
            }
        }

        #region  Public Methods

        public OrderItem ConvertToOrderItem()
        {
            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                Product = this.Product,
                Quantity = this.Quantity
            };
            return orderItem;
        }

        public void UpdateQuantity(int quantity)
        {
            this.Quantity = quantity;
        }
        #endregion 
    }
}