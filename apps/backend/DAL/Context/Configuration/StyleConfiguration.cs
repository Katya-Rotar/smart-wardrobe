using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class StyleConfiguration : IEntityTypeConfiguration<Style>
    {
        public void Configure(EntityTypeBuilder<Style> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.StyleName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Styles)
                .WithOne(os => os.Style)
                .HasForeignKey(os => os.StyleID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.ClothingItems)
                .WithOne(cis => cis.Style)
                .HasForeignKey(cis => cis.StyleID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}