using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>
    {
        public PurchaseRepository(AppDbContext context) : base(context)
        {

        }
    }
}
