using System.ComponentModel.DataAnnotations;

namespace PreSchool.Data.Enumerations
{
    public enum AppFeaturesEnum
    {
        #region Store Management

        [Display(Name = "Add Multiple stores", GroupName = nameof(AppFeaturesGroupEnum.StoreManagement), Description = "Add Multiple stores")]
        MultipleStores = 1,

        [Display(Name = "Change store theme", GroupName = nameof(AppFeaturesGroupEnum.StoreManagement), Description = "Change store themes")]
        ChangeStoreThemes = 1,
        #endregion

    }
   
}
