using System;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Order
{
    public class ProductCategorizationDto : EntityDto<Guid>
    {
        public Guid CategoryId { get; set; }

        public Guid ProductId { get; set; } 
    }
}