using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class ClothingItemConfiguration : IEntityTypeConfiguration<ClothingItem>
    {
        public void Configure(EntityTypeBuilder<ClothingItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ci => ci.Color)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ci => ci.ImageURL)
                .HasMaxLength(500);

            builder.Property(ci => ci.LastWornDate)
                .IsRequired(false);

            builder.Property(ci => ci.UserID).IsRequired();
            builder.Property(ci => ci.CategoryID).IsRequired();
            builder.Property(ci => ci.TypeID).IsRequired();
            builder.Property(ci => ci.TemperatureSuitabilityID).IsRequired();
            
            builder.HasOne(ci => ci.User)
                .WithMany(u => u.ClothingItems)
                .HasForeignKey(ci => ci.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(ci => ci.Category)
                .WithMany(c => c.ClothingItems)
                .HasForeignKey(ci => ci.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(ci => ci.Type)
                .WithMany(t => t.ClothingItems)
                .HasForeignKey(ci => ci.TypeID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(ci => ci.TemperatureSuitability)
                .WithMany(ts => ts.ClothingItems)
                .HasForeignKey(ci => ci.TemperatureSuitabilityID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(ci => ci.Outfits)
                .WithOne(oi => oi.ClothingItem)
                .HasForeignKey(oi => oi.ClothingItemID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(ci => ci.Styles)
                .WithOne(cis => cis.ClothingItem)
                .HasForeignKey(cis => cis.ClothingItemID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(ci => ci.Seasons)
                .WithOne(cis => cis.ClothingItem)
                .HasForeignKey(cis => cis.ClothingItemID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
