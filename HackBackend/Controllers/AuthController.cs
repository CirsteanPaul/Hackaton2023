using HackBackend.Services.Services.Common.Auth;
using HackBackend.Web.Models.Common;
using HackBackend.Web.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rent.WebApi.Models.Authentication;

namespace HackBackend.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ResponseModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public ActionResult<LoginResponse> LoginUser([FromBody] LoginRequest login)
        {
            var token = authenticationService.LoginUser(login.Username, login.Password);
            return Ok(new LoginResponse(token));
        }
    }
}
