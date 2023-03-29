using HackBackend.Data.Entities;
using HackBackend.Services.Services.Common.Auth;

namespace HackBackend.Services.Mappers
{
    public static class RegisterMapper
    {
        public static User ToEntity(this RegisterDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email
            };
        }
    }
}
