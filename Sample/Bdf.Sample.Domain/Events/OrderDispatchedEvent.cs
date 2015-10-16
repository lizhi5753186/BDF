using System;
using Bdf.Events.Bus;

namespace Bdf.Sample.Domain.Events
{
    public class OrderDispatchedEvent  :EventData
    {
        public DateTime DispatchedDate { get; set; }
        public string UserEmailAddress { get; set; }
        public Guid OrderId { get; set; }
    }
}