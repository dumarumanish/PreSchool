
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Notifications
{
    public class NotificationGroupConfiguration : IEntityTypeConfiguration<NotificationGroup>
    {
        public void Configure(EntityTypeBuilder<NotificationGroup> builder)
        {
            builder.ToTable("Group", "Notification");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();

        }
    }
}

