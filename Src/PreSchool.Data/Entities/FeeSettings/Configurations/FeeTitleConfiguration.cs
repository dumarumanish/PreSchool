using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class FeeTitleConfiguration : IEntityTypeConfiguration<FeeTitle>
    {
        public void Configure(EntityTypeBuilder<FeeTitle> builder)
        {
            builder.ToTable("FeeTitle", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
