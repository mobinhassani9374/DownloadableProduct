using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>
    {
        public PurchaseRepository(AppDbContext context) : base(context)
        {

        }
        public Purchase GetWithDependency(int id)
        {
            return _context.Purchases.Include(c => c.Product).FirstOrDefault(c => c.Id == id);
        }
    }
}
