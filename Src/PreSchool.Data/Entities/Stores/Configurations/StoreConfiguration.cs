using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

