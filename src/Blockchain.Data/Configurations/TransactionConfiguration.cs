using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Blockchain.Data.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToCollection("Transactions");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).ValueGeneratedOnAdd();
        builder.Property(t => t.Amount).HasElementName("amount");
        builder.Property(t => t.RecipientId).HasElementName("recipient_id");
        builder.Property(t => t.SenderId).HasElementName("sender_id");
        builder.Property(t => t.TimeStamp).HasElementName("timestamp");
        builder.Property(t => t.BlockId).HasElementName("block_id");

        builder.HasOne(t => t.Recipient).WithMany().HasForeignKey(t => t.RecipientId);

        builder.HasOne(t => t.Sender).WithMany().HasForeignKey(t => t.SenderId);

        builder.HasOne(t => t.Block).WithMany(b => b.Transactions).HasForeignKey(t => t.BlockId);
    }
}
