using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Product
{
    public class GetCategoriesOutput : IOutputDto
    {
        public List<CategoryDto> Categories { get; set; }
    }
}