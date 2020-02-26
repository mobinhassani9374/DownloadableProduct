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

        public static ProductUpdateViewModel ToViewModel(this ProductDto source)
        {
            return new ProductUpdateViewModel
            {
                Description = source.Description,
                Dimensions = source.Dimensions,
                Extension = source.Extension,
                Id = source.Id,
                Price = source.Price,
                Title = source.Title
            };
        }
        public static ProductUpdateDto ToDto(this ProductUpdateViewModel source)
        {
            return new ProductUpdateDto
            {
                Title = source.Title,
                Description = source.Description,
                Price = source.Price,
                Id = source.Id,
                Extension = source.Extension,
                Dimensions = source.Dimensions
            };
        }
    }
}
