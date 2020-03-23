
using System;

namespace DownloadableProduct.Domain.Entities
{
    public class LogService : BaseEntity
    {
        public string Method { get; set; }

        public long? ContentLengthRequest { get; set; }

        public long? ContentLengthResponse { get; set; }

        public string RelativePath { get; set; }

        public string UserId { get; set; }

        public long? ElabsedTime { get; set; }

        public int ResponseStatusCode { get; set; }

        public DateTime CreateDate { get; set; }

        public TimeSpan Elabsed { get; set; }

        public string IpAddress { get; set; }
    }
}
