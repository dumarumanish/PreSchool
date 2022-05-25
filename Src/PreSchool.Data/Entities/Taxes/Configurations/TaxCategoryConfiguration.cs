using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Taxes.Configurations
{
    public class TaxCategoryConfiguration : IEntityTypeConfiguration<TaxCategory>
    {
        public void Configure(EntityTypeBuilder<TaxCategory> builder)
        {
            builder.ToTable("Category", "Tax");
            builder.ConfigureCommonProperties();



        }
    }
}
