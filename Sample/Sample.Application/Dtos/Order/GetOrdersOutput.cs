using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Order
{
    public class GetOrdersOutput : IOutputDto
    {
        public IList<OrderDto> Orders { get; set; }
    }
}