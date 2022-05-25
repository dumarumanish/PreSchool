using System.ComponentModel.DataAnnotations;

namespace PreSchool.Data.Entities.AppUsers
{
    public enum EmploymentTypeEnum
    {
        [Display(Name = "Internal User Permanent", Description = "Internal user that are Permanent")]
        Permanent = 1,

        [Display(Name = "Internal User Temporary", Description = "Internal user that are Temporary")]
        Temporary = 4,

        [Display(Name = "Internal User Intern", Description = "Internal user that are Intern")]
        Intern = 6,

    }
}
