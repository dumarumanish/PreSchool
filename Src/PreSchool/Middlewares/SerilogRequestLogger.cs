using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PreSchool.Application;
using PreSchool.Extensions;
using Microsoft.CodeAnalysis;

namespace PreSchool.Middlewares
{
    public class SerilogRequestLogger
    {
        readonly RequestDelegate _next;
        readonly IDiagnosticContext _diagnosticContext;

        public SerilogRequestLogger(RequestDelegate next, 
            IDiagnosticContext diagnosticContext)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));

            _diagnosticContext = diagnosticContext;
            _next = next;

        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            if (httpContext.User.Identity.IsAuthenticated)
                _diagnosticContext.Set("UserId", httpContext.GetUserId());
            
            return _next.Invoke(httpContext);

        }
    }
    public static class SerilogRequestLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            // Requeest logging
            builder.UseSerilogRequestLogging();

            // Add additional properties
            return builder.UseMiddleware<SerilogRequestLogger>();
        }
    }
}
