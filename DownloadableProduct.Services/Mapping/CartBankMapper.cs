using DownloadableProduct.Domain.Dto.CartBank;
using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                RejectMessage = source.RejectMessage
            };
        }
        public static List<CartBankDto> ToDto(this List<CartBank> sources, List<UserDto> users)
        {
            var result = new List<CartBankDto>();
            foreach (var source in sources)
                result.Add(source.ToDto(users));
            return result;
        }
        public static CartBankDto ToDto(this CartBank source, List<UserDto> users)
        {
            return new CartBankDto
            {
                BankName = source.BankName,
                CartNumber = source.CartNumber,
                Id = source.Id,
                Status = source.Status,
                User = users.FirstOrDefault(c => c.Id == source.UserId)
            };
        }
    }
}
