using DownloadableProduct.DataAccess.Pagination;
using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public List<Purchase> GetAllSuccess(string userId)
        {
            return _context
                .Purchases
                .Include(c => c.Product)
                .Where(c => c.UserId.Equals(userId) && c.IsSuccess)
                .ToList();
        }
        public int CountSuccess(string userId)
        {
            return _context
                .Purchases
                .Include(c => c.Product)
                .Where(c => c.UserId.Equals(userId) && c.IsSuccess)
                .Count();
        }
        public Domain.Dto.Pagination.PaginationDto<Purchase> GetAllForUser(int pageNumber, int pageSize, string userId)
        {
            return _context
                  .Purchases
                  .Include(c => c.Product)
                  .Where(c => c.UserId.Equals(userId))
                  .OrderByDescending(c => c.CreateDate)
               .ToPaginated(pageNumber, pageSize);
        }
    }
}
