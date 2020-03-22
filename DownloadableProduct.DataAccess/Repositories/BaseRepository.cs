using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadableProduct.DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Insert(TEntity entity)
        {
            _context.Add(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
        public int InsertWithSave(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public virtual TEntity Get(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(c => c.Id == id);
        }
        public virtual List<TEntity> Get(List<int> ids)
        {
            return _context.Set<TEntity>().Where(c => ids.Any(i => i == c.Id)).ToList();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
