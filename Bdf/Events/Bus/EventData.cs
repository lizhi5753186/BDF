using System;
using Bdf.Timing;

namespace Bdf.Events.Bus
{
    public abstract class EventData : IEventData
    {
        public DateTime EventTime { get; set; }

        public object EventSource { get; set; }

        protected EventData()
        {
            EventTime = ClockManager.Now;
        }

    }
}