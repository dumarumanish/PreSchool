using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public enum GenderEnum
    {

        [Display(Name = "Male", Description = "Male")]
        Male,

        [Display(Name = "Female", Description = "Female")]
        Female,

        [Display(Name = "Others", Description = "Others")]
        Others,

    }
}
