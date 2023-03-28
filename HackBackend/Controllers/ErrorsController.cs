using HackBackend.Services.Constants;
using HackBackend.Services.Exceptions;
using HackBackend.Web.Exceptions;
using HackBackend.Web.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace HackBackend.Web.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public const string ErrorHandlingRoute = @"/errors";
        private const string DefaultErrorDetail = "Internal Server Error";

        private readonly ProblemDetails defaultResponse;
        private readonly ILogger<ErrorsController> logger;

        public ErrorsController(ILogger<ErrorsController> logger)
        {
            this.logger = logger;
            this.defaultResponse = InitializeDefaultResponse();
        }

        /// All exceptions get redirected to this Action by the .NET Exception Handler
        /// All error respones adhere to the RFC 7807 standard (Type: ProblemDetails)

        [Route(ErrorHandlingRoute)]
        public ActionResult<ProblemDetails> HandleError()
        {
            try
            {
                var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                logger.LogError(context.Error, context.Error.Message);
                return HandleErrorByType(context);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, defaultResponse);
            }
        }

        private ActionResult<ProblemDetails> HandleErrorByType(IExceptionHandlerPathFeature context)
        {
            if(context.Error is LoginException)
            {
                var err = (LoginException)context.Error;
                var res = new ProblemDetails()
                {
                    Detail = err.Message,
                    Instance = context.Path
                };
                res.AddErrorCode(err.Code);
                return BadRequest(res);
            }
            if (context.Error is BusinessException)
            {
                var err = (BusinessException)context.Error;
                var res = new ProblemDetails()
                {
                    Detail = err.Message,
                    Instance = context.Path
                };
                res.AddErrorCode(err.Code);
                return BadRequest(res); // 400
            }

            if (context.Error is AuthorizationException)
            {
                var err = (AuthorizationException)context.Error;
                var res = new ProblemDetails()
                {
                    Detail = err.Message,
                    Instance = context.Path
                };
                res.AddErrorCode(err.Code);
                return StatusCode(StatusCodes.Status403Forbidden, res); // 403
            }

            if (context.Error is EntityNotFoundException)
            {
                var err = (EntityNotFoundException)context.Error;
                var res = new ProblemDetails()
                {
                    Detail = err.Message,
                    Instance = context.Path
                };
                res.AddErrorCode(err.Code);
                res.AddEntityId(err.EntityId);
                return NotFound(res); // 404
            }

            if (context.Error is InvalidModelException)
            {
                var err = (InvalidModelException)context.Error;
                var res = new ValidationProblemDetails(err.Errors);
                res.Detail = err.Message;
                res.Instance = context.Path;
                res.AddErrorCode(err.Code);
                return UnprocessableEntity(res); // 422
            }

            if (context.Error is InvalidModelStateException)
            {
                var err = (InvalidModelStateException)context.Error;
                var res = new ValidationProblemDetails(err.ModelState);
                res.Instance = context.Path;
                res.AddErrorCode(err.Code);
                return UnprocessableEntity(res); // 422
            }

            if (context.Error is BaseException)
            {
                var err = (BaseException)context.Error;
                var res = new ProblemDetails()
                {
                    Detail = err.Message,
                    Instance = context.Path
                };
                res.AddErrorCode(err.Code);
                return StatusCode(StatusCodes.Status500InternalServerError, res); // 500
            }

            if (context.Error is Exception)
            {
                var res = new ProblemDetails()
                {
                    Detail = DefaultErrorDetail,
                    Instance = context.Path,
                    Status = StatusCodes.Status500InternalServerError
                };
                res.AddErrorCode(ErrorCodes.GenericError);
                return StatusCode(StatusCodes.Status500InternalServerError, res); // 500
            }

            return StatusCode(StatusCodes.Status500InternalServerError, defaultResponse);
        }

        private ProblemDetails InitializeDefaultResponse()
        {
            var res = new ProblemDetails()
            {
                Detail = DefaultErrorDetail,
                Status = StatusCodes.Status500InternalServerError,
            };
            res.AddErrorCode(ErrorCodes.GenericError);
            return res;
        }
    }
}
