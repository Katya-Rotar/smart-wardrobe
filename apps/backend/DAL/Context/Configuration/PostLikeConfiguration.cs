using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class PostLikeConfiguration : IEntityTypeConfiguration<PostLike>
    {
        public void Configure(EntityTypeBuilder<PostLike> builder)
        {
            builder.HasKey(pl => pl.Id);

            builder.HasOne(pl => pl.Profile)
                .WithMany(p => p.PostLikes)
                .HasForeignKey(pl => pl.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pl => pl.Publication)
                .WithMany(pub => pub.PostLikes)
                .HasForeignKey(pl => pl.PublicationID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}