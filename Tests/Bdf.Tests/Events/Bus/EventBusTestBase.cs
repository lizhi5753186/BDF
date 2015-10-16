using Bdf.Events.Bus;

namespace Bdf.Tests.Events.Bus
{
    public abstract class EventBusTestBase
    {
        protected IEventBus EventBus;

        protected EventBusTestBase()
        {
            EventBus = new EventBus();
        }
    }
}