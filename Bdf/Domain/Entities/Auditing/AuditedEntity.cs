using System;
using Bdf.Domain.Entities.Auditing;

namespace Bdf.Domain.Entities.Auditing 
{
    /// <summary>
    /// A shortcut of <see cref="AuditedEntity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class AuditedEntity : AuditedEntity<int>
    {

    }
}