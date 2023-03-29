using HackBackend.Data.Entities;
using HackBackend.Data.Infrastructure.Repository;

namespace HackBackend.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUsername(string username);
    }
}
