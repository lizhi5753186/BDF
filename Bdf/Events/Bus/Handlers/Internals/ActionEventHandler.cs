using Bdf.Dependency;
using System;

namespace Bdf.Events.Bus.Handlers.Internals
{
    internal class ActionEventHandler<TEventData> : IEventHandler<TEventData>, ITransientDependency
    {
        /// <summary>
        /// Action to handle the event
        /// </summary>
        public Action<TEventData> Action { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handler"></param>
        public ActionEventHandler(Action<TEventData> handler)
        {
            Action = handler;
        }

        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}