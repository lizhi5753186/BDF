using Bdf.NHibernate.EntityMappings;

namespace Bdf.NHibernate.Tests
{
    public class PersonMap : EntityMap<Person>
    {
        public PersonMap()
            : base("People")
        {
            Map(x => x.Name);
        }
    }
}