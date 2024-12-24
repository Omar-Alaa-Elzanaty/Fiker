using Microsoft.EntityFrameworkCore;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Presistance.Context;

namespace SquadAsService.Presistance.Repo
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly SquadDb _context;

        public BaseRepo(SquadDb context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public  void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void UpdateRange(ICollection<T> entities)
        {
            _context.UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void DeleteRange(ICollection<T> entities)
        {
            _context.RemoveRange(entities);
        }
    }
}
