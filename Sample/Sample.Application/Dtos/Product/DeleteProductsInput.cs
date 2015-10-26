using System;
using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Product
{
    public class DeleteProductsInput : IInputDto
    {
        public List<Guid> ProductList { get; set; } 
    }
}