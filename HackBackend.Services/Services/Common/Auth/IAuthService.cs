using HackBackend.Data.Entities;

namespace HackBackend.Services.Services.Common.Auth
{
    public interface IAuthService
    {
        string LoginUser(string username, string password);
    }
}
