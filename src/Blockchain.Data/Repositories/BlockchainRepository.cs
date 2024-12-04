using Blockchain.Data.Attributes;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data.Repositories;

[Repository(nameof(BlockchainRepository))]
public class BlockchainRepository : IBlockchainRepository<Block>
{
    private readonly BlockchainContext _context;

    public BlockchainRepository(BlockchainContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Block entity)
    {
        await _context.Blocks.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Block> entities)
    {
        await _context.Blocks.AddRangeAsync(entities);
    }

    public async Task<IEnumerable<Block>> GetAllAsync()
    {
        return await _context.Blocks.AsNoTracking().ToListAsync();
    }

    public async Task<Block> GetByIdAsync(int id)
    {
        return await _context.Blocks.FindAsync(id)
            ?? throw new KeyNotFoundException(
                $"{typeof(Block).Name} entity with id:{id} not found"
            );
    }
}
