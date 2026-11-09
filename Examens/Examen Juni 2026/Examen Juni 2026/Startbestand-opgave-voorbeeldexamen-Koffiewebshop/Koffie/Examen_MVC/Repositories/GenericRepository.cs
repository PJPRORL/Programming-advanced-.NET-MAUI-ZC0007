using Examen_MVC.Data;
using Examen_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace Examen_MVC.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class, IModel
    {
        protected readonly ShopContext _context;

        public GenericRepository(ShopContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync<T>(T id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> ItemExists(int id)
        {
            return await _context.Brewers.AnyAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}