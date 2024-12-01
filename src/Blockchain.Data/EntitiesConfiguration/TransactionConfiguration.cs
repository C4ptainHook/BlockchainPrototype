using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blockchain.Data.EntitiesConfiguration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Amount).IsRequired();
        builder.HasOne(t => t.Recepient).WithMany().IsRequired();
        builder.HasOne(t => t.Sender).WithMany().IsRequired();
        builder.HasOne(t => t.Block).WithMany(b => b!.Transactions).IsRequired(false);
    }
}
