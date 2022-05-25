using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class AppUserTypeMappingConfiguration : IEntityTypeConfiguration<AppUserTypeMapping>
    {
        public void Configure(EntityTypeBuilder<AppUserTypeMapping> builder)
        {
            builder.ToTable("TypeMapping", "AppUser");

            builder.ConfigureCommonProperties();
        }
    }
}
