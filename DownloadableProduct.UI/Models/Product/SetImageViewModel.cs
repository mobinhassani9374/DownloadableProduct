using Microsoft.AspNetCore.Http;

namespace DownloadableProduct.UI.Models.Product
{
    public class SetImageViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
    }
}
