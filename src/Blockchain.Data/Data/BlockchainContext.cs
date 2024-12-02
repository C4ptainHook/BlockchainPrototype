using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data;

public class BlockchainContext : DbContext
{
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    public BlockchainContext(DbContextOptions<BlockchainContext> options)
        : base(options)
    {
        Blocks = Set<Block>();
        Transactions = Set<Transaction>();
        Wallets = Set<Wallet>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlockchainContext).Assembly);
    }
}