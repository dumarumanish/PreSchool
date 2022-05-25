using PreSchool.Data.Entities.Threads;
using PreSchool.Data.Entities.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Threads.Configurations
{
    public class TicketUserConfiguration : IEntityTypeConfiguration<TicketUser>
    {
        public void Configure(EntityTypeBuilder<TicketUser> builder)
        {
            builder.ToTable("User", "Ticket");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
            builder.ConfigureCommonProperties();

        }
    }
}


