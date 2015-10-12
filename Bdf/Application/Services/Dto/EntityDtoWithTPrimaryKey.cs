using System;

namespace Bdf.Application.Services.Dto
{
    /// <summary>
    /// Implements common properties for entity based DTOs.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    [Serializable]
    public abstract class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public TPrimaryKey Id { get; set; }

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
        protected EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }
}