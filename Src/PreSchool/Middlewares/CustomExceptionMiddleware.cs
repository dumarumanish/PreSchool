using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Models;
using System.Security.Claims;
using PreSchool.Application.Events;
using PreSchool.Application;

namespace PreSchool.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var customException = exception as BaseException;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Server error";
            var description = $"{exception.Message} InnerException: {exception.InnerException} Contact administrator.";

            // Server error
            if (customException == null)
            {
                var sender = new EventSender()
                {
                    EventDateTime = DateTime.Now
                };
                if (context.User.Identity.IsAuthenticated)
                {
                    if (int.TryParse(((ClaimsIdentity)context.User.Identity).FindFirst(CustomClaimType.AppUserId)?.Value, out int appuserId))
                        sender.AppUserId = appuserId;

                }
                Logger.Error(exception, sender, null);
            }

            if (null != customException)
            {
                message = customException.Message;
                description = customException.Description;
                statusCode = customException.Code;
            }

          
            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            return  response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
            {
                message = message,
                description = description
            }));
        }
    }
}