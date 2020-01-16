using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Domain.Entities;
using System.Collections.Generic;

namespace DownloadableProduct.Services.Mapping
{
    public static class ProductMapper
    {
        public static Product ToEntity(this ProductCreateDto source)
        {
            return new Product
            {
                Description = source.Description,
                Dimensions = source.Dimensions,
                Price = source.Price,
                Extension = source.Extension,
                Title = source.Title,
                UserId = source.UserId
            };
        }
        public static List<ProductDto> ToDto(this List<Product> sources)
        {
            var result = new List<ProductDto>();
            foreach (var source in sources)
                result.Add(source.ToDto());
            return result;
        }
        public static ProductDto ToDto(this Product source)
        {
            return new ProductDto
            {
                CountView = source.CountView,
                CreateDate = source.CreateDate,
                Description = source.Description,
                Dimensions = source.Dimensions,
                Extension = source.Extension,
                File = source.File,
                Id = source.Id,
                Image = source.Image,
                Price = source.Price,
                Ranking = source.Ranking,
                SmallImage = source.SmallImage,
                Status = source.Status,
                Title = source.Title,
                UserId = source.UserId,
                UserUpoadImage = source.UserUpoadImage
            };
        }
    }
}
