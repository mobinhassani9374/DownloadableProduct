using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadableProduct.UI.Models.Checkout
{
    public class CheckoutSearchViewModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 12;
    }
}
