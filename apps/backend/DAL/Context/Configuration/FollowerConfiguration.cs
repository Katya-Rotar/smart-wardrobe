using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(f => f.Id);
            
            builder.HasOne(f => f.FollowerProfile)
                .WithMany(p => p.Followers)
                .HasForeignKey(f => f.FollowerID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.FollowingProfile)
                .WithMany(p => p.Following)
                .HasForeignKey(f => f.FollowingID)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(og => og.CreatedAt)
                .IsRequired();
        }
    }
}