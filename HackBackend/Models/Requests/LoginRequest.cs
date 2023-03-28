using System.ComponentModel.DataAnnotations;

namespace HackBackend.Web.Models.Requests
{
    public sealed class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
