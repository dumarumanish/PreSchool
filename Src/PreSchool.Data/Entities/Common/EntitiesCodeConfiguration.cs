using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities
{

    public class EntitiesCodeConfiguration : IEntityTypeConfiguration<EntitiesCode>
    {
        public void Configure(EntityTypeBuilder<EntitiesCode> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.EntityName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Prefix)
                .HasMaxLength(20);

            builder.Property(p => p.Suffix)
                .HasMaxLength(20);

            builder.Property(p => p.NumericLength)
                .IsRequired();
        }
    }

}
