using System;
using Bdf.Application.Services.Dto;
using Sample.Application.Dtos.Product;

namespace Sample.Application.Dtos.Order
{
    public class ShoppingCartItemDto : EntityDto<Guid>
    {
        public int? Quantity { get; set; }

        public ProductDto Product { get; set; }

        public decimal? ItemAmount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}