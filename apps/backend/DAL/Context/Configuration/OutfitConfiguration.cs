using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class OutfitConfiguration : IEntityTypeConfiguration<Outfit>
    {
        public void Configure(EntityTypeBuilder<Outfit> builder)
        {
            builder.HasKey(o => o.Id);
            
            builder.Property(o => o.UserID)
                .IsRequired();

            builder.Property(o => o.TemperatureSuitabilityID)
                .IsRequired();
            
            builder.HasOne(o => o.User)
                .WithMany(u => u.Outfits)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.TemperatureSuitability)
                .WithMany(ts => ts.Outfits)
                .HasForeignKey(o => o.TemperatureSuitabilityID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(o => o.Publications)
                .WithOne(p => p.Outfit)
                .HasForeignKey(p => p.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Tags)
                .WithOne(ot => ot.Outfit)
                .HasForeignKey(ot => ot.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Styles)
                .WithOne(os => os.Outfit)
                .HasForeignKey(os => os.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Seasons)
                .WithOne(os => os.Outfit)
                .HasForeignKey(os => os.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Outfit)
                .HasForeignKey(oi => oi.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.GroupItems)
                .WithOne(ogi => ogi.Outfit)
                .HasForeignKey(ogi => ogi.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Events)
                .WithOne(e => e.Outfit)
                .HasForeignKey(e => e.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
