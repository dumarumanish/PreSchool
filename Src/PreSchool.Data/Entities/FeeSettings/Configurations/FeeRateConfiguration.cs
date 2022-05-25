using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class FeeRateConfiguration : IEntityTypeConfiguration<FeeRate>
    {
        public void Configure(EntityTypeBuilder<FeeRate> builder)
        {
            builder.ToTable("FeeRate", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
