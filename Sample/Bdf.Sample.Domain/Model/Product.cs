using System;
using Bdf.Domain.Entities;

namespace Bdf.Sample.Domain.Model
{
    public class Product : Entity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public bool IsNew { get; set; }

        public int Inventory { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}