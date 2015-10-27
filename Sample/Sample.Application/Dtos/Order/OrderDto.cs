﻿using System;
using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Order
{
    public class OrderDto : EntityDto<Guid>
    {
        public OrderStatusDto? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? DispatchedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserContact { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }

        public string UserAddressCountry { get; set; }
        public string UserAddressState { get; set; }
        public string UserAddressCity { get; set; }
        public string UserAddressStreet { get; set; }
        public string UserAddressZip { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
        public decimal? Subtotal { get; set; }

        public bool CanConfirm
        {
            get
            {
                return this.Status != null && this.Status == OrderStatusDto.Dispatched;
            }
        }
    }
}