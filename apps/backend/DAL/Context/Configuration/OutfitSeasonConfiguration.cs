using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class OutfitSeasonConfiguration : IEntityTypeConfiguration<OutfitSeason>
    {
        public void Configure(EntityTypeBuilder<OutfitSeason> builder)
        {
            builder.HasKey(os => os.Id);
            
            builder.Property(os => os.OutfitID)
                .IsRequired();

            builder.Property(os => os.SeasonID)
                .IsRequired();
            
            builder.HasOne(os => os.Outfit)
                .WithMany(o => o.Seasons)
                .HasForeignKey(os => os.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(os => os.Season)
                .WithMany(s => s.OutfitSeasons)
                .HasForeignKey(os => os.SeasonID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}