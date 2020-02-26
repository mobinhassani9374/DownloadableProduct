using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.Domain.Dto.Product
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string Extension { get; set; }
        public string Dimensions { get; set; }
    }
}
