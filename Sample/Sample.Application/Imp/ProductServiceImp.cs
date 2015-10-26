using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using Bdf.Application.Services;
using Bdf.Domain.Services;
using Bdf.Linq.Extensions;
using Bdf.Sample.Domain.Model;
using Bdf.Sample.Domain.Repositories;
using Bdf.Sample.Domain.Services;
using Castle.Core.Internal;
using Sample.Application.Dtos.Order;
using Sample.Application.Dtos.Paged;
using Sample.Application.Dtos.Product;

namespace Sample.Application.Imp
{
    public class ProductServiceImp : ApplicationService, IProductService
    {
        #region Private Fields
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductCategorizationRepository _productCategorizationRepository;
        private readonly IDomainService _domainService;
        #endregion

        #region Ctor
        public ProductServiceImp(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductCategorizationRepository productCategorizationRepository,
            IDomainService domainService)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productCategorizationRepository = productCategorizationRepository;
            _domainService = domainService;
        }

        #endregion

        public GetProductsOutput CreateProducts(CreateProductsInput input)
        {
            var productDtos = this.PerformCreateObjects<List<ProductDto>, ProductDto, Product>(input.Products, _productRepository);
            return new GetProductsOutput
            {
                Products = productDtos
            };
        }

        public GetCategoriesOutput CreateCategories(CreateCategoriesInput input)
        {
            var categoryDtos =this.PerformCreateObjects<List<CategoryDto>, CategoryDto, Category>(input.Categories, _categoryRepository);
            return new GetCategoriesOutput
            {
                Categories = categoryDtos
            };
        }

        public GetProductsOutput UpdateProducts(UpdateProductsInput input)
        {
            var productDtos = this.PerformUpdateObjects<List<ProductDto>, ProductDto, Product>(input.Products,
                _productRepository,
                pdto => pdto.Id.ToString(),
                (p, pdto) =>
                {
                    if (!string.IsNullOrEmpty(pdto.Description))
                        p.Description = pdto.Description;
                    if (!string.IsNullOrEmpty(pdto.ImageUrl))
                        p.ImageUrl = pdto.ImageUrl;
                    if (!string.IsNullOrEmpty(pdto.Name))
                        p.Name = pdto.Name;
                    if (pdto.IsNew != null)
                        p.IsNew = pdto.IsNew.Value;
                    if (pdto.UnitPrice != null)
                        p.UnitPrice = pdto.UnitPrice.Value;
                });

            return new GetProductsOutput
            {
                Products = productDtos
            };
        }

        public GetCategoriesOutput UpdateCategories(UpdateCategoriesInput input)
        {
            var categoryDtos = this.PerformUpdateObjects<List<CategoryDto>, CategoryDto, Category>(input.Categories,
                _categoryRepository,
                cdto => cdto.Id.ToString(),
                (c, cdto) =>
                {
                    if (!string.IsNullOrEmpty(cdto.Name))
                        c.Name = cdto.Name;
                    if (!string.IsNullOrEmpty(cdto.Description))
                        c.Description = cdto.Description;
                });
            return new GetCategoriesOutput
            {
                Categories = categoryDtos
            };
        }

        public void DeleteProducts(DeleteProductsInput input)
        {
            this.PerformDeleteObjects<Product>(input.ProductList,
                _productRepository,
                id =>
                {
                    var categorization = _productCategorizationRepository.Single(c => c.Product.Id == id);
                    if (categorization != null)
                        _productCategorizationRepository.Delete(categorization);
                });
        }

        public void DeleteCategories(DeleteCategoriesInput input)
        {
            this.PerformDeleteObjects<Category>(input.CategoryList,
                _categoryRepository,
                id =>
                {
                    var categorization = _productCategorizationRepository.Single(c => c.Category.Id == id);
                    if (categorization != null)
                        _productCategorizationRepository.Delete(categorization);
                });
        }

        public ProductCategorizationDto CategorizeProduct(Guid productId, Guid categoryId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException("productId");
            if (categoryId == Guid.Empty)
                throw new ArgumentNullException("categoryId");
            var product = _productRepository.Get(productId);
            var category = _categoryRepository.Get(categoryId);
            return Mapper.Map<ProductCategorization, ProductCategorizationDto>(_domainService.Categorize(product, category));
        }

