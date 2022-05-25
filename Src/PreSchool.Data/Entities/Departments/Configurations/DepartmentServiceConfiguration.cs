using PreSchool.Data.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Departments
{
    public class DepartmentServiceConfiguration : IEntityTypeConfiguration<DepartmentService>
    {
        public void Configure(EntityTypeBuilder<DepartmentService> builder)
        {
            builder.ToTable("DepartmentService", "AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();

        }
    }
}
