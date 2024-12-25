using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Interfaces.Repo
{
    public interface IBaseRepo<T> where T : class
    {
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void UpdateRange(ICollection<T> entities);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Entities { get; }
    }
}
