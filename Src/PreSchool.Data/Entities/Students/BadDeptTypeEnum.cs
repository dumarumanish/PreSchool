using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public enum BadDeptTypeEnum
    {

        [Display(Name = "Incurred", Description = "Incurred")]
        Incurred,

        [Display(Name = "Recovered", Description = "Recovered")]
        Recovered,

    }
}
