using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class BillPeriodConfiguration : IEntityTypeConfiguration<BillPeriod>
    {
        public void Configure(EntityTypeBuilder<BillPeriod> builder)
        {
            builder.ToTable("BillPeriod", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
