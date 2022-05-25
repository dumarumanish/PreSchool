using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Data.Entities.Students
{
    public enum AdmissionTypeEnum
    {

        [Display(Name = "Playgroup", Description = "Playgroup")]
        Toddler,

        [Display(Name = "Nursery", Description = "Nursery")]
        EarlyYears,

        [Display(Name = "Jr. Kg.", Description = "Jr. Kg.")]
        Reception,


        [Display(Name = "Sr. Kg.", Description = "Sr. Kg.")]
        Year1,

        [Display(Name = "Day Care", Description = "Day Care")]
        DayCare,

        [Display(Name = "After School Hours", Description = "After School Hours")]
        AfterSchoolHours,

        [Display(Name = "Others", Description = "Others")]
        Others,

    }
}
