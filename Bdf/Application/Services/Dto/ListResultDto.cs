using System;
using System.Collections.Generic;

namespace Bdf.Application.Services.Dto
{
    /// <summary>
    /// Implements <see cref="IListResult{T}"/>
    /// </summary>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        /// <summary>
        /// List of item
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ListResultDto()
        {
        }

        /// <summary>
        ///  Constructor.
        /// </summary>
        /// <param name="items">List of item</param>
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}