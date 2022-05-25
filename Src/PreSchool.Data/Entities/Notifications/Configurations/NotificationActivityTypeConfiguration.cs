
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Notifications
{
    public class NotificationActivityTypeConfiguration : IEntityTypeConfiguration<NotificationActivityType>
    {
        public void Configure(EntityTypeBuilder<NotificationActivityType> builder)
        {
            builder.ToTable("ActivityType", "Notification");

            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id)
                   .ValueGeneratedNever();
        }
    }
}

