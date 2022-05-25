using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Infrastructure.Services.ReCaptcha;

namespace PreSchool
{
    public class VerifyReCaptchaAttribute : ActionFilterAttribute
    {
        public VerifyReCaptchaAttribute()
        {

        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var model = context.ActionArguments.First().Value;
            var propertyInfo = model.GetType().GetProperty("Token");

            if (propertyInfo == null)
                throw new BadRequestException("Invalid", "Captcha is empty");

            var token = propertyInfo.GetValue(model, null) as string;

            if (string.IsNullOrEmpty(token))
                throw new BadRequestException("Invalid", "Captcha is empty");

            var recaptchaService = context.HttpContext.RequestServices.GetRequiredService<IReCaptchaService>();
            if (!await recaptchaService.VerifyReCaptcha(token))
                throw new BadRequestException("Invalid captcha", "Captcha could not be verified");


            await next();
        }
    }

}
