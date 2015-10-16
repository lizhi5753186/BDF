using System;
using Bdf.Domain.Entities;

namespace Bdf.Sample.Domain.Model
{
    public class Category  :Entity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}