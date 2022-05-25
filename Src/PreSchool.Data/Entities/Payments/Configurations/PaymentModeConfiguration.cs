using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Payments.Configurations
{
    public class CheckoutAttributeConfiguration : IEntityTypeConfiguration<PaymentMode>
    {
        public void Configure(EntityTypeBuilder<PaymentMode> builder)
        {
            builder.ToTable("PaymentMode", "Payment");

       
            builder.ConfigureCommonTypeProperties();



        }
    }
}

