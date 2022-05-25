using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.AppConfigurations.Models.Dtos
{
    public class AppFeatureDto
    {
        public int Id { get; set; }
        public int AppFeatureGroupId { get; set; }
        public string AppFeatureName { get; set; }
        public string GroupName { get; set; }
        public string  Description { get; set; }
    }
}
