using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class OutfitGroupConfiguration : IEntityTypeConfiguration<OutfitGroup>
    {
        public void Configure(EntityTypeBuilder<OutfitGroup> builder)
        {
            builder.HasKey(og => og.Id);
            
            builder.Property(og => og.GroupName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(og => og.Description)
                .HasMaxLength(500);
            
            builder.Property(og => og.CreatedAt)
                .IsRequired();
            
            builder.HasOne(og => og.User)
                .WithMany(u => u.OutfitGroups)
                .HasForeignKey(og => og.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}