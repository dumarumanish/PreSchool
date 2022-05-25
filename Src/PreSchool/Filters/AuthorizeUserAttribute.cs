using PreSchool.Data.Enumerations;
using PreSchool.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace PreSchool
{
    public class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
    {
        private Permissions[] RequiredPermissions { get; set; }

        public AuthorizeUserAttribute()
        {

        }
        public AuthorizeUserAttribute(params Permissions[] permissions)
        {
            RequiredPermissions = permissions;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Check if there is any required permission
            if (RequiredPermissions == null || RequiredPermissions.Count() == 0)
                return;

            // Get user permission
            var userPermissions = context.HttpContext.GetUserPermissions();

            //Check if there is any permission
            if (userPermissions == null || userPermissions.Count == 0)
            {
                context.Result = new ForbidResult();
                return;
            }

            // Check if user is super admin
            if (userPermissions.Contains(Permissions.TotalAccess))
                return;

            // Check for required permission
            foreach (var permission in RequiredPermissions)
            {
                if (userPermissions.Contains(permission))
                {
                    return;
                }
            }
            context.Result = new ForbidResult();


        }
    }
}
