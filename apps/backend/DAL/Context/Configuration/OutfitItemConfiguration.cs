using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class OutfitItemConfiguration : IEntityTypeConfiguration<OutfitItem>
    {
        public void Configure(EntityTypeBuilder<OutfitItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            
            builder.Property(oi => oi.OutfitID)
                .IsRequired();

            builder.Property(oi => oi.ClothingItemID)
                .IsRequired();
            
            builder.HasOne(oi => oi.Outfit)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(oi => oi.ClothingItem)
                .WithMany(ci => ci.Outfits)
                .HasForeignKey(oi => oi.ClothingItemID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}