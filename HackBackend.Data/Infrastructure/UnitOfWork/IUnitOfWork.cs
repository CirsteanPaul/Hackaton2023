using System.Threading;
using System.Threading.Tasks;
using HackBackend.Data.Repositories;

namespace HackBackend.Data.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }


        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Reload<TEntity>(TEntity entity) where TEntity : class;
        bool IsModified<TEntity>(TEntity entity) where TEntity : class;
    }
}
