using HackBackend.Services.Constants;
using System;

namespace HackBackend.Services.Exceptions
{
    public class LoginException : BaseException
    {
        public LoginException(string message) : base(ErrorCodes.GenericAuthenticationError, message)
        {
        }
    }
}
