using HackBackend.Services.Constants;
using System;

namespace HackBackend.Services.Exceptions
{
    public abstract class BaseException : Exception
    {
        public ErrorCodes Code { get; protected set; }

        public BaseException() : base()
        {
            InitializeDefaultErrorCode();
        }

        public BaseException(string message) : base(message)
        {
            InitializeDefaultErrorCode();
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
            InitializeDefaultErrorCode();
        }

        public BaseException(ErrorCodes code) : base()
        {
            Code = code;
        }

        public BaseException(ErrorCodes code, string message) : base(message)
        {
            Code = code;
        }

        public BaseException(ErrorCodes code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        private void InitializeDefaultErrorCode()
        {
            Code = ErrorCodes.GenericError;
        }
    }
}
