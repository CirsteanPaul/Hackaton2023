namespace HackBackend.Web.Models.Authentication
{
    public sealed class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
