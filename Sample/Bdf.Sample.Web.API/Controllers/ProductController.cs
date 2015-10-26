using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sample.Application;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.Paged;
using Sample.Application.Dtos.Product;

namespace Bdf.Sample.Web.API.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productServiceImp;

        public ProductController(IProductService productService)
        {
            _productServiceImp = productService;
        }

        [Route("Products")]
        public GetProductsOutput GetProducts()
        {
            return _productServiceImp.GetProducts();
        }

        [Route("ProductsForCategory")]
        public GetProductsOutput GetProductsForCategory(Guid categoryId)
        {
            return _productServiceImp.GetProductsForCategory(categoryId);
        }

        [Route("NewProducts")]
        public GetProductsOutput GetNewProducts(int count)
        {
            return _productServiceImp.GetNewProducts(count);
        }

        [Route("Category")]
        public CategoryDto GetCategoryById(Guid id)
        {
            return _productServiceImp.GetCategoryById(id);
        }

        [Route("Categories")]
        public GetCategoriesOutput GetCategories()
        {
            return _productServiceImp.GetCategories();
        }

        [Route("Product")]
        public ProductDto GetProductById(Guid id)
        {
            return _productServiceImp.GetProductById(id);
        }

        [Route("CreateProducts")]
        public GetProductsOutput CreateProducts(CreateProductsInput productsDtos)
        {
            return _productServiceImp.CreateProducts(productsDtos);
        }

        [Route("CreateCategories")]
        public GetCategoriesOutput CreateCategories(CreateCategoriesInput categoriDtos)
        {
            return _productServiceImp.CreateCategories(categoriDtos);
        }

        [Route("UpdateProducts")]
        public GetProductsOutput UpdateProducts(UpdateProductsInput productsDtos)
        {
            return _productServiceImp.UpdateProducts(productsDtos);
        }

        [Route("UpdateCategories")]
        public GetCategoriesOutput UpdateCategories(UpdateCategoriesInput categoriDtos)
        {
            return _productServiceImp.UpdateCategories(categoriDtos);
        }

        [Route("DeleteProducts")]
        public void DeleteProducts(DeleteProductsInput produtList)
        {
            _productServiceImp.DeleteProducts(produtList);
        }

        [Route("DeleteCategories")]
        public void DeleteCategories(DeleteCategoriesInput categoryList)
        {
            _productServiceImp.DeleteCategories(categoryList);
        }

        [Route("CategorizeProduct")]
        public ProductCategorizationDto CategorizeProduct(Guid productId, Guid categoryId)
        {
            return _productServiceImp.CategorizeProduct(productId, categoryId);
        }


        [Route("UncategorizeProduct")]
        public void UncategorizeProduct(Guid productId)
        {
            _productServiceImp.UncategorizeProduct(productId);
        }

        [Route("ProductsWithPagination")]
        public ProductDtoWithPagination GetProductsWithPagination(Pagination pagination)
        {
            return _productServiceImp.GetProductsWithPagination(pagination);
        }

        [Route("ProductsForCategoryWithPagination")]
        public ProductDtoWithPagination GetProductsForCategoryWithPagination(Guid categoryId, Pagination pagination)
        {
            return _productServiceImp.GetProductsForCategoryWithPagination(categoryId, pagination);
        }
    }
}
