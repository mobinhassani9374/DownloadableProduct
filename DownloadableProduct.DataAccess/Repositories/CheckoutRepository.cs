using DownloadableProduct.DataAccess.Pagination;
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
        public int CountWatingForUser(string userId)
        {
            return _context.Checkouts.Where(c => c.Status == CheckoutStatus.Wating).Count();
        }
        public Domain.Dto.Pagination.PaginationDto<Checkout> GetAllForUser(int pageNumber, int pageSize, string userId)
        {
            return _context
                  .Checkouts
                  .Where(c => c.UserId.Equals(userId))
                  .OrderByDescending(c => c.CreateDate)
               .ToPaginated(pageNumber, pageSize);
        }
    }
}
