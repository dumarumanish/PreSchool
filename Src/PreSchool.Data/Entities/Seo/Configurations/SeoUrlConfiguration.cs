
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PreSchool.Data.Entities.Seo.Configurations
{
    public class SeoUrlConfiguration : IEntityTypeConfiguration<SeoUrl>
    {
        public void Configure(EntityTypeBuilder<SeoUrl> builder)
        {
            builder.ToTable("Url", "Seo");
            builder.ConfigureCommonProperties();



        }
    }
}
