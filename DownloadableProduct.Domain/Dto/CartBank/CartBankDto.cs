using DownloadableProduct.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Domain.Dto.CartBank
{
    public class CartBankDto
    {
        public int Id { get; set; }
        public string CartNumber { get; set; }
        public UserDto User { get; set; }
        public string BankName { get; set; }
        public Enums.CartBankStatus Status { get; set; }
    }
}
