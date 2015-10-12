using System;

namespace Bdf.Events.Bus
{
    /// <summary>
    /// the interface of event data
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// Thie time when the event occured.
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional)
        /// </summary>
        object EventSource { get; set; }
    }
}