
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Emails
{
    public class EmailHistoryConfiguration : IEntityTypeConfiguration<EmailHistory>
    {
        public void Configure(EntityTypeBuilder<EmailHistory> builder)
        {
            builder.ToTable("History", "Email");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

        }
    }
}

