using System;
using Bdf.Events.Bus.Handlers;

namespace Bdf.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events 
    /// with a sigle intance object
    /// <remarks>
    /// This class always gets the same sigle instance of hanlder.
    /// </remarks>
    /// </summary>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// The event handler instance.
        /// </summary>
        public IEventHandler HandlerInstance { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handler"></param>
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            HandlerInstance = handler;
        }

        public IEventHandler GetHandler()
        {
            return HandlerInstance;
        }

        public void ReleaseHandler(Handlers.IEventHandler handler)
        {
        }
    }
}