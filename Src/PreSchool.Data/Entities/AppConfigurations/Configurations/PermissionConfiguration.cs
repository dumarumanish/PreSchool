using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppConfigurations.Configurations
{
    public class PermissionConfiguration :  IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission","App");

            // Id
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id)
                           .ValueGeneratedNever();
            builder.Property(e => e.Name)
                 .IsRequired();
           

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(124);



        }
    }
}
