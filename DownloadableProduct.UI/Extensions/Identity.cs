using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace DownloadableProduct.UI.Extensions
{
    public static class Identity
    {
        public static string GetFullName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        }
    }
}
