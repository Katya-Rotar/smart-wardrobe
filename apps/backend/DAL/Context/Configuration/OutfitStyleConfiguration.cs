using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class OutfitStyleConfiguration : IEntityTypeConfiguration<OutfitStyle>
    {
        public void Configure(EntityTypeBuilder<OutfitStyle> builder)
        {
            builder.HasKey(os => os.Id);
            
            builder.Property(os => os.OutfitID)
                .IsRequired();

            builder.Property(os => os.StyleID)
                .IsRequired();
            
            builder.HasOne(os => os.Outfit)
                .WithMany(o => o.Styles)
                .HasForeignKey(os => os.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(os => os.Style)
                .WithMany(s => s.Styles)
                .HasForeignKey(os => os.StyleID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}