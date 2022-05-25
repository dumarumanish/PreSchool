using System.Collections.Generic;

namespace PreSchool.Application.Services.AppConfigurations.Models.Dtos
{
    public class AppFeatureByGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<AppFeatureDto> AppFeatures { get; set; }
    }
}
