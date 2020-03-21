using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Domain.Dto.Product
{
    public class BuyDto
    {
        public string ProductTitle { get; set; }
        public long ProductPrice { get; set; }
        public int CountBuy { get; set; }
        public List<BuyUserDto> Users { get; set; } = new List<BuyUserDto>();
    }
    public class BuyUserDto
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public DateTime BuyDate { get; set; }
    }
}
