using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class AppUserTypeConfiguration : IEntityTypeConfiguration<AppUserType>
    {
        public void Configure(EntityTypeBuilder<AppUserType> builder)
        {
            builder.ToTable("Type", "AppUser");

          

            builder.ConfigureCommonTypeProperties();
        }
    }
}
