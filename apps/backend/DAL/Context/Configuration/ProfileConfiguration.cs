using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserID) 
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ProfileImage)
                .HasMaxLength(500);

            builder.Property(p => p.Bio)
                .HasMaxLength(1000);
            
            builder.HasMany(p => p.SavedPosts)
                .WithOne(sp => sp.Profile)
                .HasForeignKey(sp => sp.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Publications)
                .WithOne(pub => pub.Profile)
                .HasForeignKey(pub => pub.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PostLikes)
                .WithOne(pl => pl.Profile)
                .HasForeignKey(pl => pl.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.CommentLikes)
                .WithOne(cl => cl.Profile)
                .HasForeignKey(cl => cl.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Profile)
                .HasForeignKey(c => c.ProfileID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Followers)
                .WithOne(f => f.FollowerProfile)
                .HasForeignKey(f => f.FollowerID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Following)
                .WithOne(f => f.FollowingProfile)
                .HasForeignKey(f => f.FollowingID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
