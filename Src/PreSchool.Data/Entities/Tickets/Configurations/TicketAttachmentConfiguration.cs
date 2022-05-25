using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Tickets.Configurations
{
    public class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            builder.ToTable("Attachment", "Ticket");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
            builder.ConfigureCommonProperties();
        }
    }
}
