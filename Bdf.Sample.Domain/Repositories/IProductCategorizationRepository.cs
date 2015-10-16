using System;
using System.Collections.Generic;
using Bdf.Domain.Repositories;
using Bdf.Sample.Domain.Model;

namespace Bdf.Sample.Domain.Repositories
{
    public interface IProductCategorizationRepository : IRepository<ProductCategorization, Guid>
    {
        IEnumerable<Product> GetProductsForCategory(Category category);

        Category GetCategoryForProduct(Product product);
    }
}