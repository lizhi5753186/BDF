using System;
using System.Collections.Generic;

namespace Bdf.Application.Services.Dto
{
    /// <summary>
    /// This class can be used to return a paged list from an <see cref="IApplicationService"/> method.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="PagedResultDto{T}.Items"/> list</typeparam>
    [Serializable]
    public class PagedResultOutput<T> : ListResultDto<T>, IPagedResult<T>, IOutputDto
    {
        /// <summary>
        /// Total count of Items.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Creates a new <see cref="PagedResultOutput{T}"/> object.
        /// </summary>
        public PagedResultOutput()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="totalCount">Total count of Items</param>
        /// <param name="items">List of items in current page</param>
        public PagedResultOutput(int totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
        }

        
    }
}