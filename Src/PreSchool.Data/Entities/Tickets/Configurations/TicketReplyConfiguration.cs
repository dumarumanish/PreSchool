using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Tickets.Configurations
{
    public class TicketReplyConfiguration : IEntityTypeConfiguration<TicketReply>
    {
        public void Configure(EntityTypeBuilder<TicketReply> builder)
        {
            builder.ToTable("Reply", "Ticket");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
            builder.ConfigureCommonProperties();

        }
    }
}


