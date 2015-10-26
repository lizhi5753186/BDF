using System;
using Bdf.Domain.Entities;

namespace Bdf.Sample.Domain.Model
{
    public class ProductCategorization : Entity<Guid>
    {
        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }

        public override string ToString()
        {
            return string.Format("CategoryID: {0}, ProductID: {1}", this.Category.Id, this.Product.Id);
        }

        // 通过商品对象和分类对象来创建商品分类对象
        public static ProductCategorization CreateCategorization(Product product, Category category)
        {
            return new ProductCategorization { Product = product, Category = category };
        }
    }
}