using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Infastructures
{
    public interface ICurrentUserService
    {
        bool IsAuthenticated { get; }
        HttpContext HttpContext { get; }

        int AppUserId { get; }
        AppUserTypeEnum AppUserType { get; }
        int? StoreId { get; }
        int? VendorId { get; }
        int? CustomerId { get; }
        string UserIpAddress { get; }

        List<Permissions> UserPermissions { get; }

        bool HavePermission(params Permissions[] permissions);

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
        bool HaveSuperPermission(Permissions subPermission, Permissions superPermission);

        
    }
}
