using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Schools.Configurations
{
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.ToTable("Batch", "School");
            builder.ConfigureCommonProperties();



        }
    }
}
