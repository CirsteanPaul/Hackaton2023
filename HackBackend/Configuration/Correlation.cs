using CorrelationId.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HackBackend.Web.Configuration
{
    public static class Correlation
    {
        public const string Header = "X-Correlation-Id";
        public const string LoggingScopeKey = "CorrelationId";

        /// <summary>
        /// Add a correlation id on the HTTP context and the logging scope.
        /// The correlation Id is accessible from the ICorrelationContextAccessor.
        /// The "LoggingScopeKey" is the name for the key used when adding the correlation id to the logger scope
        /// </summary>
        /// <link>https://github.com/stevejgordon/CorrelationId/wiki</link>
        public static IServiceCollection AddCorrelationId(this IServiceCollection services)
        {
            return services.AddDefaultCorrelationId(options =>
            {
                options.CorrelationIdGenerator = () => Guid.NewGuid().ToString();
                options.AddToLoggingScope = true;
                options.LoggingScopeKey = LoggingScopeKey;
                options.RequestHeader = Header;
                options.ResponseHeader = Header;
                options.IncludeInResponse = true;
            });
        }
    }
}
