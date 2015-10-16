using System;
using Bdf.Domain.Entities;

namespace Bdf.Sample.Domain.Model
{
    public class ShoppingCart : Entity<Guid>
    {
        public virtual User User { get; set; }
    }
}