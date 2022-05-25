using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class AppUserConfiguration :  IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser","AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(128);

            //builder.Property(e => e.Password)
            //    .IsRequired()
            //    .HasMaxLength(128);
         

            //builder.Property(e => e.RoleId)
            //    .IsRequired();

            //builder.HasOne(r => r.Role)
            //    .WithMany(a => a.AppUsers)
            //    .HasForeignKey(f => f.RoleId)
            //    .HasConstraintName("Fk_AppUser_Role");

            builder.ConfigureCommonProperties();
            


        }
    }
}
