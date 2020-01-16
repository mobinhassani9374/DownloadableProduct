using DownloadableProduct.Identity.DataModel;
using DownloadableProduct.UI.Models.Account;

namespace DownloadableProduct.UI.Mapping
{
    public static class UserMapping
    {
        public static User ToUser(this RegisterViewModel source)
        {
            return new User
            {
                FullName = source.FullName,
                UserName = source.PhoneNumber,
                PhoneNumber = source.PhoneNumber
            };
        }
    }
}
