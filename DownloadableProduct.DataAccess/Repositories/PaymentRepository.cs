using DownloadableProduct.DataAccess.Pagination;
using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {

        }
        public Domain.Dto.Pagination.PaginationDto<Payment> GetAllForUser(int pageNumber, int pageSize, string userId)
        {
            return _context
                  .Payments
                  .Where(c => c.UserId.Equals(userId))
                  .OrderByDescending(c => c.CreateData)
               .ToPaginated(pageNumber, pageSize);
        }
    }
}
