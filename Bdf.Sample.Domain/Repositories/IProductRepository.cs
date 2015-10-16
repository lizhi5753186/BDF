using System;
using System.Collections.Generic;
using Bdf.Domain.Repositories;
using Bdf.Sample.Domain.Model;

namespace Bdf.Sample.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        IEnumerable<Product> GetNewProducts(int count = 0);
    }
}