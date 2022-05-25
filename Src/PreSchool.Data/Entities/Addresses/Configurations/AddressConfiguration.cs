using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Common.Addresses.Configurations
{

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
        {
            public void Configure(EntityTypeBuilder<Address> builder)
            {
                builder.ToTable("Address", "Address");

            }
        
    }
}
