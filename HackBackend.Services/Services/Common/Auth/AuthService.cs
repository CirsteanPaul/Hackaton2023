using HackBackend.Data.Entities;
using HackBackend.Data.Repositories;
using HackBackend.Services.Constants;
using HackBackend.Services.Exceptions;
using HackBackend.Services.Services.Common.Jwt;
using Microsoft.Extensions.Configuration;
using Rent.Core.Modules.Authentication.Jwt;

namespace HackBackend.Services.Services.Common.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration config;
        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            this.config = config;
        }

        public string LoginUser(string username, string password)
        {
            var user = userRepository.GetUserById(username);

            if (user == null)
            {
                throw new LoginException("Invalid username!");
            }

            if (user.Password != password)
            {
                throw new LoginException("Invalid password!");
            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var jwt = new JwtBuilder(config);
            jwt.AddClaim(JwtClaims.Id, user.Id.ToString());
            return jwt.GetToken();
        }
    }
}
