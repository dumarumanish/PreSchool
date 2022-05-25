using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Students.Configurations
{
    public class FeePaymentConfiguration : IEntityTypeConfiguration<FeePayment>
    {
        public void Configure(EntityTypeBuilder<FeePayment> builder)
        {
            builder.ToTable("FeePayment", "Student");
            builder.ConfigureCommonProperties();



        }
    }
}
