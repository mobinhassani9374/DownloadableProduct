using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Domain.Dto.Purchase
{
    public class PurchaseDto
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsSuccess { get; set; }
    }
}
