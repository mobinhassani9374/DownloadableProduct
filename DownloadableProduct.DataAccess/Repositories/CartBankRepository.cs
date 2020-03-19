﻿using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadableProduct.DataAccess.Repositories
{
    public class CartBankRepository : BaseRepository<CartBank>
    {
        public CartBankRepository(AppDbContext context) : base(context)
        {

        }
        public List<CartBank> GetAll(string userId)
        {
            return _context.CartBanks.Where(c => c.UserId.Equals(userId)).ToList();
        }
    }
}
