using System.Linq;
using Bdf.Dependency;
using Bdf.Domain.Repositories;
using Bdf.Events.Bus;
using Bdf.Events.Bus.Entities;
using Bdf.NHibernate.Interceptors;
using Bdf.NHibernate.Repositories;
using Bdf.Uow;
using FluentNHibernate.Cfg;
using NHibernate.Linq;
using Shouldly;
using Xunit;

namespace Bdf.NHibernate.Tests
{
    public class BasicRepositoryTests : NHibernateTestBase
    {
        private readonly IRepository<Person> _personRepository;

        public BasicRepositoryTests()
        {
            var sessionFactory = FluentConfiguration
                .ExposeConfiguration(
                    config => config.SetInterceptor(LocalIocManager.Resolve<BdfNHibernateInterceptor>()))
                .BuildSessionFactory();

            LocalIocManager.IocContainer.Install(new NhRepositoryInstaller(sessionFactory));
           
            _personRepository = LocalIocManager.Resolve<IRepository<Person>>();
            UsingSession(session => session.Save(new Person() { Name = "Leaninghard" }));
        }

        [Fact]
        public void Should_Get_All_People()
        {
            _personRepository.GetAllList().Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Insert_People()
        {
            _personRepository.Insert(new Person() { Name = "Leaninghard2" });

            var insertedPerson = UsingSession(session => session.Query<Person>().FirstOrDefault(p => p.Name == "Leaninghard2"));
            insertedPerson.ShouldNotBe(null);
            insertedPerson.Name.ShouldBe("Leaninghard2");
        }

        [Fact]
        public void Update_With_Action_Test()
        {
            var userBefore = UsingSession(session => session.Query<Person>().Single(p => p.Name == "Leaninghard"));

            var updatedUser = _personRepository.Update(userBefore.Id, user => user.Name = "yunus");
            updatedUser.Id.ShouldBe(userBefore.Id);
            updatedUser.Name.ShouldBe("yunus");

            var userAfter = UsingSession(session => session.Get<Person>(userBefore.Id));
            userAfter.Name.ShouldBe("yunus");
        }

        [Fact]
        public void Should_Trigger_Event_On_Insert()
        {
            var triggerCount = 0;

            LocalIocManager.Register(typeof(IEventBus), DependencyLifeStyle.Transient);
            LocalIocManager.Resolve<IEventBus>().Register<EntityCreatedEventData<Person>>(
                eventData =>
                {
                    eventData.Entity.Name.ShouldBe("Learning");
                    triggerCount++;
                });

            _personRepository.Insert(new Person { Name = "Learning" });

            triggerCount.ShouldBe(1);
        }

        [Fact]
        public void Should_Trigger_Event_On_Update()
        {
            var triggerCount = 0;

            LocalIocManager.Register(typeof(IEventBus), DependencyLifeStyle.Transient);
            LocalIocManager.Resolve<IEventBus>().Register<EntityUpdatedEventData<Person>>(
                eventData =>
                {
                    eventData.Entity.Name.ShouldBe("Learning2");
                    triggerCount++;
                });

            var emrePeson = _personRepository.Single(p => p.Name == "Leaninghard");
            emrePeson.Name = "Learning2";
            _personRepository.Update(emrePeson);

            triggerCount.ShouldBe(1);
        }

        [Fact]
        public void Should_Trigger_Event_On_Delete()
        {
            var triggerCount = 0;

            LocalIocManager.Register(typeof(IEventBus), DependencyLifeStyle.Transient);
            LocalIocManager.Resolve<IEventBus>().Register<EntityDeletedEventData<Person>>(
                eventData =>
                {
                    eventData.Entity.Name.ShouldBe("Leaninghard");
                    triggerCount++;
                });

            var emrePeson = _personRepository.Single(p => p.Name == "Leaninghard");
            _personRepository.Delete(emrePeson.Id);

            triggerCount.ShouldBe(1);
            _personRepository.FirstOrDefault(p => p.Name == "Leaninghard").ShouldBe(null);
        }
    }
}