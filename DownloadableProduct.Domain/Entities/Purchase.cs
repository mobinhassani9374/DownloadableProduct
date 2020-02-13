using System;

namespace DownloadableProduct.Domain.Entities
{
    public class Purchase : BaseEntity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsSuccess { get; set; }
    }
}
