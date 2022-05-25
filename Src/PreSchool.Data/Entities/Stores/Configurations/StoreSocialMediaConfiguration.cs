using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreSocialMediaConfiguration : IEntityTypeConfiguration<StoreSocialMedia>
    {
        public void Configure(EntityTypeBuilder<StoreSocialMedia> builder)
        {
            builder.ToTable("SocialMedia", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

