using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.DressCode)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(e => e.Outfit)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OutfitID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}