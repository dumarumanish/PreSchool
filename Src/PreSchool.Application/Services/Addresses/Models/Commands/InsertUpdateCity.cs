using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Addresses.Models.Commands
{
   public class InsertUpdateCity
    {
        public int Id { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

    }
}
