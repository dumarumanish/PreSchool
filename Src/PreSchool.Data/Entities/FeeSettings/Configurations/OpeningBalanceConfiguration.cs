using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class OpeningBalanceConfiguration : IEntityTypeConfiguration<OpeningBalance>
    {
        public void Configure(EntityTypeBuilder<OpeningBalance> builder)
        {
            builder.ToTable("OpeningBalance", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
