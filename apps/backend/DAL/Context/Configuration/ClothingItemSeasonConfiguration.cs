using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class ClothingItemSeasonConfiguration : IEntityTypeConfiguration<ClothingItemSeason>
    {
        public void Configure(EntityTypeBuilder<ClothingItemSeason> builder)
        {
            builder.HasKey(cis => cis.Id);

            builder.Property(cis => cis.ClothingItemID)
                .IsRequired();

            builder.Property(cis => cis.SeasonID)
                .IsRequired();

            builder.HasOne(cis => cis.ClothingItem)
                .WithMany(ci => ci.Seasons)
                .HasForeignKey(cis => cis.ClothingItemID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cis => cis.Season)
                .WithMany(s => s.ClothingItemSeasons)
                .HasForeignKey(cis => cis.SeasonID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}