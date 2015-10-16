using System;
using System.Data.Entity;
using Bdf.Domain.Entities;
using Bdf.Domain.Repositories;
using Bdf.EntityFramework.Repositories;
using Bdf.Tests;
using Castle.MicroKernel.Registration;
using NSubstitute;
using Shouldly;
using Xunit;


namespace Bdf.EntityFramework.Tests.Repositories
{
    public class EntityFrameworkGenericRepositoryRegistrarTests : TestBaseWithLocalIocManager
    {
        public EntityFrameworkGenericRepositoryRegistrarTests()
        {
            var fakeMainDbContextProvider = Substitute.For<IDbContextProvider<TestMainDbContext>>();
            var fakeTestDbContextProvider = Substitute.For<IDbContextProvider<TestDbContext>>();

            LocalIocManager.IocContainer.Register(
                Component.For<IDbContextProvider<TestMainDbContext>>()
                    .UsingFactoryMethod(() => fakeMainDbContextProvider),
                Component.For<IDbContextProvider<TestDbContext>>().UsingFactoryMethod(() => fakeTestDbContextProvider));

            // Register Repository For Entity
            EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(typeof(TestDbContext), LocalIocManager);
            EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(typeof(TestMainDbContext), LocalIocManager);
        }

        [Fact]
        public void Should_Resolve_Generic_Repositories()
        {
            // Entity 1 (with default PK)
            var entity1Repository = LocalIocManager.Resolve<IRepository<TestEntity1>>();
            entity1Repository.ShouldNotBeNull();
            (entity1Repository is EfRepositoryBase<TestMainDbContext, TestEntity1>).ShouldBeTrue();

            //Entity 1 (with specified PK)
            var entity1RepositoryWithPk = LocalIocManager.Resolve<IRepository<TestEntity1, int>>();
            entity1RepositoryWithPk.ShouldNotBe(null);
            (entity1RepositoryWithPk is EfRepositoryBase<TestMainDbContext, TestEntity1, int>).ShouldBe(true);

            //Entity 2
            var entity2Repository = LocalIocManager.Resolve<IRepository<TestEntity2, long>>();
            (entity2Repository is EfRepositoryBase<TestMainDbContext, TestEntity2, long>).ShouldBe(true);
            entity2Repository.ShouldNotBe(null);

            //Entity 3
            var entity3Repository = LocalIocManager.Resolve<ITestRepository<TestEntity3, Guid>>();
            (entity3Repository is EfRepositoryBase<TestDbContext, TestEntity3, Guid>).ShouldBe(true);
            entity3Repository.ShouldNotBe(null);
        }

        public class TestMainDbContext : TestBaseDbContext
        {
            public virtual DbSet<TestEntity2> TestEntities2 { get; set; }
        }

        public class TestBaseDbContext : BdfDbContext
        {
            public virtual DbSet<TestEntity1> TestEntities1 { get; set; }
        }

         [AutoRepositoryTypes(
            typeof(ITestRepository<>),
            typeof(ITestRepository<,>),
            typeof(TestRepositoryBase<>),
             typeof(TestRepositoryBase<,>)
            )]
        public class TestDbContext : TestBaseDbContext
        {
            public virtual DbSet<TestEntity3> TestEntities3 { get; set; }
        }

        public class TestEntity1 : Entity
        {
            
        }

        public class TestEntity2 : Entity<long>
        {
            
        }

        public class TestEntity3 : Entity<Guid>
        {
            
        }

        public interface ITestRepository<TEntity> : IRepository<TEntity>
            where TEntity :class, IEntity<int>
        {
        }

        public interface ITestRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
           where TEntity : class, IEntity<TPrimaryKey>
        {
        }

        public class TestRepositoryBase<TEntity, TPrimaryKey> :EfRepositoryBase<TestDbContext, TEntity, TPrimaryKey>, ITestRepository<TEntity, TPrimaryKey>
            where TEntity : class, IEntity<TPrimaryKey>
        {
            public TestRepositoryBase(IDbContextProvider<TestDbContext> dbContextProvider)
                : base(dbContextProvider)
            {
            }
        }

        public class TestRepositoryBase<TEntity> : TestRepositoryBase<TEntity, int>, ITestRepository<TEntity>
           where TEntity : class, IEntity<int>
        {
            public TestRepositoryBase(IDbContextProvider<TestDbContext> dbContextProvider)
                : base(dbContextProvider)
            {
            }
        }
    }
}