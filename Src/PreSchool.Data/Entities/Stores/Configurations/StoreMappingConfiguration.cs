using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreMappingConfiguration : IEntityTypeConfiguration<StoreMapping>
    {
        public void Configure(EntityTypeBuilder<StoreMapping> builder)
        {
            builder.ToTable("Mapping", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

