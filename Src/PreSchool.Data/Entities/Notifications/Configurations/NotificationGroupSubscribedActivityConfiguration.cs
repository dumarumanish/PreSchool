
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Notifications
{
    public class NotificationGroupSubscribedActivityConfiguration : IEntityTypeConfiguration<NotificationGroupSubscribedActivity>
    {
        public void Configure(EntityTypeBuilder<NotificationGroupSubscribedActivity> builder)
        {
            builder.ToTable("GroupSubscribedActivity", "Notification");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();

        }
    }
}

