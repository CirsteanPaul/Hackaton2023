using HackBackend.Data.Entities;
using HackBackend.Data.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace HackBackend.Data.Infrastructure.Context
{
    public interface IAppDbContext : IEntityFrameworkContext
    {
        DbSet<User> Users { get; set; }
    }
}
