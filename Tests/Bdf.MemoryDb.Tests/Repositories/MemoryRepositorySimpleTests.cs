using Bdf.Domain.Entities;
using Bdf.Domain.Repositories;
using Bdf.MemoryDb.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Bdf.MemoryDb.Tests.Repositories
{
    public class MemoryRepositorySimpleTests
    {
        private readonly MemoryDatabase _database;
        private readonly IRepository<TestEntity> _repository;

        public MemoryRepositorySimpleTests()
        {
            _database = new MemoryDatabase();
            var databaseProvider = Substitute.For<IMemoryDatabaseProvider>();
            databaseProvider.Database.Returns(_database);

            _repository = new MemoryRepository<TestEntity>(databaseProvider);

            // Testing Insert by creating initial data
            _repository.Insert(new TestEntity("test1"));
            _repository.Insert(new TestEntity("test2"));
            _database.Set<TestEntity>().Count.ShouldBe(2);
        }

        [Fact]
        public void Count_Test()
        {
            _repository.Count().ShouldBe(2);
        }

        [Fact]
        public void Delete_Test()
        {
            var test1 = _repository.FirstOrDefault(e => e.Name == "test1");
            test1.ShouldNotBe(null);

            _repository.Delete(test1);

            test1 = _repository.FirstOrDefault(e => e.Name == "test1");
            test1.ShouldBe(null);
        }
        public class TestEntity : Entity
        {
            public string Name { get; set; }

            public TestEntity()
            {
            }

            public TestEntity(string name)
            {
                Name = name;
            }
        }
    }
}