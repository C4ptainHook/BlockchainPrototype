using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blockchain.Data.Repositories;

public class TransactionRepository : BaseRepository<Transaction>, IRemovable<Transaction>
{
    private readonly DbSet<Transaction> _transactions;

    public TransactionRepository(DbSet<Transaction> transactions)
        : base(transactions)
    {
        _transactions = transactions;
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
