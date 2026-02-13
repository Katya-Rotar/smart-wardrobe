using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Publication)
                .WithMany(pub => pub.Comments)
                .HasForeignKey(c => c.PublicationID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(og => og.CreatedAt)
                .IsRequired();
        }
    }
}