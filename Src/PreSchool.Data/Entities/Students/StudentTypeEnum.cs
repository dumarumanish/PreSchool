using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public enum StudentTypeEnum
    {

        [Display(Name = "Day Scholar", Description = "Day Scholar")]
        DayScholar,

        [Display(Name = "Child Care", Description = "Child Care")]
        ChildCare,

        [Display(Name = "After School", Description = "After School")]
        AfterSchool,

    }
}
