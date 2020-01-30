using DownloadableProduct.DataAccess.Pagination;
using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Domain.Enums;
using DownloadableProduct.Identity;
using System.Collections.Generic;
using System.Linq;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }
        public List<Product> GetAll(string userId)
        {
            return _context
                .Products
                .Where(c => c.UserId.Equals(userId))
                .OrderByDescending(c => c.CreateDate)
                .ToList();
        }
        public List<Product> GetAllAvailable()
        {
            return _context
                .Products
                .Where(c => c.Status.Equals(ProductStatus.Confirmed))
                .OrderByDescending(c => c.CreateDate)
                .ToList();
        }
        public List<Product> GetAllWating()
        {
            return _context
                .Products
                .Where(c => c.Status.Equals(ProductStatus.Wating))
                .OrderByDescending(c => c.CreateDate)
                .ToList();
        }
        public Domain.Dto.Pagination.PaginationDto<Product> GetAllConfirmed(int pageNumber, int pageSize)
        {
            return _context
                .Products
                .Where(c => c.Status.Equals(ProductStatus.Confirmed))
                .OrderByDescending(c => c.Id)
                .ToPaginated(pageNumber, pageSize);
        }
    }
}
