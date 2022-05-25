using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreCollectionCenterConfiguration : IEntityTypeConfiguration<StoreCollectionCenter>
    {
        public void Configure(EntityTypeBuilder<StoreCollectionCenter> builder)
        {
            builder.ToTable("CollectionCenter", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

