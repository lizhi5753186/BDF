using System;
using Bdf.Domain.Entities;

namespace Bdf.Sample.Domain.Model
{
    public class Role : Entity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}