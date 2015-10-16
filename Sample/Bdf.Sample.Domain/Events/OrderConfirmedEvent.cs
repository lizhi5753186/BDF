using System;
using Bdf.Events.Bus;

namespace Bdf.Sample.Domain.Events
{
    public class OrderConfirmedEvent : EventData
    {
        public DateTime ConfirmedDate { get; set; }
        public string UserEmailAddress { get; set; }
        public Guid OrderId { get; set; }
    }
}