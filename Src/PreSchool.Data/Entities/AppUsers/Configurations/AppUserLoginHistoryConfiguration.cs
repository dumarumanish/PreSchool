using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class AppUserLoginHistoryConfiguration :  IEntityTypeConfiguration<AppUserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<AppUserLoginHistory> builder)
        {
            builder.ToTable("LoginHistory","AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

           
        }
    }
}
