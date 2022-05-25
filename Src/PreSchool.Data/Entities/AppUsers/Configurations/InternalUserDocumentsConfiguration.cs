using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class InternalUserDocumentsConfiguration :  IEntityTypeConfiguration<InternalUserDocuments>
    {
        public void Configure(EntityTypeBuilder<InternalUserDocuments> builder)
        {
            builder.ToTable("InternalUserDocuments", "AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();
            


        }
    }
}
