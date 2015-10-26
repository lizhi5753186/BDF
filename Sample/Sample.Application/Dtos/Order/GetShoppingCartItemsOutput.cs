using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Order
{
    public class GetShoppingCartItemsOutput : IOutputDto
    {
        public IList<ShoppingCartItemDto> Items { get; set; }
    }
}