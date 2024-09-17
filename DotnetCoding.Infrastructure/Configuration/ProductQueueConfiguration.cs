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
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.State).IsRequired();
            builder.Property(p => p.RequestReason).IsRequired().HasMaxLength(200);
            builder.Property(p => p.RequestedDate).IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Queues);

            builder.HasIndex(p => p.RequestedDate);
            builder.HasIndex(p => p.State);
            builder.HasIndex(p => p.ProductId);
        }
    }
}
