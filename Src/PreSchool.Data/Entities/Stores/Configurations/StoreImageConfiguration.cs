using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreImageConfiguration : IEntityTypeConfiguration<StoreImage>
    {
        public void Configure(EntityTypeBuilder<StoreImage> builder)
        {
            builder.ToTable("Image", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

