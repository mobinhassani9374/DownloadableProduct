using DownloadableProduct.Domain.Dto.Pagination;
using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Domain.Dto.User;
using System.Collections.Generic;
using System.Linq;

namespace DownloadableProduct.Services.Mapping
{
    public static class UserMapper
    {
        public static PaginationDto<ProductDto> SetUser(this PaginationDto<ProductDto> sources, List<UserDto> users)
        {
            sources.Data.ForEach(c =>
            {
                c.User = users.FirstOrDefault(i => i.Id == c.UserId);
            });

            return sources;
        }
    }
}
