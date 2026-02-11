using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class ClothingItemStyleConfiguration : IEntityTypeConfiguration<ClothingItemStyle>
    {
        public void Configure(EntityTypeBuilder<ClothingItemStyle> builder)
        {
            builder.HasKey(cis => cis.Id);
            
            builder.Property(cis => cis.ClothingItemID)
                .IsRequired();

            builder.Property(cis => cis.StyleID)
                .IsRequired();
            
            builder.HasOne(cis => cis.ClothingItem)
                .WithMany(ci => ci.Styles)
                .HasForeignKey(cis => cis.ClothingItemID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cis => cis.Style)
                .WithMany(s => s.ClothingItems)
                .HasForeignKey(cis => cis.StyleID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}