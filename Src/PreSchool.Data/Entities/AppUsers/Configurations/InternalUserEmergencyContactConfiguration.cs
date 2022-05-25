using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class InternalUserEmergencyContactConfiguration :  IEntityTypeConfiguration<InternalUserEmergencyContact>
    {
        public void Configure(EntityTypeBuilder<InternalUserEmergencyContact> builder)
        {
            builder.ToTable("InternalUserEmergencyContact", "AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();
            


        }
    }
}
