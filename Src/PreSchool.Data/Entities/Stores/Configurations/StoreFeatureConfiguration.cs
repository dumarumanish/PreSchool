
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreFeatureConfiguration : IEntityTypeConfiguration<StoreFeature>
    {
        public void Configure(EntityTypeBuilder<StoreFeature> builder)
        {
            builder.ToTable("Feature", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

