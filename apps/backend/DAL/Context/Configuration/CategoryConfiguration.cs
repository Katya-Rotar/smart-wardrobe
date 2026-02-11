using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasMany(c => c.TypeCategories)
                .WithOne(tc => tc.Category)
                .HasForeignKey(tc => tc.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(c => c.ClothingItems)
                .WithOne(ci => ci.Category)
                .HasForeignKey(ci => ci.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}