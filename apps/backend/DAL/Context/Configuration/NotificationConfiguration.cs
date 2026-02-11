using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);
            
            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(n => n.Event)
                .WithMany(e => e.Notifications)
                .HasForeignKey(n => n.EventID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}