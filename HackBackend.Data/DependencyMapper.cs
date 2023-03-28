using HackBackend.Data.Infrastructure.Context;
using HackBackend.Data.Infrastructure.UnitOfWork;
using HackBackend.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackBackend.Data
{
    public static class DependencyMapper
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = GetConnectionString(configuration);

            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("HackatonDatabase");
        }
    }
}
