using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.FeeSettings.Configurations
{
    public class StudentDiscountConfiguration : IEntityTypeConfiguration<StudentDiscount>
    {
        public void Configure(EntityTypeBuilder<StudentDiscount> builder)
        {
            builder.ToTable("StudentDiscount", "FeeSetting");
            builder.ConfigureCommonProperties();



        }
    }
}
