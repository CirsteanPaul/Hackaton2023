using HackBackend.Data.Entities;
using HackBackend.Data.Infrastructure.UnitOfWork;
using HackBackend.Data.Repositories;
using HackBackend.Services.Constants;
using HackBackend.Services.Exceptions;
using HackBackend.Services.Mappers;
using HackBackend.Services.Services.Common.Jwt;
using Microsoft.Extensions.Configuration;
using Rent.Core.Modules.Authentication.Jwt;
using System.ComponentModel.DataAnnotations;

namespace HackBackend.Services.Services.Common.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration config;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
        }

        public string LoginUser(string username, string password)
        {
            var user = unitOfWork.Users.GetUserByUsername(username);

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

        public void RegisterUser(RegisterDto dto)
        {
            if(!IsValidEmail(dto.Email))
            {
                throw new BusinessException(ErrorCodes.GenericRegisterError,
                    "Invalid email address");
            }

            var existingUser = unitOfWork.Users.GetUserByUsername(dto.Username);

            if(existingUser is not null)
            {
                throw new BusinessException(ErrorCodes.GenericRegisterError,
                    "Username already in use");
            }

            var userEntity = dto.ToEntity();
            unitOfWork.Users.Add(userEntity);

            unitOfWork.SaveChanges();
        }
        
        private bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private string GenerateToken(User user)
        {
            var jwt = new JwtBuilder(config);
            jwt.AddClaim(JwtClaims.Id, user.Id.ToString());
            return jwt.GetToken();
        }
    }
}
