using DownloadableProduct.Domain.Entities;
using System.Collections.Generic;

namespace DownloadableProduct.Domain.Dto.Pagination
{
    public class PaginationDto<T> : PaginationDto
    {
        public List<T> Data { get; set; }
    }
    public class PaginationDto
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
