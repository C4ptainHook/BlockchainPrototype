using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blockchain.Data.Configuration;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.TimeStamp).IsRequired();
        builder.Property(b => b.Proof).IsRequired();
        builder.Property(b => b.PreviousHash).IsRequired(false);
        builder.HasMany(b => b.Transactions).WithOne(t => t.Block).IsRequired(false);
    }
}
