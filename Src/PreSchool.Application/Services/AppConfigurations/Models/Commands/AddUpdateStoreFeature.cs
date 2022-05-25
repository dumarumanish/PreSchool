using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppUsers.Models.Commands
{
    public class AddUpdateStoreFeature
    {
        public int StoreId { get; set; }
        public int AppFeatureId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
