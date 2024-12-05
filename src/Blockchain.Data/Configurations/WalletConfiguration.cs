using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Blockchain.Data.Configuration;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToCollection("Wallets");
        builder.HasKey(w => w.Id);

        builder.Property(w => w.NickName).HasElementName("nick_name");
        builder.Property(w => w.Amount).HasElementName("amount");
    }
}
