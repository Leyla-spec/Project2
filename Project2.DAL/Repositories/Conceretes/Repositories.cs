using Microsoft.EntityFrameworkCore;
using Project2.Core.Entities;
using Project2.DAL.Contexts;
using Project2.DAL.Repositories.Interfaces;

namespace Project2.DAL.Repositories.Conceretes
{
    public class Repositories<T> : IRepository<T> where T : BaseEntity
    {
        public DbSet<T> Table { get; set; }
        protected readonly MenuAndOrderDbContext _context;
        public Repositories(MenuAndOrderDbContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            Table.Add(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            Table.Remove(await GetByIdAsync(id.Value));
            await SaveChangesAsync();
        }


        public async Task<List<T>> GetAllAsync()
        {
            var entities = await Table.ToListAsync();
            return entities;
        }

        public async Task<T?> GetByIdAsync(int id, bool isTracking = false)
        {
            IQueryable<T> query = Table;

            if (!isTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Table.Update(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
