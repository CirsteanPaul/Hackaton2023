using HackBackend.Services.Constants;
using HackBackend.Services.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace HackBackend.Web.Exceptions
{
    public class InvalidModelStateException : BaseException
    {
        public ModelStateDictionary ModelState { get; }

        public InvalidModelStateException(ModelStateDictionary modelState) : base(ErrorCodes.GenericInvalidApiModel, GetDefaultMessage(modelState))
        {
            this.ModelState = modelState;
        }

        public InvalidModelStateException(ModelStateDictionary modelState, string message) : base(ErrorCodes.GenericInvalidApiModel, message)
        {
            this.ModelState = modelState;
        }

        public InvalidModelStateException(ModelStateDictionary modelState, ErrorCodes code) : base(code, GetDefaultMessage(modelState))
        {
            this.ModelState = modelState;
        }

        public InvalidModelStateException(ModelStateDictionary modelState, ErrorCodes code, string message) : base(code, message)
        {
            this.ModelState = modelState;
        }

        private static string GetDefaultMessage(ModelStateDictionary modelState)
        {
            var errorMessageCollection = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            var errorMessages = string.Join(" ", errorMessageCollection);

            return $"Invalid Model State. Errors: {{ {errorMessages} }}";
        }
    }
}
