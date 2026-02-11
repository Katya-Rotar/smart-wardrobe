using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Context.Configuration
{
    public class TemperatureSuitabilityConfiguration : IEntityTypeConfiguration<TemperatureSuitability>
    {
        public void Configure(EntityTypeBuilder<TemperatureSuitability> builder)
        {
            builder.HasKey(ts => ts.Id);
            
            builder.Property(ts => ts.TemperatureSuitabilityName)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.HasMany(ts => ts.Outfits)
                .WithOne(o => o.TemperatureSuitability)
                .HasForeignKey(o => o.TemperatureSuitabilityID)
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.HasMany(ts => ts.ClothingItems)
                .WithOne(ci => ci.TemperatureSuitability)
                .HasForeignKey(ci => ci.TemperatureSuitabilityID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}