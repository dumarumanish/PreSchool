
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Notifications
{
    public class NotificationGroupSubscriberConfiguration : IEntityTypeConfiguration<NotificationGroupSubscriber>
    {
        public void Configure(EntityTypeBuilder<NotificationGroupSubscriber> builder)
        {
            builder.ToTable("GroupSubscriber", "Notification");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();

        }
    }
}

