using System;
using System.Collections.Generic;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Product
{
    public class DeleteCategoriesInput : IInputDto
    {
        public List<Guid> CategoryList { get; set; }
    }
}