using Bdf.Domain.Services;
using Bdf.Sample.Domain.Model;

namespace Bdf.Sample.Domain.Services
{
    public static  class DomainServiceExtensions
    {
        public static Order CreateOrder(this IDomainService domainService, User user, ShoppingCart shoppingCart)
        {
            return ((SampleDomainService) domainService).CreateOrder(user, shoppingCart);
        }

        public static ProductCategorization Categorize(this IDomainService domainService, Product product, Category category)
        {
            return ((SampleDomainService)domainService).Categorize(product, category);
        }

        public static void Uncategorize(this IDomainService domainService, Product product, Category category = null)
        {
            ((SampleDomainService)domainService).Uncategorize(product, category);
        }

        public static UserRole AssignRole(this IDomainService domainService, User user, Role role)
        {
            return ((SampleDomainService) domainService).AssignRole(user, role);
        }

        public static void UnassignRole(this IDomainService domainService, User user, Role role = null)
        {
            ((SampleDomainService) domainService).UnassignRole(user, role);
        }
    }
}