using Blockchain.Data.Attributes;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data.Repositories;

[Repository(nameof(BlockchainRepository))]
public class BlockchainRepository : IBlockchainRepository<Block>
{
    private readonly DbSet<Block> _blocks;

    public BlockchainRepository(DbSet<Block> blocks)
    {
        _blocks = blocks;
    }

    public async Task AddAsync(Block entity)
    {
        await _blocks.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Block> entities)
    {
        await _blocks.AddRangeAsync(entities);
    }

    public async Task<IEnumerable<Block>> GetAllAsync()
    {
        return await _blocks.AsNoTracking().ToListAsync();
    }

    public async Task<Block> GetByIdAsync(int id)
    {
        return await _blocks.FindAsync(id)
            ?? throw new KeyNotFoundException(
                $"{typeof(Block).Name} entity with id:{id} not found"
            );
    }
}
