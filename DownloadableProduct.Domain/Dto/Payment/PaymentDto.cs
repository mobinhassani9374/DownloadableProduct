using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Domain.Dto.Payment
{
    public class PaymentDto
    {
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public DateTime? ResponseDate { get; set; }
        public long Price { get; set; }
        public PaymentType Type { get; set; }
        public int ValueId { get; set; }
        public bool IsSuccess { get; set; }
        public ProductDto Product { get; set; }
    }
}
