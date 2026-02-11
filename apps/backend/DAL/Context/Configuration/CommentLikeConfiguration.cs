using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> builder)
        {
            builder.HasKey(cl => cl.Id);
            
            builder.HasOne(cl => cl.Profile)
                .WithMany(p => p.CommentLikes)
                .HasForeignKey(cl => cl.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship with Comment
            builder.HasOne(cl => cl.Comment)
                .WithMany(c => c.CommentLikes)
                .HasForeignKey(cl => cl.CommentID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}