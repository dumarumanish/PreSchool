using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Common.Addresses.Configurations
{

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City", "Address");
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(80);

            builder.ConfigureCommonProperties();
        }
    }

   
}
