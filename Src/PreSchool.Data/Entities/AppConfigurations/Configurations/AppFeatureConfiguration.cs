using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppConfigurations.Configurations
{
    public class AppFeatureConfiguration :  IEntityTypeConfiguration<AppFeature>
    {
        public void Configure(EntityTypeBuilder<AppFeature> builder)
        {
            builder.ToTable("Feature","App");

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
