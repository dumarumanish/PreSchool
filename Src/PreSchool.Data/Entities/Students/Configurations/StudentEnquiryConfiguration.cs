using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Students.Configurations
{
    public class StudentEnquiryConfiguration : IEntityTypeConfiguration<StudentEnquiry>
    {
        public void Configure(EntityTypeBuilder<StudentEnquiry> builder)
        {
            builder.ToTable("Enquiry", "Student");
            builder.ConfigureCommonProperties();



        }
    }
}
