using DownloadableProduct.Domain.Dto.Pagination;
using DownloadableProduct.Domain.Dto.Payment;
using DownloadableProduct.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Services.Mapping
{
    public static class PaymentMapper
    {
        public static PaginationDto<PaymentDto> ToDto(this PaginationDto<Payment> source)
        {
            return new PaginationDto<PaymentDto>
            {
                Count = source.Count,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                Data = source.Data.ToDto()
            };
        }
        public static List<PaymentDto> ToDto(this List<Payment> sources)
        {
            var result = new List<PaymentDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static PaymentDto ToDto(this Payment source)
        {
            return new PaymentDto
            {
                CreateDate = source.CreateData,
                IsSuccess = source.IsSuccess,
                Price = source.Price,
                ResponseDate = source.ResponseDate,
                Type = source.Type,
                UserId = source.UserId,
                ValueId = source.ValueId
            };
        }
    }
}
