using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class InternalUserAcademicConfiguration :  IEntityTypeConfiguration<InternalUserAcademic>
    {
        public void Configure(EntityTypeBuilder<InternalUserAcademic> builder)
        {
            builder.ToTable("InternalUserAcademic", "AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();
            


        }
    }
}
