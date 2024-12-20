using Blockchain.Data.Attributes;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Blockchain.Data.Repositories;

[Repository(nameof(TransactionRepository))]
public class TransactionRepository : ITransactionRepository<Transaction>
{
    private readonly BlockchainContext _context;

    public TransactionRepository(BlockchainContext context)
    {
        _context = context;
    }

    public async Task<Transaction> AddAsync(Transaction entity)
    {
        await _context.Transactions.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<Transaction> entities)
    {
        await _context.Transactions.AddRangeAsync(entities);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAttachedToTheBlock(Block? block)
    {
        var transactions = await _context.Transactions.AsNoTracking().ToListAsync();
        return transactions.Where(t => t.BlockId == block?.Id);
    }

    public async Task<Transaction?> GetByIdAsync(string id)
    {
        return await _context.Transactions.FindAsync(id)
            ?? throw new KeyNotFoundException(
                $"{typeof(Transaction).Name} entity with id:{id} not found"
            );
    }

    public void Update(Transaction newTransaction)
    {
        _context.ChangeTracker.Clear();
        _context.Transactions.Update(newTransaction);
    }

    public void Remove(Transaction entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.Transactions.Attach(entity);
        }
        _context.Transactions.Remove(entity);
    }

    public void RemoveRange(IEnumerable<Transaction> entities)
    {
        _context.Transactions.RemoveRange(entities);
    }
}
