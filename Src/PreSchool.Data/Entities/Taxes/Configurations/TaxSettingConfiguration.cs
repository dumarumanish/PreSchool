using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Taxes.Configurations
{
    public class TaxSettingConfiguration : IEntityTypeConfiguration<TaxSetting>
    {
        public void Configure(EntityTypeBuilder<TaxSetting> builder)
        {
            builder.ToTable("Setting", "Tax");
            builder.ConfigureCommonProperties();



        }
    }
}
