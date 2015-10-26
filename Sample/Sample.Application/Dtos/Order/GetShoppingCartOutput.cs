using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Order
{
    public class GetShoppingCartOutput : IOutputDto
    {
        public ShoppingCartDto ShoppingCart { get; set; }
    }
}