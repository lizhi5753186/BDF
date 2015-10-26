using Bdf.Application.Services.Dto;
using Sample.Application.Dtos.Product;

namespace Sample.Application.Dtos.Paged
{
    public class ProductDtoWithPagination : PagedResultOutput<ProductDto>
    {
        public Pagination Pagination { get; set; }
    }
}