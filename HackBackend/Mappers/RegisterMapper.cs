using HackBackend.Services.Services.Common.Auth;
using HackBackend.Web.Models.Authentication;

namespace HackBackend.Web.Mappers
{
    public static class RegisterMapper
    {
        public static RegisterDto ToDto(this RegisterRequest model)
        {
            if(model is null)
            {
                return null;
            }

            return new RegisterDto
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
            };
        }
    }
}
