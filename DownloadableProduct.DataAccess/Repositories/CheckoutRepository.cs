using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Domain.Enums;
using DownloadableProduct.Identity;
using System.Collections.Generic;
using System.Linq;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class CheckoutRepository : BaseRepository<Checkout>
    {
        public CheckoutRepository(AppDbContext context) : base(context)
        {

        }
        public List<Checkout> GetAllWatingCheckout()
        {
            return _context
                .Checkouts
                .Where(c => c.Status == CheckoutStatus.Wating)
                .ToList();
        }
    }
}
