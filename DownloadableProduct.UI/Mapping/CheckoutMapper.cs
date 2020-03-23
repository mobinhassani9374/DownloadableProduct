using DownloadableProduct.Domain.Dto.Checkout;
using DownloadableProduct.UI.Models.Checkout;

namespace DownloadableProduct.UI.Mapping
{
    public static class CheckoutMapper
    {
        public static CheckoutRequestDto ToDto(this CheckoutCreateViewModel source, string userId)
        {
            return new CheckoutRequestDto
            {
                Price = source.Price,
                UserId = userId,
                CartNumber = source.CartNumber
            };
        }
    }
}
