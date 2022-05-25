using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreImageTypeConfiguration : IEntityTypeConfiguration<StoreImageType>
    {
        public void Configure(EntityTypeBuilder<StoreImageType> builder)
        {
            builder.ToTable("ImageType", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id)
                           .ValueGeneratedNever();

            builder.ConfigureCommonProperties();

        }
    }
}

