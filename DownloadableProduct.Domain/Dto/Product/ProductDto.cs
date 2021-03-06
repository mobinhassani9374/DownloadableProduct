﻿using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Enums;
using System;

namespace DownloadableProduct.Domain.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
        public string SmallImage { get; set; }
        public string File { get; set; }
        public string Extension { get; set; }
        public int CountView { get; set; }
        public int CountBuy { get; set; }
        public DateTime CreateDate { get; set; }
        public int Ranking { get; set; }
        public string Dimensions { get; set; }
        public string UserUpoadImage { get; set; }
        public ProductStatus Status { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public long? FileLength { get; set; }
        public string RejectMessage { get; set; }
    }
}
