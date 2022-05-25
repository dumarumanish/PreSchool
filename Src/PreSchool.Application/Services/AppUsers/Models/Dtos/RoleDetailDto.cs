using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Dtos
{
    public class RoleDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public LimitedToStoresDto LimitedToStores { get; set; }
    }
}
