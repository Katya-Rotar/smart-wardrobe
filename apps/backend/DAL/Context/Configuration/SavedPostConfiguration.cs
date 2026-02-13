using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class SavedPostConfiguration : IEntityTypeConfiguration<SavedPost>
    {
        public void Configure(EntityTypeBuilder<SavedPost> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.HasOne(sp => sp.User)
                .WithMany(p => p.SavedPosts)
                .HasForeignKey(sp => sp.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sp => sp.Publication)
                .WithMany(pub => pub.SavedPosts)
                .HasForeignKey(sp => sp.PublicationID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}