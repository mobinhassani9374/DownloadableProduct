namespace DownloadableProduct.Domain.Dto.Checkout
{
    public class CheckoutRequestDto
    {
        public string UserId { get; set; }
        public long Price { get; set; }
        public int CartNumber { get; set; }
    }
}
