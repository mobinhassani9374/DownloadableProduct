using DNTPersianUtils.Core;
namespace DownloadableProduct.Utillity
{
    public static class Validation
    {
        public static bool IsPhoneNumber(string phoneNumber)
        {
            return phoneNumber.IsValidIranianMobileNumber();
        }
    }
}
