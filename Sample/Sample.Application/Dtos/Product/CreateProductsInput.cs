using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Product
{
    public class CreateProductsInput : IInputDto
    {
        public List<ProductDto> Products { get; set; }
    }
}