using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Students.Configurations
{
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.ToTable("Fee", "Student");
            builder.ConfigureCommonProperties();



        }
    }
}
