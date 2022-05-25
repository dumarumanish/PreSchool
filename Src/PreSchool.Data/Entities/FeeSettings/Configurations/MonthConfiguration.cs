using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class MonthConfiguration : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.ToTable("Month", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
