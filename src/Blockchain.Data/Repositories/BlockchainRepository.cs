using Blockchain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data.Repositories;

public class BlockchainRepository : BaseRepository<Block>
{
    private readonly DbSet<Block> _blocks;

    public BlockchainRepository(DbSet<Block> blocks)
        : base(blocks)
    {
        _blocks = blocks;
    }
}
