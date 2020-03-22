using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadableProduct.UI.Models.Payment
{
    public class PaymentSearchViewModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 12;
    }
}
