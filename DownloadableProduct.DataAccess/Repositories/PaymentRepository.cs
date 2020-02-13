using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {

        }
    }
}
