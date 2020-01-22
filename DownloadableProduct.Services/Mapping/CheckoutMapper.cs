using DownloadableProduct.Domain.Dto.Checkout;
using DownloadableProduct.Domain.Entities;

namespace DownloadableProduct.Services.Mapping
{
    public static class CheckoutMapper
    {
        public static Checkout ToEntity(this CheckoutRequestDto source)
        {
            return new Checkout
            {
                Price = source.Price,
                UserId = source.UserId
            };
        }
    }
}
