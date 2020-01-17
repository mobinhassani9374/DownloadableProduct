using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Services.Mapping
{
    public static class ParchaseMapper
    {
        public static Purchase ToEntity(this ParchaseRequestDto source)
        {
            return new Purchase
            {
                ProductId = source.ProductId,
                UserId = source.UserId
            };
        }
    }
}
