using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class InternalUserExperienceConfiguration :  IEntityTypeConfiguration<InternalUserExperience>
    {
        public void Configure(EntityTypeBuilder<InternalUserExperience> builder)
        {
            builder.ToTable("InternalUserExperience", "AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();
            


        }
    }
}
