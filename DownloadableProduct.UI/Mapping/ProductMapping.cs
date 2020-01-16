using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.UI.Models.Product;

namespace DownloadableProduct.UI.Mapping
{
    public static class ProductMapping
    {
        public static ProductCreateDto ToDto(this ProductCreateViewModel source, string userId)
        {
            return new ProductCreateDto
            {
                Description = source.Description,
                Dimensions = source.Dimensions,
                Extension = source.Extension,
                Price = source.Price,
                Title = source.Title,
                UserId = userId
            };
        }
    }
}
