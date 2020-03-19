using DownloadableProduct.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Domain.Entities
{
    public class CartBank
    {
        public int Id { get; set; }
        public string CartNumber { get; set; }
        public string BankName { get; set; }
        public string UserId { get; set; }
        public CartBankStatus Status { get; set; }
        public string RejectMessage { get; set; }
    }
}
