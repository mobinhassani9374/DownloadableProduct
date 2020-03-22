using DownloadableProduct.Domain.Dto.Pagination;
using DownloadableProduct.Domain.Dto.Purchase;
using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Services.Mapping
{
    public static class ParchaseMapper
    {
        public static Purchase ToEntity(this ParchaseRequestDto source)
        {
            return new Purchase
            {
                ProductId = source.ProductId,
                UserId = source.UserId
            };
        }
        public static List<PurchaseDto> ToDto(this List<Purchase> sources)
        {
            var result = new List<PurchaseDto>();

            foreach (var source in sources)
                result.Add(source.ToDto());

            return result;
        }
        public static PurchaseDto ToDto(this Purchase source)
        {
            return new PurchaseDto
            {
                CreateDate = source.CreateDate,
                IsSuccess = source.IsSuccess,
                ProductId = source.ProductId,
                UserId = source.UserId,
                Product = source.Product.ToDto()
            };
        }
        public static PaginationDto<PurchaseDto> ToDto(this PaginationDto<Purchase> sources)
        {
            return new PaginationDto<PurchaseDto>
            {
                Count = sources.Count,
                PageCount = sources.PageCount,
                PageNumber = sources.PageNumber,
                PageSize = sources.PageSize,
                Data = sources.Data.ToDto()
            };
        }
    }
}
