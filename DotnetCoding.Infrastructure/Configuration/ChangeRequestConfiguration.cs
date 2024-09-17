using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetCoding.Infrastructure.Configuration;

public class ChangeRequestConfiguration : IEntityTypeConfiguration<ChangeRequest>
{
    public void Configure(EntityTypeBuilder<ChangeRequest> builder)
    {
        builder.ToTable(nameof(ChangeRequest));
        builder.HasKey(h => h.Id);
        builder.Property(p => p.ProductQueueId).IsRequired();
        builder.Property(p => p.PropertyName).IsRequired();
        builder.Property(p => p.CurrentValue).IsRequired();
        builder.Property(p => p.NewValue).IsRequired();

        builder.HasOne(x => x.ProductQueue)
            .WithOne(x => x.ChangeRequest);

    }
}
