using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Students.Configurations
{
    public class AdditionalFeeConfiguration : IEntityTypeConfiguration<AdditionalFee>
    {
        public void Configure(EntityTypeBuilder<AdditionalFee> builder)
        {
            builder.ToTable("AdditionalFee", "Student");
            builder.ConfigureCommonProperties();



        }
    }
}
