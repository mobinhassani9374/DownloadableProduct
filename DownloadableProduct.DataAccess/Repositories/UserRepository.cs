using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Identity;
using DownloadableProduct.Identity.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<UserDto> Get(List<string> ids)
        {
            return _context
                .Users
                .Where(c => ids.Any(i => i == c.Id))
                .Select(c => new UserDto
                {
                    FullName = c.FullName,
                    Id = c.Id,
                    Wallet = c.Wallet
                })
                .ToList();
        }
        public User GetEntity(string id)
        {
            return _context
                .Users
                .Where(c => c.Id == id)
                .FirstOrDefault();
        }
        public void Update(User user)
        {
            _context.Update(user);
        }
    }
}
