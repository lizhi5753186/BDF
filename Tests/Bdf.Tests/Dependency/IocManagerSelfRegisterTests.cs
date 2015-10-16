using Bdf.Dependency;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Dependency
{
    public class IocManagerSelfRegisterTests : TestBaseWithLocalIocManager
    {

        [Fact]
        public void Should_Self_Register_With_All_Interfaces()
        {
            var registrar = LocalIocManager.Resolve<IIocRegistrar>();
            var resolver = LocalIocManager.Resolve<IIocResolver>();
            var managerByInterface = LocalIocManager.Resolve<IIocManager>();
            var managerByClass = LocalIocManager.Resolve<IocManager>();

            managerByClass.ShouldBeSameAs(registrar);
            managerByClass.ShouldBeSameAs(resolver);
            managerByClass.ShouldBeSameAs(managerByInterface);
        }
    }
}