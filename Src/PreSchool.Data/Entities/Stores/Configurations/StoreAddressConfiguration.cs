using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreAddressConfiguration : IEntityTypeConfiguration<StoreAddress>
    {
        public void Configure(EntityTypeBuilder<StoreAddress> builder)
        {
            builder.ToTable("Address", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

