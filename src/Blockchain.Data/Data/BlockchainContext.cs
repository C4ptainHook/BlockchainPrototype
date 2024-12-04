using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data;

public class BlockchainContext : DbContext
{
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    public BlockchainContext(DbContextOptions<BlockchainContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlockchainContext).Assembly);
    }
}
