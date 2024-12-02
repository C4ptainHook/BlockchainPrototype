using Blockchain.Data.Attributes;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data.Repositories;

[Repository(nameof(TransactionRepository))]
public class TransactionRepository : ITransactionRepository<Transaction>
{
    private readonly DbSet<Transaction> _transactions;

    public TransactionRepository(DbSet<Transaction> transactions)
    {
        _transactions = transactions;
    }

    public async Task AddAsync(Transaction entity)
    {
        await _transactions.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<Transaction> entities)
    {
        await _transactions.AddRangeAsync(entities);
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _transactions.AsNoTracking().ToListAsync();
    }

    public async Task<Transaction> GetByIdAsync(int id)
    {
        return await _transactions.FindAsync(id)
            ?? throw new KeyNotFoundException(
                $"{typeof(Transaction).Name} entity with id:{id} not found"
            );
    }

    public void Remove(Transaction entity)
    {
        _transactions.Remove(entity);
    }

    public void RemoveRange(IEnumerable<Transaction> entities)
    {
        _transactions.RemoveRange(entities);
    }
}
