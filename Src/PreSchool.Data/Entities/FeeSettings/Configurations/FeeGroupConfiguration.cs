using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class FeeGroupConfiguration : IEntityTypeConfiguration<FeeGroup>
    {
        public void Configure(EntityTypeBuilder<FeeGroup> builder)
        {
            builder.ToTable("FeeGroup", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
