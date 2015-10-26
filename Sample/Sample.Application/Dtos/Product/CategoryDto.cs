using System;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Product
{
    public class CategoryDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}