using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadableProduct.UI.Models.Purchase
{
    public class PurchaseSearchViewModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 12;
    }
}
