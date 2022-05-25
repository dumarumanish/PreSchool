using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Models
{
    public class UsersClaims
    {
        public int UserId { get; }
        public string Username { get; }
        public int UserType { get; }
        public string Permissions { get; }

        public UsersClaims(int userId, string username, int userType, List<int> permissions)
        {
            UserId = userId;
            Username = username;
            if (permissions == null)
                permissions = new List<int>();
            Permissions = string.Join(",", permissions);
            UserType = userType;
        }
    }
}
