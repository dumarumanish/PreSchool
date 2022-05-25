using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations.AppUsers
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role", "AppUser");

            // Id
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Name)
                 .IsRequired()
                 .HasMaxLength(64);


            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(124);

            builder.ConfigureCommonProperties();
        }
    }
}
