using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Type = DAL.Entities.Type;

namespace DAL.Context.Configuration
{
    public class TypeConfiguration : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.TypeName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasMany(t => t.TypeCategories)
                .WithOne(tc => tc.Type)
                .HasForeignKey(tc => tc.TypeID)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(t => t.ClothingItems)
                .WithOne(ci => ci.Type)
                .HasForeignKey(ci => ci.TypeID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}