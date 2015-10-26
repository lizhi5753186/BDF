using System;
using Bdf.Application.Services.Dto;
using Sample.Application.Dtos.Product;

namespace Sample.Application.Dtos.Order
{
    public class OrderItemDto : EntityDto<Guid>
    {
        public int? Quantity { get; set; }
        public string OrderId { get; set; }
        public ProductDto Product { get; set; }
        public decimal? ItemAmount { get; set; }
    }
}