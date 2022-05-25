using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Students.Configurations
{
    public class StudentRegistrationConfiguration : IEntityTypeConfiguration<StudentRegistration>
    {
        public void Configure(EntityTypeBuilder<StudentRegistration> builder)
        {
            builder.ToTable("Registration", "Student");
            builder.ConfigureCommonProperties();



        }
    }
}
