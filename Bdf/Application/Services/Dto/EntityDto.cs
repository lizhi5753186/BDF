using System;

namespace Bdf.Application.Services.Dto
{
    /// <summary>
    /// A shortcut of <see cref="EntityDto{TPrimaryKey}"/> with primary key type <see cref="int"/>
    /// </summary>
    [Serializable]
    public abstract class EntityDto : EntityDto<int>, IEntityDto
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected EntityDto()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        protected EntityDto(int id)
            : base(id)
        {
        }
    }
}