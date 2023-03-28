using HackBackend.Data.Entities;
using HackBackend.Data.Infrastructure.Context;
using HackBackend.Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HackBackend.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IAppDbContext dbContext;

        public UserRepository(IAppDbContext dbContext) : base((DbContext)dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetUserById(string username)
        {
            return dbContext.Users
                .FirstOrDefault(u => u.Username == username);
        }
    }
}
