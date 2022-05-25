using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class AppUserForgotPasswordConfiguration :  IEntityTypeConfiguration<AppUserOtpCode>
    {
        public void Configure(EntityTypeBuilder<AppUserOtpCode> builder)
        {
            builder.ToTable("OtpCode","AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

           
        }
    }
}
