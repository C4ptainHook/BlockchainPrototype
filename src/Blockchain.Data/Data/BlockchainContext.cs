using Blockchain.Data.Configuration;
using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Blockchain.Data;

public class BlockchainContext : DbContext
{
    public required DbSet<Block> Blocks { get; set; }
    public required DbSet<Transaction> Transactions { get; set; }
    public required DbSet<Wallet> Wallets { get; set; }

    public BlockchainContext(DbContextOptions<BlockchainContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlockchainContext).Assembly);
    }
}
