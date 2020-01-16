using Microsoft.AspNetCore.Identity;

namespace DownloadableProduct.Identity.DataModel
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
