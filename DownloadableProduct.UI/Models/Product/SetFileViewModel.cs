using Microsoft.AspNetCore.Http;

namespace DownloadableProduct.UI.Models.Product
{
    public class SetFileViewModel
    {
        public int Id { get; set; }
        public IFormFile File { get; set; }
    }
}
