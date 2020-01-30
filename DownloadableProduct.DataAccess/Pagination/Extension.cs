using System.Linq;
using DownloadableProduct.Domain.Dto.Pagination;
using DownloadableProduct.Domain.Entities;

namespace DownloadableProduct.DataAccess.Pagination
{
    public static class Extension
    {
        public static PaginationDto<TEntity> ToPaginated<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize) where TEntity : BaseEntity
        {
            var result = new PaginationDto<TEntity>();

            result.Count = query.Count();

            result.PageNumber = pageNumber;

            result.PageSize = pageSize;

            result.PageCount = result.Count / result.PageSize;

            if (result.Count % result.PageSize > 0) result.PageCount++;

            result.Data = query
                .Skip((result.PageNumber - 1) * result.PageSize)
                .Take(result.PageSize)
                .ToList();

            return result;
        }
    }
}
