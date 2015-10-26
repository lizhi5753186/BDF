using System;
using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Order
{
    public class ShoppingCartDto : EntityDto<Guid>
    {
        public string CustomerId { get; set; }

        public IList<ShoppingCartItemDto> Items { get; set; }

        public decimal? Subtotal { get; set; }
    }
}