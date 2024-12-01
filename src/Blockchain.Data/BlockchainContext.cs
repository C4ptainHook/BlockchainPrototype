using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data;

public class BlockchainContext : DbContext
{
    public BlockchainContext(DbContextOptions<BlockchainContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlockchainContext).Assembly);
    }
}
