
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Files
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("File", "File");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

        }
    }
}

