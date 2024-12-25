using Fiker.Application.Interfaces.Repo;
using Fiker.Presistance.Context;
using System.Collections;

namespace Fiker.Presistance.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SquadDb _context;
        private Hashtable _repositories;

        public UnitOfWork(
            SquadDb context)
        {
            _context = context;
            _repositories = [];
        }

        public IBaseRepo<T> Repository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepo<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepo<T>)_repositories[type]!;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}