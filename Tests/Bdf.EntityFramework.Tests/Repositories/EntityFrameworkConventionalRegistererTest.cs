using System.Reflection;
using Bdf.Configuration.Startup;
using Bdf.Dependency;
using Bdf.EntityFramework.Dependency;
using Bdf.Tests;
using Shouldly;
using Xunit;

namespace Bdf.EntityFramework.Tests.Repositories
{
    public class EntityFrameworkConventionalRegistererTest : TestBaseWithLocalIocManager
    {
        [Fact]
        public void Should_Set_ConnectionString_If_Configured()
        {
            new EntityFrameworkConventionalRegisterer()
                .RegisterAssembly(
                new ConventionalRegistrationContext(
                    Assembly.GetExecutingAssembly(),
                    LocalIocManager,
                    new ConventionalRegistrationConfig()));

            // should call default constructor since IBdfStartupConfiguration is not configured. 
            var context1 = LocalIocManager.Resolve<TestDbContext>();
            context1.CalledConstructorWithConnectionString.ShouldBe(false);

            LocalIocManager.Register<IBdfStartupConfiguration, BdfStartupConfiguration>();

            //Should call default constructor since IBdfStartupConfiguration registered by IBdfStartupConfiguration.DefaultNameOrConnectionString is not set. 
            var context2 = LocalIocManager.Resolve<TestDbContext>();
            context2.CalledConstructorWithConnectionString.ShouldBe(false);

            LocalIocManager.Resolve<IBdfStartupConfiguration>().DefaultNameOrConnectionString = "Server=localhost;Database=test;User=sa;Password=123";

            //Should call constructor with nameOrConnectionString since IBdfStartupConfiguration.DefaultNameOrConnectionString is set.
            var context3 = LocalIocManager.Resolve<TestDbContext>();
            context3.CalledConstructorWithConnectionString.ShouldBe(true);
        }

        public class TestDbContext :BdfDbContext
        {
            public bool CalledConstructorWithConnectionString { get; private set; }

            public TestDbContext()
            {

            }

            public TestDbContext(string nameOrConnectionString)
                : base(nameOrConnectionString)
            {
                CalledConstructorWithConnectionString = true;
            }

            public override void Initialize()
            {

            }
        }
    }
}