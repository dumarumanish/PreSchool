using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations.AppUsers
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission", "AppUser");

            // Id
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.PermissionId)
                 .IsRequired();

            builder.Property(e => e.RoleId)
                .IsRequired();

            builder.ConfigureCommonProperties();



            builder.HasOne(d => d.Role)
               .WithMany(p => p.RolePermissions)
               .HasForeignKey(d => d.RoleId)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_RolePermission_Role");



            builder.HasOne(d => d.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RolePermission_Permission");




        }
    }
}
