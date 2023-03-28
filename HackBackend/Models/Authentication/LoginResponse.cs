namespace Rent.WebApi.Models.Authentication
{
    public class LoginResponse
    {
        public LoginResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
