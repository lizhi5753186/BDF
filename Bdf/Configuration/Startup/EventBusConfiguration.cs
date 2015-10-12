namespace Bdf.Configuration.Startup
{
    /// <summary>
    /// <see cref="IEventBusConfiguration"/> implements
    /// </summary>
    internal class EventBusConfiguration : IEventBusConfiguration
    {
        public bool UseDefaultEventBus { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventBusConfiguration()
        {
            UseDefaultEventBus = true;
        }
    }
}