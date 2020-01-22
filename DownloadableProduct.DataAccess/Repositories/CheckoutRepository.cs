using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class CheckoutRepository : BaseRepository<Checkout>
    {
        public CheckoutRepository(AppDbContext context) : base(context)
        {

        }
    }
}
