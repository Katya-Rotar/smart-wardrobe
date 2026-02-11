using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class OutfitGroupItemConfiguration : IEntityTypeConfiguration<OutfitGroupItem>
    {
        public void Configure(EntityTypeBuilder<OutfitGroupItem> builder)
        {
            builder.HasKey(ogi => ogi.Id);
            
            builder.HasOne(ogi => ogi.OutfitGroup)
                .WithMany(og => og.OutfitGroups)
                .HasForeignKey(ogi => ogi.OutfitGroupID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(ogi => ogi.Outfit)
                .WithMany(o => o.GroupItems)
                .HasForeignKey(ogi => ogi.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}