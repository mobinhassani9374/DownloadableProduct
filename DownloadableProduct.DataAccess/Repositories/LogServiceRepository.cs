using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class LogServiceRepository : BaseRepository<LogService>
    {
        public LogServiceRepository(AppDbContext context) : base(context)
        {

        }
    }
}
