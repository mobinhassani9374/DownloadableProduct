using Microsoft.AspNetCore.Http;

namespace DownloadableProduct.UI.Models.Product
{
    public class ProductCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; } = 0;
        public string Extension { get; set; }
        public string Dimensions { get; set; }
    }
}
