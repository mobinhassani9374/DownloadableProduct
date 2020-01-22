using DownloadableProduct.Domain.Enums;
using System;

namespace DownloadableProduct.Domain.Entities
{
    public class Checkout : BaseEntity
    {
        public long Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public CheckoutStatus Status { get; set; }
        public string UserId { get; set; }
    }
}
