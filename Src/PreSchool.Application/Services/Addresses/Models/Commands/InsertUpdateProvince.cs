using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Commands
{
    public class InsertUpdateProvince
    {
        public int Id { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; }

    
        public string Abbreviation { get; set; }

      
        public bool Published { get; set; }

      
        public int DisplayOrder { get; set; }
    }
}
