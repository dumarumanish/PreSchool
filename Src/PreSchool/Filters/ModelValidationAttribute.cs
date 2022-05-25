using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Serilog;
using System.Linq;
using System.Net;
using PreSchool.Application.Models;

namespace PreSchool.Filters
{
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState); // returns 400 with error

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errors = context.ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage);
                Log.Error("Validation Error", errors);

                var errorResponse = new ErrorResponse
                {
                    message = "Validation Error",
                    description = JsonConvert.SerializeObject(errors)
                };
                context.Result = new JsonResult(errorResponse);
            }
        }
    }
}
