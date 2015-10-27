using System;
using Bdf.Application.Services.Dto;

namespace Sample.Application.Dtos.Product
{
    public class ProductDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public bool? IsNew { get; set; }

        public CategoryDto Category { get; set; }

        public string CategoryName
        {
            get
            {
                if (this.Category == null)
                    return "(未分类)";
                else
                    return this.Category.Name;
            }
        }
    }
}