using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class StudentFeeConfiguration : IEntityTypeConfiguration<StudentFee>
    {
        public void Configure(EntityTypeBuilder<StudentFee> builder)
        {
            builder.ToTable("StudentFee", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
