using System;
using NHibernate;
using NHibernate.Type;
using Bdf.Dependency;
using Bdf.Domain.Entities;
using Bdf.Domain.Entities.Auditing;
using Bdf.Events.Bus.Entities;
using Bdf.Extensions;
using Bdf.Runtime.Session;
using Bdf.Timing;

namespace Bdf.NHibernate.Interceptors
{
    public class BdfNHibernateInterceptor : EmptyInterceptor
    {
        public IEntityChangedEventHelper EntityChangedEventHelper { get; set; }

        private readonly IIocManager _iocManager;
        private readonly Lazy<IBdfSession> _BdfSession;
        public BdfNHibernateInterceptor(IIocManager iocManager)
        {
            _iocManager = iocManager;
             _BdfSession =
                new Lazy<IBdfSession>(
                    () => _iocManager.IsRegistered(typeof(IBdfSession))
                        ? _iocManager.Resolve<IBdfSession>()
                        : NullBdfSession.Instance
                    );

            EntityChangedEventHelper =
                _iocManager.IsRegistered(typeof (IEntityChangedEventHelper))
                    ? _iocManager.Resolve<IEntityChangedEventHelper>()
                    : NullEntityChangedEventHelper.Instance;
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            //Set CreationTime for new entity
            if (entity is IHasCreationTime)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "CreationTime")
                    {
                        state[i] = (entity as IHasCreationTime).CreationTime = ClockManager.Now;
                    }
                }
            }

            //Set CreatorUserId for new entity
            if (entity is ICreationAudited)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "CreatorUserId")
                    {
                        state[i] = (entity as ICreationAudited).CreatorUserId = _BdfSession.Value.UserId;
                    }
                }
            }

            EntityChangedEventHelper.TriggerEntityCreatedEvent(entity);

            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            //Set modification audits
            if (entity is IModificationAudited)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "LastModificationTime")
                    {
                        currentState[i] = (entity as IModificationAudited).LastModificationTime = ClockManager.Now;
                    }
                    else if (propertyNames[i] == "LastModifierUserId")
                    {
                        currentState[i] = (entity as IModificationAudited).LastModifierUserId = _BdfSession.Value.UserId;
                    }
                }
            }

            //Set deletion audits
            if (entity is IDeletionAudited && (entity as IDeletionAudited).IsDeleted)
            {
                //Is deleted before? Normally, a deleted entity should not be updated later but I preferred to check it.
                var previousIsDeleted = false;
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "IsDeleted")
                    {
                        previousIsDeleted = (bool)previousState[i];
                        break;
                    }
                }

                if (!previousIsDeleted)
                {
                    for (var i = 0; i < propertyNames.Length; i++)
                    {
                        if (propertyNames[i] == "DeletionTime")
                        {
                            currentState[i] = (entity as IDeletionAudited).DeletionTime = ClockManager.Now;
                        }
                        else if (propertyNames[i] == "DeleterUserId")
                        {
                            currentState[i] = (entity as IDeletionAudited).DeleterUserId = _BdfSession.Value.UserId;
                        }
                    }
                }
            }

            if (entity is ISoftDelete && entity.As<ISoftDelete>().IsDeleted)
            {
                EntityChangedEventHelper.TriggerEntityDeletedEvent(entity);
            }
            else
            {
                EntityChangedEventHelper.TriggerEntityUpdatedEvent(entity);
            }

            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            EntityChangedEventHelper.TriggerEntityDeletedEvent(entity);

            base.OnDelete(entity, id, state, propertyNames, types);
        }
    }
}