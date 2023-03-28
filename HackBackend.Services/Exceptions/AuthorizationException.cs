using HackBackend.Services.Constants;
using System;

namespace HackBackend.Services.Exceptions
{
    public sealed class AuthorizationException : BaseException
    {
        public AuthorizationException() : base(ErrorCodes.GenericAuthorizationError) { }

        public AuthorizationException(string message) : base(ErrorCodes.GenericAuthorizationError, message) { }

        public AuthorizationException(string message, Exception innerException) : base(ErrorCodes.GenericAuthorizationError, message, innerException) { }

        public AuthorizationException(ErrorCodes code, string message, Exception innerException) : base(code, message, innerException) { }

        public AuthorizationException(ErrorCodes code, string message) : base(code, message) { }

        public AuthorizationException(ErrorCodes code) : base(code) { }
    }
}
