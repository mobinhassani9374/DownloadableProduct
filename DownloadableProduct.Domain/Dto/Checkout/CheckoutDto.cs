using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Domain.Dto.Checkout
{
    public class CheckoutDto
    {
        public int Id { get; set; }
        public long Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public CheckoutStatus Status { get; set; }
        public UserDto User { get; set; }
    }
}
