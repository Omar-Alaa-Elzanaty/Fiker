using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Interfaces.Repo
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
        IBaseRepo<T> Repository<T>() where T : class;
    }
}
