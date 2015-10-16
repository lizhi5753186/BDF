using Bdf.Events.Bus;
using Bdf.Events.Bus.Handlers;

namespace Bdf.Sample.Domain.Events.EventHandlers
{
    public class OrderConfirmedEventHandler : IEventHandler<OrderConfirmedEvent>
    {
        public void HandleEvent(OrderConfirmedEvent eventData)
        {
            
        }
    }
}