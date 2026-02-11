using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.ProfileImage)
            .HasMaxLength(500);

        builder.HasMany(u => u.ClothingItems)
            .WithOne(ci => ci.User)
            .HasForeignKey(ci => ci.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Outfits)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Notifications)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.OutfitGroups)
            .WithOne(og => og.User)
            .HasForeignKey(og => og.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Events)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}