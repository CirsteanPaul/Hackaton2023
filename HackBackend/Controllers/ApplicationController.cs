using HackBackend.Web.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Modules.Authentication.Jwt;
using System;
using System.Linq;

namespace HackBackend.Web.Controllers
{
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        protected int? UserId
        {
            get
            {
                if (HttpContext is null || HttpContext.User is null)
                {
                    return null;
                }

                var currentUser = HttpContext.User;

                if (!currentUser.HasClaim(c => c.Type == JwtClaims.Id))
                {
                    return null;
                }

                var contextUserId = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == JwtClaims.Id).Value;
                var isParsed = int.TryParse(contextUserId, out int userId);

                return isParsed ? userId : null;
            }
        }

        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelStateException(ModelState);
            }
        }

        protected void ValidateUserId()
        {
            if (UserId is null)
            {
                throw new ArgumentNullException(nameof(UserId), "User Id not found");
            }
        }
    }
}
