using Bdf.Domain.Entities;
using Bdf.Events.Bus.Entities;
using Shouldly;
using Xunit;

namespace Bdf.Tests.Events.Bus
{
    public class GenericInheritanceTest : EventBusTestBase
    {
        [Fact]
        public void Should_Trigger_For_Inherited_Generic_1()
        {
            var triggerEvent = false;

            EventBus.Register<EntityChangedEventData<Person>>(eventData =>
                {
                    eventData.Entity.Id.ShouldBe(12);
                    triggerEvent = true;
                });

            EventBus.Trigger(new EntityUpdatedEventData<Person>(new Person { Id = 12}));
            triggerEvent.ShouldBeTrue();
        }

        [Fact]
        public void Should_Trigger_For_Inherited_Generic_2()
        {
            var triggeredEvent = false;

            EventBus.Register<EntityChangedEventData<Person>>(
                eventData =>
                {
                    eventData.Entity.Id.ShouldBe(12);
                    triggeredEvent = true;
                });

            EventBus.Trigger(new EntityChangedEventData<Student>(new Student { Id = 12 }));

            triggeredEvent.ShouldBe(true);
        }

        public class Person :Entity
        {
            
        }

        public class Student: Person
        {
             
        }
    }
}