using Bdf.Domain.Entities;

namespace Bdf.NHibernate.Tests
{
    public class Person : Entity
    {
        public const int MaxNameLength = 64;

        public virtual string Name { get; set; }
    }
}