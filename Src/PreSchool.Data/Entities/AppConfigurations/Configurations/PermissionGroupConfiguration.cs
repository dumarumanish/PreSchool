using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppConfigurations.Configurations
{
    public class PermissionGroupConfiguration :  IEntityTypeConfiguration<PermissionGroup>
    {
        public void Configure(EntityTypeBuilder<PermissionGroup> builder)
        {
            builder.ToTable("PermissionGroup","App");

            // Id
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                           .ValueGeneratedNever();


            builder.Property(e => e.GroupName)
                 .IsRequired();
            

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(124);



        }
    }
}
