using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
    {
        public void Configure(EntityTypeBuilder<Publication> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.ImageURL)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.CommentingOptions)
                .IsRequired();
            
            builder.HasOne(p => p.Profile)
                .WithMany(pr => pr.Publications)
                .HasForeignKey(p => p.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(p => p.Outfit)
                .WithMany(o => o.Publications)
                .HasForeignKey(p => p.OutfitID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}