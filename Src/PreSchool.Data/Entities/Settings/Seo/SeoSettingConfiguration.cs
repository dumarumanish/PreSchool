using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PreSchool.Data.Entities.Seo.Configurations
{
    public class SeoSettingConfiguration : IEntityTypeConfiguration<SeoSetting>
    {
        public void Configure(EntityTypeBuilder<SeoSetting> builder)
        {
            builder.ToTable("Seo", "Setting");
            builder.ConfigureCommonProperties();


        }
    }
}
