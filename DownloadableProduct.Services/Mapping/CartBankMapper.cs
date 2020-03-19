using DownloadableProduct.Domain.Dto.CartBank;
using DownloadableProduct.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Services.Mapping
{
    public static class CartBankMapper
    {
        public static List<CartBankDto> ToDto(this List<CartBank> sources)
        {
            var result = new List<CartBankDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static CartBankDto ToDto(this CartBank source)
        {
            return new CartBankDto
            {
                BankName = source.BankName,
                CartNumber = source.CartNumber,
                Id = source.Id,
                Status = source.Status,
            };
        }
    }
}
