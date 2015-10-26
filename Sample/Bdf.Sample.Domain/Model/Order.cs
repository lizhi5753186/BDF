using System;
using System.Collections.Generic;
using System.Linq;
using Bdf.Domain.Entities;
using Bdf.Domain.Entities.Auditing;

namespace Bdf.Sample.Domain.Model
{
    public class Order : Entity<Guid>, IHasCreationTime
    {
        private List<OrderItem> _orderItems;

        public Order()
        {
            CreationTime = DateTime.Now;
            _orderItems =new List<OrderItem>();
            Status = OrderStatus.Created;
        }

        public OrderStatus Status { get; set; }
        public DateTime CreationTime { get; set; }

        public DateTime? DispatchedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }
        public virtual List<OrderItem> OrderItems
        {
            get
            {
                return _orderItems;
            }
            set
            {
                _orderItems = value;
            }
        }

        public virtual User User { get; set; }

        public Address DeliveryAddress
        {
            get
            {
                return User.DeliveryAddress;
            }
        }

        public decimal Subtotal
        {
            get
            {
                return this.OrderItems.Sum(p => p.ItemAmout);
            }
        }


        public void Confirm()
        {
            
        }

        public void Dispatch()
        {

        }
    }
}