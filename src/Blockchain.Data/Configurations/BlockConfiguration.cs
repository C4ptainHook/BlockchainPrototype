using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Blockchain.Data.Configuration;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.ToCollection("Blocks");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Index).HasElementName("index");
        builder.Property(b => b.TimeStamp).HasElementName("timestamp");
        builder.Property(b => b.Proof).HasElementName("proof");
        builder.Property(b => b.PreviousHash).HasElementName("previous_hash");
    }
}
