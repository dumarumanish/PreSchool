using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Students.Configurations
{
    public class BadDeptConfiguration : IEntityTypeConfiguration<BadDept>
    {
        public void Configure(EntityTypeBuilder<BadDept> builder)
        {
            builder.ToTable("BadDept", "Student");
            builder.ConfigureCommonProperties();



        }
    }
}
