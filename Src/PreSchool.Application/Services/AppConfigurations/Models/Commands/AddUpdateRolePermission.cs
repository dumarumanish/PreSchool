﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppConfigurations.Models.Commands
{
    public class AddUpdateRolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
