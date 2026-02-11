using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class TypeCategoryConfiguration : IEntityTypeConfiguration<TypeCategory>
    {
        public void Configure(EntityTypeBuilder<TypeCategory> builder)
        {
            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.CategoryID)
                .IsRequired();

            builder.Property(tc => tc.TypeID)
                .IsRequired();
            
            builder.HasOne(tc => tc.Category)
                .WithMany(c => c.TypeCategories)
                .HasForeignKey(tc => tc.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(tc => tc.Type)
                .WithMany(t => t.TypeCategories)
                .HasForeignKey(tc => tc.TypeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}