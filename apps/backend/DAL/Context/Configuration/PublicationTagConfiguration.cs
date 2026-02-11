using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class PublicationTagConfiguration : IEntityTypeConfiguration<PublicationTag>
    {
        public void Configure(EntityTypeBuilder<PublicationTag> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.HasOne(pt => pt.Publication)
                .WithMany(p => p.PublicationTags)
                .HasForeignKey(pt => pt.PublicationID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.Tag)
                .WithMany(t => t.PublicationTags)
                .HasForeignKey(pt => pt.TagID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}