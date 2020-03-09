using DownloadableProduct.Domain.Dto.Checkout;
using DownloadableProduct.Domain.Dto.Pagination;
using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DownloadableProduct.Services.Mapping
{
    public static class CheckoutMapper
    {
        public static Checkout ToEntity(this CheckoutRequestDto source)
        {
            return new Checkout
            {
                Price = source.Price,
                UserId = source.UserId
            };
        }
        public static List<CheckoutDto> ToDto(this List<Checkout> sources, List<UserDto> users)
        {
            var result = new List<CheckoutDto>();
            foreach (var source in sources)
                result.Add(source.ToDto(users));
            return result;
        }
        public static CheckoutDto ToDto(this Checkout source, List<UserDto> users)
        {
            return new CheckoutDto
            {
                CreateDate = source.CreateDate,
                Id = source.Id,
                Price = source.Price,
                ResponseDate = source.ResponseDate,
                Status = source.Status,
                User = users.FirstOrDefault(c => c.Id == source.UserId)
            };
        }
        public static PaginationDto<CheckoutDto> ToDto(this PaginationDto<Checkout> source)
        {
            var result = new PaginationDto<CheckoutDto>();

            result.Count = source.Count;
            result.PageCount = source.PageCount;
            result.PageNumber = source.PageNumber;
            result.PageSize = source.PageSize;
            result.Data = source.Data.ToDto(new List<UserDto>());

            return result;
        }
    }
}
