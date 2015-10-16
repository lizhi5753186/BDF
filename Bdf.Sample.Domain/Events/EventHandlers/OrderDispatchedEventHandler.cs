using Bdf.Events.Bus;
using Bdf.Events.Bus.Handlers;

namespace Bdf.Sample.Domain.Events.EventHandlers
{
    public class OrderDispatchedEventHandler: IEventHandler<OrderDispatchedEvent>
    {
        public void HandleEvent(OrderDispatchedEvent eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}