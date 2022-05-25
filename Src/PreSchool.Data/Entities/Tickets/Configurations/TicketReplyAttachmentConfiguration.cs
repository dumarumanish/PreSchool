using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Tickets.Configurations
{
    public class TicketReplyAttachmentConfiguration : IEntityTypeConfiguration<TicketReplyAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketReplyAttachment> builder)
        {
            builder.ToTable("ReplyAttachment", "Ticket");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
            builder.ConfigureCommonProperties();

        }
    }
}


