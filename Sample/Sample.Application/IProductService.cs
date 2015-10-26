using System;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.Paged;
using Sample.Application.Dtos.Product;

namespace Sample.Application
{
    public interface IProductService
    {
        GetProductsOutput CreateProducts(CreateProductsInput input);

        GetCategoriesOutput CreateCategories(CreateCategoriesInput input);

        GetProductsOutput UpdateProducts(UpdateProductsInput input);

        GetCategoriesOutput UpdateCategories(UpdateCategoriesInput input);

        void DeleteProducts(DeleteProductsInput input);

        void DeleteCategories(DeleteCategoriesInput input);

        void UncategorizeProduct(Guid productId);

        ProductCategorizationDto CategorizeProduct(Guid productId, Guid categoryId);

        GetProductsOutput GetProducts();

        ProductDtoWithPagination GetProductsWithPagination(Pagination pagination);
        
        GetProductsOutput GetProductsForCategory(Guid categoryId);

        ProductDtoWithPagination GetProductsForCategoryWithPagination(Guid categoryId, Pagination pagination);

        GetProductsOutput GetNewProducts(int count);

        CategoryDto GetCategoryById(Guid id);
        GetCategoriesOutput GetCategories();

        ProductDto GetProductById(Guid id);
    }
}