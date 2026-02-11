using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SeasonName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasMany(s => s.OutfitSeasons)
                .WithOne(os => os.Season)
                .HasForeignKey(os => os.SeasonID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(s => s.ClothingItemSeasons)
                .WithOne(cis => cis.Season)
                .HasForeignKey(cis => cis.SeasonID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}