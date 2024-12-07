using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Data.Attributes;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data.Repositories;

[Repository(nameof(WalletRepository))]
public class WalletRepository : IWalletRepository<Wallet>
{
    private readonly BlockchainContext _context;

    public WalletRepository(BlockchainContext context)
    {
        _context = context;
    }

    public async Task<Wallet> AddAsync(Wallet entity)
    {
        await _context.Wallets.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<Wallet> entities)
    {
        await _context.Wallets.AddRangeAsync(entities);
    }

    public async Task<IEnumerable<Wallet>> GetAllAsync()
    {
        return await _context.Wallets.AsNoTracking().ToListAsync();
    }

    public async Task<Wallet> GetByIdAsync(string id)
    {
        return await _context.Wallets.FindAsync(id)
            ?? throw new KeyNotFoundException(
                $"{typeof(Wallet).Name} entity with id:{id} not found"
            );
    }

    public async Task<Wallet> GetByNickNameAsync(string nickName)
    {
        return await _context.Wallets.FirstOrDefaultAsync(w => w.NickName == nickName)
            ?? throw new KeyNotFoundException(
                $"{typeof(Wallet).Name} entity with nickname:{nickName} not found"
            );
    }

    public void Remove(Wallet entity)
    {
        _context.Wallets.Remove(entity);
    }

    public void RemoveRange(IEnumerable<Wallet> entities)
    {
        _context.Wallets.RemoveRange(entities);
    }
}
