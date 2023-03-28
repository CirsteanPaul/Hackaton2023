using HackBackend.Services.Services.Common.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace HackBackend.Services
{
    public static class DependencyMapper
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
