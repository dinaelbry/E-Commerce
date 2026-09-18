using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Common.PaginationReasult
{
    public sealed class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageIndex, int pageSize, int totalCount, IReadOnlyList<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = totalCount;
            Data = data;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int Count { get; }
        public IReadOnlyList<TEntity> Data { get; }
    }
}