        public void UncategorizeProduct(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException("productId");
            var product = _productRepository.Get(productId);
            _domainService.Uncategorize(product);
        }

        public GetProductsOutput GetProducts()
        {
            var productDtos = new List<ProductDto>();

            _productRepository.
               GetAll().
               ToList().
               ForEach(p =>
               {
                   var productDto = Mapper.Map<Product, ProductDto>(p);
                   {
                       var category = _productCategorizationRepository.GetCategoryForProduct(p);
                       if (category != null)
                           productDto.Category = Mapper.Map<Category, CategoryDto>(category);
                   }

                   productDtos.Add(productDto);
               });

            return new GetProductsOutput
            {
                Products = productDtos
            };
        }

        public GetProductsOutput GetProductsForCategory(Guid categoryId)
        {
            var productDtos = new List<ProductDto>();

            var category = _categoryRepository.Get(categoryId);
            var products = _productCategorizationRepository.GetProductsForCategory(category);
            products.ToList().ForEach(p => productDtos.Add(Mapper.Map<Product, ProductDto>(p)));
            return new GetProductsOutput
            {
                Products = productDtos
            };
        }

        public GetProductsOutput GetNewProducts(int count)
        {
            var newProducts = new List<ProductDto>();
            _productRepository.GetNewProducts(count)
                .ToList()
                .ForEach
                (
                    np => newProducts.Add(Mapper.Map<Product, ProductDto>(np))
                );

            return new GetProductsOutput()
            {
                Products = newProducts
            };
        }

        public CategoryDto GetCategoryById(Guid id)
        {
            var category = _categoryRepository.Get(id);
            var result = Mapper.Map<Category, CategoryDto>(category);

            return result;
        }

        public GetCategoriesOutput GetCategories()
        {
            var categoryDtos = new List<CategoryDto>();

            _categoryRepository.GetAll().ToList().ForEach(c =>
            {
                var categoryDto = Mapper.Map<Category, CategoryDto>(c);
                categoryDtos.Add(categoryDto);
            });

            return new GetCategoriesOutput
            {
                Categories = categoryDtos
            };
        }

        public ProductDto GetProductById(Guid id)
        {
            var product = _productRepository.Get(id);
            var result = Mapper.Map<Product, ProductDto>(product);
            result.Category =
                Mapper.Map<Category, CategoryDto>(_productCategorizationRepository.GetCategoryForProduct(product));
            return result;
        }


        public ProductDtoWithPagination GetProductsWithPagination(Pagination pagination)
        {
            var products = _productRepository.GetAll().OrderBy(p=>p.Name);
            var skip = (pagination.PageNumber - 1)*pagination.PageSize;
            var pagedProducts = products.PageBy(skip, pagination.PageSize);
            if (pagedProducts == null)
                return null;
            pagination.TotalPages = (products.Count() + pagination.PageSize - 1) / pagination.PageSize;

            var productDtoList = new List<ProductDto>();
            pagedProducts.ForEach(p=>productDtoList.Add(Mapper.Map<Product, ProductDto>(p)));
            
            return new ProductDtoWithPagination
            {
                Pagination = pagination,
                Items = productDtoList
            };
        }

        public ProductDtoWithPagination GetProductsForCategoryWithPagination(Guid categoryId, Pagination pagination)
        {
            var products = _productCategorizationRepository.GetAllList(pc => pc.Category.Id == categoryId).Select(pc => pc.Product).OrderBy(p => p.Name);
            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            var pagedProducts = products.AsQueryable().PageBy(skip, pagination.PageSize);
            if (pagedProducts == null)
                return null;

            pagination.TotalPages = (products.Count() + pagination.PageSize - 1) / pagination.PageSize;

            var productDtoList = new List<ProductDto>();
            pagedProducts.ForEach(p => productDtoList.Add(Mapper.Map<Product, ProductDto>(p)));

            return new ProductDtoWithPagination
            {
                Pagination = pagination,
                Items = productDtoList
            };
        }
    }
}