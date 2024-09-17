using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetCoding.Infrastructure.Configuration
{
    public class ProductQueueConfiguration : IEntityTypeConfiguration<ProductQueue>
    {
        public void Configure(EntityTypeBuilder<ProductQueue> builder)
        {
            builder.ToTable(nameof(ProductQueue));
            builder.HasKey(h => h.Id);
            builder.Property(p => p.RequestReason).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Status).IsRequired().HasMaxLength(50);
            builder.Property(p => p.RequestDate).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Queues);

            builder.HasIndex(p => p.RequestDate);
            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => p.ProductId);
        }
    }
}
