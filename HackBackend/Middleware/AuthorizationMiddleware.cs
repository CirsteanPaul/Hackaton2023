using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace HackBackend.Web.Middleware
{
    public static class AuthorizationMiddleware
    {
        public static void AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            byte[] securityKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                        ValidAudience = configuration["JwtSettings:Audience"],
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        RequireExpirationTime = true,
                        RequireAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = true
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (!context.Request.Headers.ContainsKey("Authorization"))
                            {
                                context.Token = context.Request.Cookies["token"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
