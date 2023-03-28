using HackBackend.Data.Entities;

namespace HackBackend.Data.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(string username);
    }
}
