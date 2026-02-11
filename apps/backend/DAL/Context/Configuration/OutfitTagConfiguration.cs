using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class OutfitTagConfiguration : IEntityTypeConfiguration<OutfitTag>
    {
        public void Configure(EntityTypeBuilder<OutfitTag> builder)
        {
            builder.HasKey(ot => ot.Id);
            
            builder.HasOne(ot => ot.Outfit)
                .WithMany(o => o.Tags)
                .HasForeignKey(ot => ot.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(ot => ot.Tag)
                .WithMany(t => t.Tags)
                .HasForeignKey(ot => ot.TagID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}