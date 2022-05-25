using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Stores.Configurations
{
    public class StoreUserConfiguration : IEntityTypeConfiguration<StoreUser>
    {
        public void Configure(EntityTypeBuilder<StoreUser> builder)
        {
            builder.ToTable("User", "Store");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);


            builder.ConfigureCommonProperties();

        }
    }
}

