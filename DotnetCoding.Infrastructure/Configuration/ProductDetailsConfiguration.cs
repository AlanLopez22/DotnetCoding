using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetCoding.Infrastructure.Configuration
{
    public class ProductDetailsConfiguration : IEntityTypeConfiguration<ProductDetails>
    {
        public void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            builder.ToTable(nameof(ProductDetails));
            builder.HasKey(h => h.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18, 2)");
            builder.Property(p => p.Status).IsRequired().HasMaxLength(50);
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.PostedDate).IsRequired();

            builder.HasIndex(p => new { p.IsActive, p.PostedDate });
            builder.HasIndex(p => p.Name);
            builder.HasIndex(p => p.PostedDate);
            builder.HasIndex(p => p.Price);
            builder.HasIndex(p => p.Status);
        }
    }
}
