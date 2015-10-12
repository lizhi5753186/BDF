using Bdf.Events.Bus.Handlers;

namespace Bdf.Events.Bus.Factories
{
    /// <summary>
    /// This interface is used to factories those are reponsible to get and release event handlers.
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// Gets an event handler.
        /// </summary>
        /// <returns>The event handler</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// Releases an event handler.
        /// </summary>
        /// <param name="handler">Handle to be released</param>
        void ReleaseHandler(IEventHandler handler); 
    }
}