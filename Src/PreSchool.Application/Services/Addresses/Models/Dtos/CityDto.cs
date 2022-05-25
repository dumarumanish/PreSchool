using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }

        public int DisplayOrder { get; set; }
    }
}
