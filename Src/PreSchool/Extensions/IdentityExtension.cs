using PreSchool.Application.Exceptions;
using PreSchool.Application.Models;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PreSchool.Extensions
{
    public static class IdentityExtension
    {
        public static List<Permissions> GetUserPermissions(this HttpContext httpContext)
        {
            var claim = ((ClaimsIdentity)httpContext.User.Identity).FindFirst(CustomClaimType.Permissions);
            if (claim == null || claim.Value == null)
                return new List<Permissions>();
            var permissionIds = claim.Value.Split(",");

            var permissions = new List<Permissions>();

            foreach (var id in permissionIds)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(id))
                        permissions.Add((Permissions)Convert.ToInt32(id));
                }
                catch (Exception )
                {
                    
                }
            }

            return permissions;
        }

      
        public static int GetUserId(this HttpContext httpContext)
        {
            var claim = ((ClaimsIdentity)httpContext.User.Identity).FindFirst(CustomClaimType.AppUserId);
            if (claim == null || claim.Value == null)
                throw new UnAuthorizedException("Invalid Token");

            try
            {
                return int.Parse(claim.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int GetStoreId(this HttpContext httpContext)
        {
            var claim = ((ClaimsIdentity)httpContext.User.Identity).FindFirst(CustomClaimType.StoreId);
            if (claim == null || claim.Value == null)
                // Default store Id
                return 1;
            try
            {
                return int.Parse(claim.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string GetUserIpAddress(this HttpContext httpContext)
        {
            //first try to get IP address from the forwarded header
            if (httpContext.Request.Headers != null)
            {
                //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
                //connecting to a web server through an HTTP proxy or load balancer

                var forwardedHeader = httpContext.Request.Headers["X-Forwarded-For"];
                if (!StringValues.IsNullOrEmpty(forwardedHeader))
                    return forwardedHeader.FirstOrDefault();
            }

            //if this header not exists try get connection remote IP address
            if (httpContext.Connection.RemoteIpAddress != null)
               return httpContext.Connection.RemoteIpAddress.ToString();

            return null;
        }

        public static bool HavePermission(this HttpContext httpContext, params Permissions[] permissions)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            // Check if there is any required permission
            if (permissions == null || permissions.Count() == 0)
                return false;

            // Get user permission
            var userPermissions = httpContext.GetUserPermissions();

            //Check if there is any permission
            if (userPermissions == null || userPermissions.Count == 0)
            {
                return false;
            }

            // Check if user is super admin
            if (userPermissions.Contains(Permissions.TotalAccess))
                return true;

            // Check for required permission
            foreach (var permission in permissions)
            {
                if (userPermissions.Contains(permission))
                {
                    return true;
                }
            }
            return false;
        }

        
        /// <summary>
        /// Check if the current user contain super permission of perticular permission or not
        /// Eg: Let SuperPermission be ViewAllUsersDetail
        ///     and SubPermission be ViewMyUserDetail
        ///     then user can view all the user detail if user contain ViewAllUserDetail permission 
        ///     but can only view current user detail if user contain ViewMyUserDetail
        /// </summary>
        /// <param name="httpContext">Current context</param>
        /// <param name="subPermission">sub permission of super permission</param>
        /// <param name="superPermission"> super permission of sub permission</param>
        /// <returns> True if user contain super permission, false if not else throw forbidden exception </returns>
        public static bool HaveSuperPermission(this HttpContext httpContext, Permissions subPermission, Permissions superPermission)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                throw new ForbiddenException();

            // Get user permissions
            var userPermissions = httpContext.GetUserPermissions();

            //Check if there is any permission
            if (userPermissions == null || userPermissions.Count == 0)
                throw new ForbiddenException();

            // Check if user is super admin
            if (userPermissions.Contains(Permissions.TotalAccess))
                return true;

            // Check if the user contain super permission of myPermission
            if (userPermissions.Contains(superPermission))
                return true;


            // check if the user contain sub permission
            if (userPermissions.Contains(subPermission))
                return false;

            // Must contain atleast superPermission or subPermission
            throw new ForbiddenException();
        }


    }
}
