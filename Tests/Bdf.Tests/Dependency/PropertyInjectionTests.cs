using Bdf.Application.Services;
using Bdf.Runtime.Session;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Dependency
{
    public class PropertyInjectionTests : TestBaseWithLocalIocManager
    {
        [Fact]
        public void Should_Inject_Session_For_ApplicationService()
        {
            var session = Substitute.For<IBdfSession>();
            session.UserId.Returns(42);

            LocalIocManager.Register<MyApplicationService>();
            LocalIocManager.IocContainer.Register(
                Component.For<IBdfSession>().UsingFactoryMethod(() => session)
                );

            var myAppService = LocalIocManager.Resolve<MyApplicationService>();
            myAppService.TestSession();
        }

        private class MyApplicationService : ApplicationService
        {
            public void TestSession()
            {
                BdfSession.ShouldNotBe(null);
                BdfSession.UserId.ShouldBe(42);
            }
        } 
    }
}