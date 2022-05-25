using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Common.Addresses.Configurations
{

    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Region", "Address");
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(80);
            builder.ConfigureCommonProperties();

        }
    }

   
}
