namespace Bdf.Events.Bus.Handlers
{
    /// <summary>
    /// Defines an interface for a class that handles event of type <see cref="TEventData"/>
    /// </summary>
    /// <typeparam name="TEventData">Event type to handle</typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        /// Handlers must implementing this method to handle event.
        /// </summary>
        /// <param name="eventData"></param>
        void HandleEvent(TEventData eventData);
    }
}