using System.ComponentModel.DataAnnotations;

namespace PreSchool.Data.Entities.AppUsers
{
    public enum AppUserTypeEnum
    {
        [Display(Name = "Internal User", Description = "Internal user that manages the application")]
        Internal = 1,

        [Display(Name = "Customer", Description = "Customer user")]
        Customer = 4,

    }
}
