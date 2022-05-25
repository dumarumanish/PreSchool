using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.AppConfigurations.Models.Dtos;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PreSchool.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor
          )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity.IsAuthenticated ?? false;

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        public int AppUserId
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(CustomClaimType.AppUserId);
                if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                    throw new UnAuthorizedException("User is not authenticated");

                try
                {
                    return int.Parse(claim.Value);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }


        public int? StoreId
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(CustomClaimType.StoreId);
                if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                    // Default store Id
                    return null;
                try
                {
                    if (string.IsNullOrWhiteSpace(claim.Value))
                        return null;
                    return int.Parse(claim.Value);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public int? CustomerId
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(CustomClaimType.CustomerId);
                if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                    return null;
                try
                {
                  
                    return int.Parse(claim.Value);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public int? VendorId
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(CustomClaimType.VendorId);
                if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
                    return null;
                try
                {
                    return int.Parse(claim.Value);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public AppUserTypeEnum AppUserType
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(CustomClaimType.AppUserType);
                if (claim == null || claim.Value == null)
                    throw new UnAuthorizedException("User is not authenticated");
                try
                {
                    return (AppUserTypeEnum)Convert.ToInt32(claim.Value);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string UserIpAddress
        {
            get
            {
                //first try to get IP address from the forwarded header
                if (_httpContextAccessor.HttpContext.Request.Headers != null)
                {
                    //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
                    //connecting to a web server through an HTTP proxy or load balancer

                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                        return forwardedHeader.FirstOrDefault();
                }

                //if this header not exists try get connection remote IP address
                if (_httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                    return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                return null;
            }
        }



        public List<Permissions> UserPermissions
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(CustomClaimType.Permissions);
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
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return permissions;
            }
        }

        public bool HavePermission(params Permissions[] permissions)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            // Check if there is any required permission
            if (permissions == null || permissions.Count() == 0)
                return false;

            //  user permission
            var userPermissions = UserPermissions;

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
        /// <param name="_httpContextAccessor.HttpContext">Current context</param>
        /// <param name="subPermission">sub permission of super permission</param>
        /// <param name="superPermission"> super permission of sub permission</param>
        /// <returns> True if user contain super permission, false if not else throw forbidden exception </returns>
        public bool HaveSuperPermission(Permissions subPermission, Permissions superPermission)
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                throw new ForbiddenException();

            //  user permissions
            var userPermissions = UserPermissions;

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
