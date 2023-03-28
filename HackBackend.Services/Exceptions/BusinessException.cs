using HackBackend.Services.Constants;
using System;

namespace HackBackend.Services.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException() : base(ErrorCodes.GenericBusinessError) { }

        public BusinessException(string message) : base(ErrorCodes.GenericBusinessError, message) { }

        public BusinessException(string message, Exception innerException) : base(ErrorCodes.GenericBusinessError, message, innerException) { }

        public BusinessException(ErrorCodes code, string message, Exception innerException) : base(code, message, innerException) { }

        public BusinessException(ErrorCodes code, string message) : base(code, message) { }

        public BusinessException(ErrorCodes code) : base(code) { }
    }
}
