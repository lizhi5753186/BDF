using System;
using Bdf.Events.Bus.Handlers;

namespace Bdf.Events.Bus.Factories.Internals
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to handle events 
    /// with a Transient intance object
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory
        where THandler : IEventHandler, new()
    {

        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}