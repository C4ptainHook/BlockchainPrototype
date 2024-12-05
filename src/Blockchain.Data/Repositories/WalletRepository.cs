using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blockchain.Data.Attributes;
using Blockchain.Data.Entities;

namespace Blockchain.Data.Repositories;

[Repository(nameof(WalletRepository))]
public class WalletRepository
{
    private readonly BlockchainContext _context;

    public WalletRepository(BlockchainContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Wallet entity)
    {
        await _context.Wallets.AddAsync(entity);
    }
}
