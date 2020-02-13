using System;

namespace DownloadableProduct.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public DateTime CreateData { get; set; }
        public string UserId { get; set; }
        public DateTime? ResponseDate { get; set; }
        public long Price { get; set; }
        public int Type { get; set; }
        public int ValueId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
